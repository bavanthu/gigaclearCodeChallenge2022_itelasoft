using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;


namespace Gigaclear_code_challenge
{
    public class ProcessGraphFile

    {
        private const string nodeId = "[a-zA-Z0-9]+";
        private const string graphmlFile = @"strict processgraphfile ""(.*)"" \{([\s\S]+)\}";
        private const string nodeGraphmlFile = @"(" + nodeId + @") \[([^\]]+)\]";
        private const string edgeGraphmlFileRegex = @"(" + nodeId + @") -- (" + nodeId + @") +\[([^\]]+)\]";

        public List<GigaclearEdge> Edges { get; } = new List<GigaclearEdge>();

        public List<GigaclearNode> Nodes { get; } = new List<GigaclearNode>();


        public void AppendNode(GigaclearNode node)
        {
            if (Nodes.Contains(node))
                return;

            if (Nodes.Any((a) => a.Id == node.Id))
                throw new ArgumentException("Node with same Id already exists");

            Nodes.Add(node);
        }

        public void AppendEdge(GigaclearEdge edge)
        {
            if (!Nodes.Contains(edge.StartNode) || !Nodes.Contains(edge.EndNode))
                throw new ArgumentException("Edge links to non-existant node(s)");

            if (!Edges.Contains(edge))
                Edges.Add(edge);
        }

        public GigaclearNode GetNodeById(string id)
        {
            return Nodes.Single((a) => a.Id == id);
        }

        public static ProcessGraphFile ReadGraphmlFile(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProcessGraphFile));
            Graphml graph;
             using (FileStream stream = File.OpenRead(@"problem.graphml"))
        {
            graph = (Graphml)serializer.Deserialize(stream);
        }
            
            var fileContents = File.ReadAllText(filename);
            
            if (!Regex.IsMatch(fileContents, graphmlFile))
                throw new FormatException($"File '{filename}' is not in correct format");

            var graphMatch = Regex.Match(fileContents, graphmlFile);
            var lines = graphMatch.Groups[2].Value.Split(';');
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

              graph.processGraphmlFileLine(line.Trim());
            }

          return graph;
        }

        public int FindCost(RateCard rateCard)
        {
            var cost = 0;
            cost += rateCard.CabinetRateCard * Nodes.Count(node => node.Type == GigaclearNodeType.Cabinet);
            cost += rateCard.ChamberRateCard * Nodes.Count(node => node.Type == GigaclearNodeType.Chamber);
            cost += rateCard.PotRateCard * Nodes.Count(node => node.Type == GigaclearNodeType.Pot);
            cost += rateCard.TrenchRoadRateCard * Edges.Where(edge => edge.Type == GigaclearEdgeType.Road).Sum(edge => edge.Length);
            cost += rateCard.TrenchVergeRateCard * Edges.Where(edge => edge.Type == GigaclearEdgeType.Verge).Sum(edge => edge.Length);
            cost += rateCard.PotFromCabinetRateCard * Nodes.Where(node => node.Type == GigaclearNodeType.Pot).Sum(node => LengthOfCabinetFromNode(node));
            return cost;
        }

        public int LengthOfCabinetFromNode(GigaclearNode node)
        {
            Dictionary<GigaclearNode, int> distanceToNodes = new Dictionary<GigaclearNode, int>();

            distanceToNodes.Add(node, 0);

            distanceToNodes = calculateDistanceNextNodes(node, distanceToNodes, 0);

            return distanceToNodes.Where((kvp) => kvp.Key.Type == GigaclearNodeType.Cabinet).OrderBy(kvp => kvp.Value).First().Value;
        }


        private Dictionary<GigaclearNode, int> calculateDistanceNextNodes(GigaclearNode node, Dictionary<GigaclearNode, int> distanceToNodes, int distanceAlready)
        {
            var linkedEdges = Edges.Where(edge => edge.StartNode.Id == node.Id || edge.EndNode.Id == node.Id);
            foreach (var edge in linkedEdges)
            {
                var otherNode = edge.StartNode.Id == node.Id ? edge.EndNode : edge.StartNode;
                if (distanceToNodes.ContainsKey(otherNode))
                    continue;
                distanceToNodes.Add(otherNode, edge.Length + distanceAlready);
                distanceToNodes = calculateDistanceNextNodes(otherNode, distanceToNodes, edge.Length + distanceAlready);
            }
            return distanceToNodes;
        }


        private void processGraphmlFileLine(string line)
        {

            if (Regex.IsMatch(line, nodeGraphmlFile))
            {
                var nodeMatch = Regex.Match(line, nodeGraphmlFile);
                var arguments = readArgumentsList(nodeMatch.Groups[2].Value);
                var node = new GigaclearNode(nodeMatch.Groups[1].Value, (GigaclearNodeType)Enum.Parse(typeof(GigaclearNodeType), arguments["type"]));
                AppendNode(node);
            }
            else if (Regex.IsMatch(line, edgeGraphmlFileRegex))
            {
                var edgeMatch = Regex.Match(line, edgeGraphmlFileRegex);
                var startNode = GetNodeById(edgeMatch.Groups[1].Value);
                var endNode = GetNodeById(edgeMatch.Groups[2].Value);
                var arguments = readArgumentsList(edgeMatch.Groups[3].Value);
                var edge = new GigaclearEdge(int.Parse(arguments["length"]), (GigaclearEdgeType)Enum.Parse(typeof(GigaclearEdgeType), arguments["material"], true), startNode, endNode);
                AppendEdge(edge);
            }
            else
            {
                throw new Exception("Can't find line in XML graph");
            }
        }

        private static IDictionary<string, string> readArgumentsList(string argumentsList)
        {
            return Regex.Matches(argumentsList, "([^?=, ]+)(=([^,]*))?").Cast<Match>().ToDictionary(x => x.Groups[1].Value, x => x.Groups[3].Value);
        }

    }
}
