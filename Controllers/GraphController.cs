using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphAPIVisualizer.Objects;
using GraphAPIVisualizer.Database;
using GraphAPIVisualizer;
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
        public IActionResult Get()
        {
            List<Graph> grafos = GraphDB.Instance.GetGraphs;
            return Ok(grafos);
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

                 return Ok(nodeToUpdate); 

            } else {
                return NotFound(); 
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
        public IActionResult GetEdges(int id)
        { 
            Graph graph = GraphDB.Instance.FindById(id);
            if(graph!=null){
                List<Edge> edgeList = graph.Edges;
                return Ok(edgeList);
            }
            else return NotFound("Graph not found");
            
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
        public IActionResult Post(int id, [FromBody] EdgeDTO edgeDTO){ 
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

        // PUT api/Graph/id/edges/id/nodos
        [HttpPut("{id}/edges/{id2}")]
        public IActionResult PutEdges(int id, [FromBody] EdgeDTO edgeDTO, int idEdge){
            Graph graph = GraphDB.Instance.FindById(id);
            if (graph != null) {
                
                int initialNode = edgeDTO.startNodeId;
                int finalNode = edgeDTO.endNodeId;
                int weight = edgeDTO.duration;
                Node startNode = graph.FindNodeById(initialNode);
                Node endNode = graph.FindNodeById(finalNode);
                if (startNode != null && endNode != null) {
                    var e = graph.FindEdgeById(idEdge);
                    e.DestinationNode=endNode;
                    e.SourceNode=startNode;
                    e.Cost=weight;
                    return Ok(e);
                } else {
                    return NotFound("node not found");
                }

            }else{
                return NotFound("graph not found");
            }
        }

        // DELETE api/Graph/id/edges/id
        [HttpDelete("{id}/edges/{id2}")]
        public IActionResult DeleteEdges(int id, [FromBody] EdgeDTO edgeDTO, int idEdge){
            var g = GraphDB.Instance.FindById(id);
            if (g==null){
                return NotFound("Graph not found"); 
            } else {
                var e =GraphDB.Instance.FindById(id).FindEdgeById(idEdge);
                if(e==null){
                    return NotFound("Edge not found");
                }else{
                    var dele =GraphDB.Instance.FindById(id).FindEdgeById(idEdge);
                    GraphDB.Instance.FindById(id).Edges.Remove(dele);
                    return Ok(dele);
                }
            };
        }
        //GET /graph/id/edges/id
        [HttpGet()]
        // GET /graph/id/degree
        [HttpGet("{id}/degree")]
        public IActionResult degree(int id, [FromQuery]string sort){
            
            Graph graph = GraphDB.Instance.FindById(id);
            if (graph!=null){
                List<Node> b= graph.Nodes;
                Node[] nodesArray = new Node[] {};
                int[] degreeArray = new int[]{};
                for(int i=0; i<b.Count; i++){
                    int j=0;
                    int degree = 0;
                    graph.FindNodeById(i);
                    while(graph.FindEdgeById(j)!=null){
                        if(graph.FindEdgeById(j).DestinationNode==graph.FindNodeById(i)||graph.FindEdgeById(j).SourceNode==graph.FindNodeById(i)){
                            degree+=1;
                        }    
                        j+=1;
                    }
                    nodesArray[i] = graph.FindNodeById(i);
                    degreeArray[i]=degree;
                }
                if(sort=="DESC"){
                    nodesArray=sorting(nodesArray, "DESC",degreeArray);
                }
                if(sort=="ASC"){
                    nodesArray=sorting(nodesArray, "ASC",degreeArray);
                }
                return Ok(nodesArray);
            }else return NotFound("Graph not found");

        }
        private Node[] sorting(Node[] array, string iden, int[] dArray){
            int i=0;
            Boolean exito = true;
            while (i<array.Length){
                if ((dArray[i]<dArray[i+1])&&iden=="DESC"){
                    var pos1 = array[i];
                    var pos2 = array[i+1];
                    var pos1a = dArray[i];
                    var pos2a = dArray[i+1];
                    dArray[i]=pos2a;
                    dArray[i+1]=pos1a;
                    array[i]=pos2;
                    array[i+1]=pos1;
                    exito=false;
                }
                if((dArray[i]>dArray[i+1])&&iden=="ASC"){
                    var pos1 = array[i];
                    var pos2 = array[i+1];
                    var pos1a = dArray[i];
                    var pos2a = dArray[i+1];
                    dArray[i]=pos2a;
                    dArray[i+1]=pos1a;
                    array[i]=pos2;
                    array[i+1]=pos1;
                    exito=false;
                }
                i+=1;
            }
            if (exito==false){
                return sorting(array, iden,dArray);
            }
            return array;
        }

        // GET api/graph/id/dijkstra
        [HttpGet("{id}/dijkstra")]
        public IActionResult dijkstra(int id, [FromQuery] int startNode, [FromQuery] int endNode)
        {
            //GraphDB.Instance.FindById(1).FindEdgeById(2).
            int graphSize = GraphDB.Instance.GetSize();
            int[,] adjMatrix = new int[graphSize,graphSize];
            int[] distance = new int[graphSize];
            int[] previous = new int[graphSize];
            for(int i = 1; i<graphSize; i++)
            {
                distance[i] = int.MaxValue;
                previous[i] = 0;
            }
            distance[startNode] = 0;
            PriorityQueue<int> queue = new PriorityQueue<int>();
            queue.Enqueue(startNode,adjMatrix);
            for (int i = 1; i < graphSize; i++)
            {
                for (int j = 1; j < graphSize; j++)
                {
                    if (adjMatrix[i, j] > 0)
                    {
                        queue.Enqueue(i,adjMatrix);
                    }
                }
            }
            // while (!queue.Empty())
            // {
            //     int u = pq.dequeue_min();
           
            //     for (int v = 1; v < graphSize; v++)//scan each row fully
            //     {
            //         if (adjMatrix[u,v] > 0)//if there is an adjacent node
            //         {
            //             int alt = distance[u] + adjMatrix[u, v];
            //             if (alt < distance[v])
            //             {
            //                 distance[v] = alt;
            //                 previous[v] = u;
            //                 queue.Enqueue(u,adjMatrix);
            //             }
            //         }
            //     }
            // }
        //distance to 1..2..3..4..5..6 etc lie inside each index
 
            // for (int i = 1; i < graphSize; i++)
            // {
            // Console.WriteLine("Distance from {0} to {1}: {2}", source, i, distance[i]);
            // }
            return Ok();
        }
        
    }
}
