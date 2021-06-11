using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GraphAPIVisualizer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {

        private readonly ILogger<GraphController> _logger;

        public GraphController(ILogger<GraphController> logger){
            _logger = logger;
        }

        [HttpGet]
        public String Get(){
            return "Hello world";
        }
    }
}
