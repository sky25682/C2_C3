using ApiModules.Requests;
using Microsoft.AspNetCore.Mvc;
using TeamServer.Services;
using TeamServer.Models;

namespace TeamServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListenersController : ControllerBase
    {
        private readonly IListenerService _listeners;

        public ListenersController(IListenerService listeners)
        {
            _listeners = listeners;
        }

        [HttpGet]
        public IActionResult GetListeners()
        {
            var listeners = _listeners.GetListeners();
            return Ok(listeners);
        }

        [HttpGet("{name}")]
        public IActionResult GetListener(string name)
        {
            var listeners = _listeners.GetListener(name);
            if (listeners is null) return NotFound();

            return Ok(listeners);
        }

        [HttpPost]
       public IActionResult StartListener([FromBody] StartHttpListenerRequest request)
         {
            var listener = new HttpListener(request.Name, (request.BindPort));
            listener.Start();
            _listeners.AddListener(listener);

            var root = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            var path = $"{root}/{listener.Name}";

            return Created(root, path);
        }

        [HttpDelete("{name}")]
        public IActionResult StopListener(string name) 
        {
            var listener = _listeners.GetListener(name);
            if (listener is null) return NotFound();
            listener.Stop();

            _listeners.RemoveListener(listener);

            return NoContent();
        }

    }
}
