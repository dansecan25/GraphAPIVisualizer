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
        public IActionResult Post()
        {
            Graph result = new Graph();
            GraphDB.Instance.addGraph(result);
            return Ok(result);
        }

          // DELETE: api/Graph
        [HttpDelete]
        public IActionResult Delete()
        { //deletes all graph elements
            GraphDB.Instance.GetGraphs.Clear();
            return NoContent();

        }

        // GET: api/Graph/id
        [HttpGet("{id}")] //obtains the graph with the specific id
        public IActionResult Get(int id)
        {
            var g = GraphDB.Instance.GetGraph(id);
            if (g==null){
                return NotFound();
            }
            return Ok(g);
        }

         // DELETE: api/Graph/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var g = GraphDB.Instance.GetGraph(id);
            if (g==null){
                return NotFound();
            } else {
                GraphDB.Instance.GetGraphs.Remove(g);
                return NoContent();
            }
            
        }

        // POST api/Graph/id/nodes
        [HttpPost("{id}/nodes")]
        public IActionResult Post(int id, [FromBody] NodeDTO nodeDTO)
        { //
            Graph graph = GraphDB.Instance.FindById(id);
            if (graph != null) {

                String entity = nodeDTO.entity;

                Node node = new Node(entity);

                graph.Nodes.Add(node);

                 return Ok("id asignado al nodo: " + node.Id);

            } else {
                return NotFound();
            }
        }

        // GET api/Graph/id/nodes
        [HttpGet("{id}/nodes")]
        public List<Node> GetNodes(int id)
        { 
            Graph graph = GraphDB.Instance.FindById(id);
            List<Node> nodeList = graph.Nodes;
            return nodeList;
        }

        // PUT api/Graph/id/nodes/id
        [HttpPut("{id}/nodes/{nodeId}")]
        public IActionResult Put(int id, int nodeId, [FromBody] NodeDTO nodeDTO)
        { 
            Graph graph = GraphDB.Instance.FindById(id);
            if (graph != null) {

                String entity = nodeDTO.entity;

                Node nodeToUpdate = graph.FindNodeById(nodeId);

                nodeToUpdate.Entity = entity;

                 return Ok(); 

            } else {
                return NotFound(); // cambiar response
            }
        }

        // DELETE api/Graph/id/nodes/id
        [HttpDelete("{id}/nodes/{nodeId}")]
        public IActionResult Delete(int id, int nodeId)
        {
            var g = GraphDB.Instance.GetGraph(id).FindNodeById(nodeId);
            if (g==null){
                return NotFound(); // cambiar response
            } else {
                GraphDB.Instance.GetGraph(id).Nodes.Remove(g);
                return Ok();
            }
        
        }
        
        // DELETE api/Graph/id/nodes
        [HttpDelete("{id}/nodes")]
        public IActionResult DeleteNodes(int id)
        {
            var g = GraphDB.Instance.GetGraph(id).Nodes;
            if (g==null){
                return NotFound(); // cambiar response
            } else {
                GraphDB.Instance.GetGraph(id).Nodes.Clear();
                return Ok();
            }
        
        }

        // GET api/Graph/id/edges
        [HttpGet("{id}/edges")]
        public List<Edge> GetEdges(int id)
        { 
            Graph graph = GraphDB.Instance.FindById(id);
            List<Edge> edgeList = graph.Edges;
            return edgeList;
        }

        // DELETE api/Graph/id/edges
        [HttpDelete("{id}/edges")]
        public IActionResult DeleteEdge(int id)
        {
            var g = GraphDB.Instance.GetGraph(id).Edges;
            if (g==null){
                return NotFound(); // cambiar response
            } else {
                GraphDB.Instance.GetGraph(id).Edges.Clear();
                return Ok();
            }
        
        }

        // POST api/Graph/id/edges
        [HttpPost("{id}/edges")]

        public IActionResult Post(int id, [FromBody] EdgeDTO edgeDTO){ //
            Graph graph = GraphDB.Instance.FindById(id);
            if (graph != null) {

                Node startNode = graph.FindNodeById(edgeDTO.startNodeId);
                Node endNode = graph.FindNodeById(edgeDTO.endNodeId);
                int duration = edgeDTO.duration;

                if (startNode != null && endNode != null) {
                    Edge edge = new Edge(startNode, endNode, duration);
                    graph.Edges.Add(edge);
                    return Ok(edge);
                } else {
                    return NotFound("node not found");
                }

            } else {
                return NotFound("graph not found");
            }
        }
        // PUT api/Graph/id/edges/id
        // DELETE api/Graph/id/edges/id
        // GET /graph/id/degree
        // GET api/graph/id/dijkstra

        
    }
}
