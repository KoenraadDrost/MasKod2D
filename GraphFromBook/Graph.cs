using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasKod2D.GraphFromBook
{
    public class Graph
    {
        public Node StartLocation { get; set; }
        public Node EndLocation { get; set; }
        public Dictionary<Node, bool> Map { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Graph(int width, int height)
        {
            StartLocation = new Node(0, 0, true);
            EndLocation = new Node(0, 0, true);
            Map = new Dictionary<Node, bool>();
            Width = width;
            Height = height;

            //Create map
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Map.Add(new Node(i, j, true), true);
                }
            }
        }

        public Node GetNode(float x, float y)
        {
            foreach (KeyValuePair<Node, bool> node in Map)
            {
                if (node.Key.Location.X == x && node.Key.Location.Y == y)
                    return node.Key; 
            }
            return null;
        }

        private bool Search(Node currentNode)
        {
            currentNode.State = NodeState.Closed;
            // Get list of adjacent walkable nodes
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
            
            // Sort the list by F-value before iterating over its contents
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));

            foreach (Node nextNode in nextNodes)
            {
                if (nextNode.Location == EndLocation.Location)
                {
                    EndLocation.ParentNode = nextNode.ParentNode;
                    return true;
                }
                else
                {
                    // Recurses back into Search(Node)
                    if (Search(nextNode))
                    {
                        return true;
                    }
                        
                }
            }
            return false;
        }

        private IEnumerable<Node> GetAdjacentNodes(Node currentNode)
        {
            List<Node> adj = new List<Node>();

            // Top
            if (currentNode.Location.Y - 1 > 0)
                adj.Add(GetNode(currentNode.Location.X, currentNode.Location.Y - 1));

            // Mid Left
            if (currentNode.Location.X - 1 > -1)
                adj.Add(GetNode(currentNode.Location.X - 1, currentNode.Location.Y));

            // Mid Right
            if (currentNode.Location.X + 1 < Width)
                adj.Add(GetNode(currentNode.Location.X + 1, currentNode.Location.Y));

            // Bottom Middle
            if (currentNode.Location.Y + 1 < Height)
                adj.Add(GetNode(currentNode.Location.X, currentNode.Location.Y + 1));

            /*// Top Left
            if (currentNode.Location.X - 1 > -1 && currentNode.Location.Y - 1 > -1)
                adj.Add(GetNode(currentNode.Location.X - 1, currentNode.Location.Y - 1));

            // Top Right
            if (currentNode.Location.X + 1 < Width && currentNode.Location.Y - 1 > -1)
                adj.Add(GetNode(currentNode.Location.X + 1, currentNode.Location.Y - 1));

            // Bottom Left
            if (currentNode.Location.X - 1 > -1 && currentNode.Location.Y + 1 < Height)
                adj.Add(GetNode(currentNode.Location.X - 1, currentNode.Location.Y + 1));

            // Bottom Right
            if (currentNode.Location.X + 1 < Width && currentNode.Location.Y + 1 < Height)
                adj.Add(GetNode(currentNode.Location.X + 1, currentNode.Location.Y + 1));*/

            return adj;
        }

        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Node> nextLocations = GetAdjacentNodes(fromNode);

            foreach (var location in nextLocations)
            {
                int x = (int)location.Location.X;
                int y = (int)location.Location.Y;

                // Stay within the grid's boundaries
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    continue;

                Node node = GetNode(x, y);

                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                    continue;

                // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                    continue;

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
                {
                    float traversalCost = node.GetTraversalCost(node, node.ParentNode, EndLocation);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.SetF(node, node.ParentNode, EndLocation);
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }

                
            }

            return walkableNodes;
        }

        public List<Node> FindPath()
        {
            List<Node> path = new List<Node>();
            bool success = Search(StartLocation);
            if (success)
            {
                Node node = EndLocation;
                while (node.ParentNode != null)
                { 
                    path.Add(node); 
                    node = node.ParentNode;
                }
                path.Reverse();
            }
            return path;
        }
    }
}
