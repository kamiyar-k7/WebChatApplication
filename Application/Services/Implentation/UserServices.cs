using Application.Passhelper;
using Application.Serilizer;
using Application.Services.Interfaces;
using Application.Statics;
using Application.ViewModel_And_Dto.Dto;
using Application.ViewModel_And_Dto.Dto.UserSide;
using AutoMapper;
using Doamin.Entities.UserEntities;
using Doamin.IRepository.UserPart;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace Application.Services.Implentation;

public class UserServices : IUserServices
{
    #region Ctor

    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IValidator<object> _validator;
    private readonly IMemoryCache _cache;


    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    public UserServices(IConfiguration configuration,
        IMapper mapper,
        IValidator<object> validator,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMemoryCache cache)
    {
        _configuration = configuration;
        _mapper = mapper;
        _validator = validator;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _cache = cache;
    }

    #endregion

    #region Auth services

    #region SignUp

    public async Task<int> SendVerfiyCode(string userEmail )
    {
        var email = _configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
        var password = _configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
        var host = _configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
        var port = _configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

        var smtp = new SmtpClient(host, port);
        smtp.Credentials = new NetworkCredential(email, password);
        smtp.EnableSsl = true;

        var veridycode = GenerateVerificationCode();

        var mailMessage = new MailMessage()
        {
            From = new MailAddress(email),
            Subject = "Email Verification For Kaaf Chat",
            Body = $"Welcom To KaafChat  \n\n Your Verification code is:  {veridycode} \n\n Please enter this code to verify your email address\n\n if you didnt send any request just forget this Email \n Thank you",
            IsBodyHtml = false

        };

        mailMessage.To.Add(userEmail);


        await smtp.SendMailAsync(mailMessage);
        return veridycode;
    }

    public int GenerateVerificationCode()
    {
        int length = 6;
        var random = new Random();
        var code = new char[length];

        for (int i = 0; i < length; i++)
        {
            code[i] = (char)('0' + random.Next(0, 10));
        }


        return int.Parse(code);
    }

    public async Task GetEmailForSignUp(UserSignUpDto userSignUpDto)
    {
        // dto validator
        var res = await _validator.ValidateAsync(userSignUpDto);
        if (!res.IsValid)
        {
            var errorgroups = res.Errors.GroupBy(x => x.PropertyName).ToList();
            List<ValidationDto> errors = errorgroups.Select(x => new ValidationDto
            {
                propertyname = x.Key,
                errors = x.Select(x => x.ErrorMessage).ToList()
            }).ToList();
            var serializederrors = MyJsonSerial.Serialize(errors);
            throw new Exception(serializederrors);
        }

        #region Check existness

        var emailexist = await _userRepository.IsEmailExist(userSignUpDto.UserEmail);
        if (emailexist)
        {
            throw new Exception("The Email Is Already Exist");
        }

        var nameexist = await _userRepository.IsUserNameExist(userSignUpDto.UserName);
        if (nameexist)
        {
            throw new Exception("The UserName Is AlreadeyExist \n Please Try Another UserName");
        }
        #endregion


        var encodedPass = PassHelper.EncodePasswordMd5(userSignUpDto.Password);

        var verifycode = await SendVerfiyCode(userSignUpDto.UserEmail );


        var userCacheData = new Dictionary<string, object>
        {
          { "EncodedPassword", encodedPass },
          { "UserEmail", userSignUpDto.UserEmail },
            {"UserName" , userSignUpDto.UserName }

        };


        _cache.Set("UserDetails", userCacheData, TimeSpan.FromMinutes(20));
        _cache.Set(userSignUpDto.UserEmail, verifycode, TimeSpan.FromMinutes(3));
      
    }

    public async Task ResenVerifyCode(string UserEmial)
    {
        var verifycode = await SendVerfiyCode(UserEmial);

        _cache.Set(UserEmial, verifycode, TimeSpan.FromMinutes(3));

    }

    public async Task<bool> VerifyCode(UserVerifyDto verifyDto)
    {
        _cache.TryGetValue(verifyDto.UserEmail, out int usercode);

        if (usercode != 0)
        {
            if (verifyDto.Code == usercode)
            {
                await SignUp();
                return true;

            }
            else
            {
                throw new Exception("The Code Is Invalid!");
            }

        }
        else
        {
            throw new Exception("Ask For New Code");
        }


    }

    public async Task SignUp()
    {

        if (!_cache.TryGetValue("UserDetails", out Dictionary<string, object> cacheddata) || cacheddata == null)
        {
            throw new Exception("Your SignUp Got Failed. Please Try Again. Visit /signup to try again.");

        }

        var pass = cacheddata["EncodedPassword"].ToString();
        var userEmail = cacheddata["UserEmail"].ToString();

        var emailexist = await _userRepository.IsEmailExist(userEmail);
        if (emailexist)
        {
            throw new Exception("The Email Is Already Exist");
        }

        var userName = cacheddata["UserName"].ToString();

        var role = await _roleRepository.GetRoleName(StaticRoleNames.User);

        var user = new User()
        {
            RoleName = role.RoleName,
            CreatedAt = DateTime.Now,
            UserEmail = userEmail,
            UserName = userName,
            Password = pass,

        };

        UserSelectedRole userSelectedRole = new UserSelectedRole()
        {
            Role = role,
            User = user,
            RoleId = role.Id,
            UserId = user.Id,
            RoleName = role.RoleName,
            UserName = user.UserName
        };

        user.userSelectedRoles = new List<UserSelectedRole> { userSelectedRole };
        await _userRepository.AddUser(user);


    }

    #endregion

    #region Sign In

    public async Task<string> SignIn(UserSignInDto userDto)
    {

        #region Validation

        var res = await _validator.ValidateAsync(userDto);
        if (!res.IsValid)
        {
            var errorgroups = res.Errors.GroupBy(x => x.PropertyName).ToList();
            var Errors = errorgroups.Select(x => new
            {
                propertyname = x.Key,
                errors = x.Select(x => x.ErrorMessage).ToList()
            }).ToList();

            var serializederrors = JsonSerializer.Serialize(Errors, options: new JsonSerializerOptions { WriteIndented = true });

            throw new Exception(serializederrors);
        }

        #endregion

        var encodedPass = PassHelper.EncodePasswordMd5(userDto.Password);

        var user = await _userRepository.SignIn(userDto.UserEmail, encodedPass);

        if (user == null)
        {
            //throw new Exception("The Emial Or Password Are Incorrect");
            return null;
        }

        string token = await GenerateToken(user);

        var serializedtoken = MyJsonSerial.Serialize(token);
        return serializedtoken;

    }

    private async Task<string> GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var crediantials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub , user.UserName),
            new Claim(JwtRegisteredClaimNames.Name , user.UserName),
            new Claim(JwtRegisteredClaimNames.Email , user.UserEmail),
            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
            new Claim(ClaimTypes.Role , user.RoleName),
            new Claim("UserId" , user.Id.ToString())

        };

        var token = new JwtSecurityToken
            (
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["JwtSettings:Duration"])),
            signingCredentials: crediantials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }


    #endregion

    #endregion


    #region user Servcies for chat

    public async Task<List<OtherUserDto>> FindUsers(string UserName)
    {
        var users = await _userRepository.FindUsers(UserName);

        List<OtherUserDto> mapped = _mapper.Map<List<OtherUserDto>>(users);

        return mapped;

    }

    public async Task<OtherUserDto> GetOtheUserDetails(int id)
    {
        var user = await _userRepository.GetOtherUserDetails(id);

        var mapped = _mapper.Map<OtherUserDto>(user);

        return mapped;

    }

    #endregion
}
