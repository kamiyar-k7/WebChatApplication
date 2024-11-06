using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebChatApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class RolesController : ControllerBase
{

    #region Ctor

    public RolesController()
    {
            
    }

    #endregion
}
