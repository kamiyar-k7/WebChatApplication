using Application.Serilizer;
using Application.Services.Interfaces;
using Application.Statics;
using Application.ViewModel_And_Dto.Dto;
using Application.ViewModel_And_Dto.Dto.UserSide;
using AutoMapper;
using Doamin.Entities.UserEntities;
using Doamin.IRepository.UserPart;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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

    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    public UserServices(IConfiguration configuration,
        IMapper mapper,
        IValidator<object> validator,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _configuration = configuration;
        _mapper = mapper;
        _validator = validator;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    #endregion

    #region Auth services
    public async Task SignUp(UserSignUpDto userDto)
    {
        var res = await _validator.ValidateAsync(userDto);
        if (!res.IsValid)
        {
            var errorGroups = res.Errors.GroupBy(x => x.PropertyName).ToList();
            List<ValidationDto> errors = errorGroups.Select(x => new ValidationDto
            {
                propertyname = x.Key,
                errors = x.Select(x => x.ErrorMessage).ToList()
            }).ToList();

            //    var serializederrors = JsonSerializer.Serialize(errors, options: new JsonSerializerOptions { WriteIndented = true });
            var serializederrors = MyJsonSerial.Serialize(errors);
            throw new Exception(serializederrors);
        }
        var exist = await _userRepository.IsExist(userDto.UserEmail);
        if (exist)
        {
            throw new Exception("The Email Is Already Exist");
        }

        var role = await _roleRepository.GetRoleName(StaticRoleNames.User);

        var user = _mapper.Map<User>(userDto); // encode password later
        user.RoleName = role.RoleName;
        user.CreatedAt = DateTime.Now;

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


        var user = await _userRepository.SignIn(userDto.UserEmail, userDto.Password);

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


    public async Task<List<UserSearchDto>> FindUsers(string UserName)
    {
        var users = await _userRepository.FindUsers(UserName);

        List<UserSearchDto> mapped = _mapper.Map<List<UserSearchDto>>(users);

        return mapped;

    }


}
