using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamServer.Services;



namespace TeamServer.Controllers
{

    [ApiController]

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

            var listener = _listeners.GetListener(name);
            if (listener is null) return NotFound();

            return Ok(listener);

        }


    }
}
