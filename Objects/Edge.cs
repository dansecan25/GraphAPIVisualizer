using System;
using System.Collections.Generic;

namespace GraphAPIVisualizer.Objects{
    public class Edge{
        private static int currentId=0;
        private int id;
        private long duration;
        private Node sourceNode;
        private Node destinationNode;
        public Edge(Node sourceNode, Node destinationNode, long duration){
            this.id = Edge.currentId++;
            this.sourceNode = sourceNode;
            this.destinationNode = destinationNode;
            this.duration = duration;
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
            get {return duration;} 
            set {duration = value;}
        }
        public int Id{
            get {return this.id;}
        }
        
    }
}