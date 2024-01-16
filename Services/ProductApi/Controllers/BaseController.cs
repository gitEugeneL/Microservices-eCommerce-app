using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;

[ApiVersion(1)]
[ApiController]
public class BaseController : ControllerBase
{
    protected string? CurrentUserId() => 
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    protected string? CurrentUserRole() =>
        User.FindFirst(ClaimTypes.Role)?.Value;
}
