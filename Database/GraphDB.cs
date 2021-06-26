using System;
using System.Collections.Generic;
using GraphAPIVisualizer.Objects;

namespace GraphAPIVisualizer.Database{
    public class GraphDB{
        private static GraphDB instance;
        private static readonly object padlock = new object();

        private List<Graph> graphs = new List<Graph>();
        private int size = 0;

        private GraphDB()
        {
            this.graphs = new List<Graph>();
            this.size = 0;
        }
        public void addGraph(Graph graph)
        {
            this.graphs.Add(graph);
            this.size += 1;
        }

        public Graph GetGraph(int id)
        {
            foreach (Graph g in graphs)
            {
                if (g.Id == id)
                {
                    return g;
                }
            }
            return null;
        }
        public static GraphDB Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GraphDB();
                    }
                    return instance;
                }
            }
        }

        public List<Graph> GetGraphs
        {
            get
            {
                return GraphDB.Instance.graphs;
            }
        }


        public Graph FindById(int id) {
            return graphs.Find(x => x.Id.Equals(id));
        }

        public int GetSize()
        {
            return this.size;
        }

    }
}