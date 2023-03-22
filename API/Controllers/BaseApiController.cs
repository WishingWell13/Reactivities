using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        //Double question mark means assign if null
        protected IMediator Mediator => _mediator ??=
                HttpContext.RequestServices.GetService<IMediator>();
    }
}