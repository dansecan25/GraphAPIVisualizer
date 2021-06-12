using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphAPIVisualizer.Objects;
namespace GraphAPIVisualizer.Controllers
{
    [ApiController] //required by ASP to identify the controller
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<GraphController> _logger;
        private static List<Graph> graphs = new List<Graph>();
        
        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Graph> Get()
        {
            return GraphController.graphs;
        }
        [HttpGet("{id}")]
        public Graph Get(int id){
            return GraphController.graphs.ElementAt(id);
        }

        [HttpPost]
        public IActionResult Post(){
            GraphController.graphs.Add(new Graph());
            return Ok();
        }
        [HttpDelete]
        public void Delete(){ //deletes all graph elements
            int i=0;
            while(i <= GraphController.graphs.Count){
                GraphController.graphs.RemoveAt(i);
                i+=1;
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id){
            GraphController.graphs.RemoveAt(id);
        }
    }
}
