using System.Security.Claims;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Utils;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController<T> : ControllerBase
    where T : Entity
{
    protected BaseService<T> _Service = null;
    protected Serilog.ILogger log = Logger.ContextLog<BaseController<T>>();
    protected String UserEmail = null;
    protected ClaimsPrincipal ClaimsPrincipal = null;

    public BaseController(BaseService<T> service, IHttpContextAccessor accessor)
    {
        this._Service = service;
        Task model = this._Service.SetModelState(this.ModelState);
        model.Wait();

        if (accessor != null)
        {
            if (accessor.HttpContext != null)
            {
                if (accessor.HttpContext.User != null)
                {
                    ClaimsPrincipal = accessor.HttpContext.User;
                    var identy = (ClaimsIdentity)ClaimsPrincipal.Identity;
                    if (identy != null)
                    {
                        IEnumerable<Claim> claims = identy.Claims;
                        Claim email = identy.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

                        if (email != null)
                        {
                            UserEmail = email.Value;
                            Task loaduser = this._Service.Load(UserEmail);
                            loaduser.Wait();
                        }
                        else
                        {
                            log.Debug("");
                        }
                    }
                    else
                    {
                        log.Debug("Identity is null");
                    }
                }
                else
                {
                    log.Debug("User is null");
                }
            }
            else
            {
                log.Debug("HTTPcontext is null");
            }
        }
        else
        {
            log.Debug("Accessor is null");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<T>> Get(String id)
    {
        T result = await _Service.Get(id);
        if (result == null)
        {
            return new NotFoundObjectResult(null);
        }
        return result;
    }
}
