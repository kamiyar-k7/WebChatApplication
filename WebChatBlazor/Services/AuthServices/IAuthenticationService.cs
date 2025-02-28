﻿using WebChatBlazor.Services.Base;

namespace WebChatBlazor.Services.AuthServices;

public interface IAuthenticationService
{
    Task SignUp(UserSignUpDto userdto);
    Task<bool> VerifyCode(UserVerifyDto verifyDto);
    Task ResendCode(string useremail);

    Task SignIn(UserSignInDto userdto);
    Task GetToken();
}
