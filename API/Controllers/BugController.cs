using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugController:BaseAPIControler
    {
        private readonly StoreContext _context;
        public BugController(StoreContext context)
        {
            _context=context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing=_context.products.Find(42);
            if(thing==null)
            {
                return NotFound(new APIResponse(404));
            }
             return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServererror()
        {
             var thing=_context.products.Find(42);
             var thingToReturn=thing.ToString();
             return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
             return BadRequest(new APIResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
             return Ok();
        }
    }
}