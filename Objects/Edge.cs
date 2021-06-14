using System;
using System.Collections.Generic;

namespace GraphAPIVisualizer.Objects{
    public class Edge{
        private static int currentId=0;
        private int id;
        private long cost;
        private Node sourceNode;
        private Node destinationNode;
        public Edge(Node sourceNode, Node destinationNode, long cost){
            this.id = Edge.currentId++;
            this.sourceNode = sourceNode;
            this.destinationNode = destinationNode;
            this.cost = cost;
        }



     public Node DestinationNode{
            get {return destinationNode;} 
            set {destinationNode = value;}
        }
        public Node SourceNode{
            get {return sourceNode;} 
            set {sourceNode = value;}
        }

        public long Cost{
            get {return cost;} 
            set {cost = value;}
        }
        public int Id{
            get {return this.id;}
        }
        
    }
}