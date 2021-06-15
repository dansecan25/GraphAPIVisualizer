using System;
using System.Collections.Generic;

namespace GraphAPIVisualizer.Objects{
    public class Graph{
        private static int currentId=0;
        private int id;
        private List<Node> nodes = new List<Node>();
        private List<Edge> edges = new List<Edge>();
        public Graph() {
            this.id=currentId++;
        }
        public Graph(int id){
            this.id=id;
        }
        public int Id
        {
            get{return this.id;}
            set{this.id = value;}
        }
        public List<Node> Nodes{
            get { return nodes;}
            set {this.nodes = value;}
        }
            
        public List<Edge> Edges{
            get { return edges;}
        }

        public Node FindNodeById(int id) {
            return nodes.Find(x => x.Id.Equals(id));
        }
        public Edge FindEdgeById(int id) {
            return edges.Find(x => x.Id.Equals(id));
        }

        

    }

}