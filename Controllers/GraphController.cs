using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphAPIVisualizer.Objects;
using GraphAPIVisualizer.Database;
namespace GraphAPIVisualizer.Controllers
{
    [ApiController] //required by ASP to identify the controller
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        private readonly ILogger<GraphController> _logger;
        
        public GraphController(ILogger<GraphController> logger)
        {
            _logger = logger;
        }

        // GET: api/Graph
        [HttpGet]
        public List<Graph> Get()
        {
            return GraphDB.Instance.GetGraphs;
        }

         // POST: api/Graph
        [HttpPost]
        public IActionResult Post(){
            Graph result = new Graph();
            GraphDB.Instance.addGraph(result);
            return Ok(result);
        }

          // DELETE: api/Graph
        [HttpDelete]
        public IActionResult Delete(){ //deletes all graph elements
            GraphDB.Instance.GetGraphs.Clear();
            return NoContent();

        }

        // GET: api/Graph/id
        [HttpGet("{id}")] //obtains the graph with the specific id
        public IActionResult Get(int id){
            var g = GraphDB.Instance.GetGraph(id);
            if (g==null){
                return NotFound();
            }
            return Ok(g);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var g = GraphDB.Instance.GetGraph(id);
            if (g==null){
                return NotFound();
            } else {
                GraphDB.Instance.GetGraphs.Remove(g);
                return NoContent();
            }
            
        }

       

        [HttpPost("{id}/edges")]
        public IActionResult Post(int id, [FromBody] EdgeDTO edgeDTO){ //
            Graph graph = GraphDB.Instance.FindById(id);
            if (graph != null) {

                Node startNode = edgeDTO.startNode;
                Node endNode = edgeDTO.endNode;
                int duration = edgeDTO.duration;

                Edge edge = new Edge(startNode, endNode, duration);

                graph.Edges.Add(edge);

                 return Ok("se encontro el grafo " + id);

            } else {
                return Ok("no se encontro el grafo " + id);
            }
           
        }



      

      
        
    }
}
