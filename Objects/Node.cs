using System;
using System.Collections.Generic;

namespace GraphAPIVisualizer.Objects{
    public class Node{
        private static int currentId=0;
        private int id;
        private String entity;
        private int inDegree = 0;
        private int outDegree = 0;
        public Node(){
            this.id = Node.currentId++;
        }
        public Node(String entity){
            this.entity = entity;
        }
        public String Entity{
            get{return entity;} 
            set {this.entity=value;}
        }
        public int Id{
            get {return this.id;}
            set {this.id=value;}
        }
        
    }
}