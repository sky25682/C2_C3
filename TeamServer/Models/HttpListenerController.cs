﻿using Microsoft.AspNetCore.Mvc;

namespace TeamServer.Models
{
    
        [Controller]
        public class HttpListenerController : ControllerBase
        {
            public IActionResult HandleImplant()
            {
                return Ok("You listener workds");
            }
        }

    
}
