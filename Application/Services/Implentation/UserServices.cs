using Application.Services.Interfaces;
using Application.Staticks;
using Application.ViewModel_And_Dto.Dto;
using Application.ViewModel_And_Dto.Dto.UserSide;
using AutoMapper;
using Doamin.Entities.UserEntities;
using Doamin.IRepository.UserPart;
using FluentValidation;
using System.Text.Json;

namespace Application.Services.Implentation;

public class UserServices : IUserServices
{
    private readonly IMapper _mapper;
    private readonly IValidator<UserDto> _validator;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    #region Ctor
    public UserServices(IMapper mapper,
        IValidator<UserDto> validator,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _mapper = mapper;
        _validator = validator;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    #endregion


    public async Task AddUser(UserDto userDto)
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

            var serializederrors = JsonSerializer.Serialize(errors, options: new JsonSerializerOptions { WriteIndented = true });

            throw new Exception(serializederrors);
        }


        var role = await _roleRepository.GetRoleName(StaticRoleNames.User);

        var user = _mapper.Map<User>(userDto);
        user.RoleName = role.RoleName;

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



}
