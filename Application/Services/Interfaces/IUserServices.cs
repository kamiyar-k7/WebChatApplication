﻿using Application.ViewModel_And_Dto.Dto.UserSide;

namespace Application.Services.Interfaces;

public interface IUserServices
{
    Task SignUp(UserSignUpDto userDto);
    
    Task<string> SignIn(UserSignInDto userDto);
}