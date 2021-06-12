using System;
using System.Collections.Generic;

namespace GraphAPIVisualizer.Objects{
    public class Node{
        private static int currentId=0;
        private int id;
        private String value;
        public Node(){
            this.id = Node.currentId++;
        }
        public Node(int id){
            this.id=id;
        }
        public String Value{
            get{return value;} 
            set {this.value=value;}
        }
        public int Id{
            get {return this.id;}
            set {this.id=value;}
        }
        
    }
}