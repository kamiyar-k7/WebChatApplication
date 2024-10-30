namespace WebChatBlazor.Services.AuthServices;

public interface IUserProvider
{

    Task<UserContext> SetCurrentUserFromClaims();
}
