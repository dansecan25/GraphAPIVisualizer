using System;
using System.Collections.Generic;

namespace GraphAPIVisualizer.Objects{
    public class EdgeDTO{
        public int startNodeId {get; set;}
        public int endNodeId {get; set;}
        public int duration {get; set;}
    }
}