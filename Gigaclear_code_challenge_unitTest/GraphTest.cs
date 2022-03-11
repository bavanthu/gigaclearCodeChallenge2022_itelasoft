using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using Gigaclear_code_challenge;

namespace Gigaclear_code_challenge_unitTest
{
    public class GraphTest
    {
        [Test]
        public void NewEmptyGraph()
        {
            var graph = new Graph();

            Assert.That(graph.Nodes.Count, Is.EqualTo(0));
            Assert.That(graph.Edges.Count, Is.EqualTo(0));
        }
      
        [Test]
        public void AppendNode()
        {
            var graph = new Graph();
            var node = new GigaclearNode("A", GigaclearNodeType.Cabinet);

            graph.AppendNode(node);

            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
            Assert.That(graph.Nodes, Contains.Item(node));
            Assert.That(graph.Edges.Count, Is.EqualTo(0));
        }

        [Test]
        public void AppendStartNode()
        {
            var graph = new Graph();
            var nodeA = new GigaclearNode("A", GigaclearNodeType.Cabinet);
            var nodeB = new GigaclearNode("B", GigaclearNodeType.Cabinet);
            graph.AppendNode(nodeA);
            var edge = new GigaclearEdge(10, GigaclearEdgeType.Road, nodeA, nodeB);

            Assert.Throws<ArgumentException>(() => graph.AppendEdge(edge));
        }

        [Test]
        public void AppendSecondNode()
        {
            var graph = new Graph();
            var nodeA = new GigaclearNode("A", GigaclearNodeType.Cabinet);
            graph.AppendNode(nodeA);
            var nodeB = new GigaclearNode("B", GigaclearNodeType.Cabinet);

            graph.AppendNode(nodeB);

            Assert.That(graph.Nodes.Count, Is.EqualTo(2));
            Assert.That(graph.Nodes, Contains.Item(nodeB));
            Assert.That(graph.Edges.Count, Is.EqualTo(0));
        }

        [Test]
        public void AppendDuplicateNode()
        {
            var graph = new Graph();
            var node = new GigaclearNode("A", GigaclearNodeType.Cabinet);
            graph.AppendNode(node);

            graph.AppendNode(node);

            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
            Assert.That(graph.Nodes, Contains.Item(node));
            Assert.That(graph.Edges.Count, Is.EqualTo(0));
        }

        [Test]
        public void AppendEdge()
        {
            var graph = new Graph();
            var nodeA = new GigaclearNode("A", GigaclearNodeType.Cabinet);
            var nodeB = new GigaclearNode("B", GigaclearNodeType.Cabinet);
            graph.AppendNode(nodeA);
            graph.AppendNode(nodeB);
            var edge = new GigaclearEdge(10, GigaclearEdgeType.Road, nodeA, nodeB);

            graph.AppendEdge(edge);

            Assert.That(graph.Nodes.Count, Is.EqualTo(2));
            Assert.That(graph.Edges.Count, Is.EqualTo(1));
            Assert.That(graph.Edges, Contains.Item(edge));
        }

        [Test]
        public void AppendSecondEdge()
        {
            var graph = new Graph();
            var nodeA = new GigaclearNode("A", GigaclearNodeType.Cabinet);
            var nodeB = new GigaclearNode("B", GigaclearNodeType.Cabinet);
            var nodeC = new GigaclearNode("C", GigaclearNodeType.Cabinet);
            graph.AppendNode(nodeA);
            graph.AppendNode(nodeB);
            graph.AppendNode(nodeC);
            var edge1 = new GigaclearEdge(10, GigaclearEdgeType.Road,nodeA, nodeB);
            graph.AppendEdge(edge1);
            var edge2 = new GigaclearEdge(10, GigaclearEdgeType.Road, nodeB, nodeC);

            graph.AppendEdge(edge2);

            Assert.That(graph.Nodes.Count, Is.EqualTo(3));
            Assert.That(graph.Edges.Count, Is.EqualTo(2));
            Assert.That(graph.Edges, Contains.Item(edge2));
        }

        [Test]
        public void AppendDuplicateEdge()
        {
            var graph = new Graph();
            var nodeA = new GigaclearNode("A", GigaclearNodeType.Cabinet);
            var nodeB = new GigaclearNode("B", GigaclearNodeType.Cabinet);
            graph.AppendNode(nodeA);
            graph.AppendNode(nodeB);
            var edge = new GigaclearEdge(10, GigaclearEdgeType.Road, nodeA, nodeB);
            graph.AppendEdge(edge);

            graph.AppendEdge(edge);

            Assert.That(graph.Nodes.Count, Is.EqualTo(2));
            Assert.That(graph.Edges.Count, Is.EqualTo(1));
            Assert.That(graph.Edges, Contains.Item(edge));
        }

       

        [Test]
        public void AppendMissedEdge_EndNode()
        {
            var graph = new Graph();
            var nodeA = new GigaclearNode("A", GigaclearNodeType.Cabinet);
            var nodeB = new GigaclearNode("B", GigaclearNodeType.Cabinet);
            graph.AppendNode(nodeB);
            var edge = new GigaclearEdge(10, GigaclearEdgeType.Road, nodeA, nodeB);

            Assert.Throws<ArgumentException>(() => graph.AppendEdge(edge));
        }
        [Test]
        public void ReadDotFileMultipleNodes()
        {
            var lines = "C [type=Pot];\r\n" +
                        "ASD [type=Chamber];";

            var graph = ReadDotFileHelper(lines);

            Assert.That(graph.Nodes.Count, Is.EqualTo(2));
            Assert.That(graph.Edges.Count, Is.EqualTo(0));
            Assert.That(graph.Nodes[0].Id, Is.EqualTo("C"));
            Assert.That(graph.Nodes[0].Type, Is.EqualTo(GigaclearNodeType.Pot));
            Assert.That(graph.Nodes[1].Id, Is.EqualTo("ASD"));
            Assert.That(graph.Nodes[1].Type, Is.EqualTo(GigaclearNodeType.Chamber));
        }


        [Test]
        public void ReadDotFileSingleNode()
        {
            var lines = "A [type=Cabinet];";

            var graph = ReadDotFileHelper(lines);

            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
            Assert.That(graph.Edges.Count, Is.EqualTo(0));
            Assert.That(graph.Nodes[0].Id, Is.EqualTo("A"));
            Assert.That(graph.Nodes[0].Type, Is.EqualTo(GigaclearNodeType.Cabinet));
        }

        [Test]
        public void ReadDotFileSingleNodeAlt()
        {
            var lines = "C [type=Pot];";

            var graph = ReadDotFileHelper(lines);

            Assert.That(graph.Nodes.Count, Is.EqualTo(1));
            Assert.That(graph.Edges.Count, Is.EqualTo(0));
            Assert.That(graph.Nodes[0].Id, Is.EqualTo("C"));
            Assert.That(graph.Nodes[0].Type, Is.EqualTo(GigaclearNodeType.Pot));
        }
        [Test]
        public void ReadDotFileMultipleEdge()
        {
            var lines = "START [type=Pot];\r\n" +
                        "End [type=Chamber];\r\n" +
                        "Node [type=Cabinet];\r\n" +
                        "START -- Node  [material=verge, length=100];\r\n" +
                        "End -- Node  [material=road, length=56];";

            var graph = ReadDotFileHelper(lines);
            Assert.That(graph.Nodes.Count, Is.EqualTo(3));
            Assert.That(graph.Edges.Count, Is.EqualTo(2));
            Assert.That(graph.Edges[0].StartNode.Id, Is.EqualTo("START"));
            Assert.That(graph.Edges[0].EndNode.Id, Is.EqualTo("Node"));
            Assert.That(graph.Edges[0].Type, Is.EqualTo(GigaclearEdgeType.Verge));
            Assert.That(graph.Edges[0].Length, Is.EqualTo(100));
            Assert.That(graph.Edges[1].StartNode.Id, Is.EqualTo("End"));
            Assert.That(graph.Edges[1].EndNode.Id, Is.EqualTo("Node"));
            Assert.That(graph.Edges[1].Type, Is.EqualTo(GigaclearEdgeType.Road));
            Assert.That(graph.Edges[1].Length, Is.EqualTo(56));
        }

        [Test]
        public void ReadDotFileSingleEdge()
        {
            var lines = "START [type=Pot];\r\n" +
                        "End [type=Chamber];\r\n" +
                        "START -- End  [material=verge, length=100];";

            var graph = ReadDotFileHelper(lines);
            Assert.That(graph.Nodes.Count, Is.EqualTo(2));
            Assert.That(graph.Edges.Count, Is.EqualTo(1));
            Assert.That(graph.Edges[0].StartNode.Id, Is.EqualTo("START"));
            Assert.That(graph.Edges[0].EndNode.Id, Is.EqualTo("End"));
            Assert.That(graph.Edges[0].Type, Is.EqualTo(GigaclearEdgeType.Verge));
            Assert.That(graph.Edges[0].Length, Is.EqualTo(100));
        }


        [TestCase(2, 3, 4, 4, 2, 5, 32)]
        [TestCase(1, 10, 2, 100, 3, 1000, 3210)]

        public void SumOfCostsGigaclearNodeTypes(int numCabinets, int costCabinet, int numChambers, int costChamber, int numPots, int costPot, int totalCost)
        {
            var rateCard = new RateCard { CabinetRateCard = costCabinet, PotRateCard = costPot, ChamberRateCard = costChamber };
            var graph = new Graph();
            var n = 0;
            for (int i = 0; i < numCabinets; i++)
                graph.AppendNode(new GigaclearNode((++n).ToString(), GigaclearNodeType.Cabinet));
            for (int i = 0; i < numPots; i++)
                graph.AppendNode(new GigaclearNode((++n).ToString(), GigaclearNodeType.Pot));
            for (int i = 0; i < numChambers; i++)
                graph.AppendNode(new GigaclearNode((++n).ToString(), GigaclearNodeType.Chamber));

            var cost = graph.FindCost(rateCard);

            Assert.That(cost, Is.EqualTo(totalCost));
        }

        [Test]
        public void SumOfCosts_Road_Edge()
        {
            var rateCard = new RateCard { TrenchRoadRateCard = 6 };
            var graph = new Graph();
            var n = 0;
            for (int i = 0; i < 10; i++)
                graph.AppendNode(new GigaclearNode((++n).ToString(), GigaclearNodeType.Chamber));

            graph.AppendEdge(new GigaclearEdge(10, GigaclearEdgeType.Verge, graph.GetNodeById("1"), graph.GetNodeById("2")));
            graph.AppendEdge(new GigaclearEdge(30, GigaclearEdgeType.Verge, graph.GetNodeById("1"), graph.GetNodeById("3")));
            graph.AppendEdge(new GigaclearEdge(30, GigaclearEdgeType.Road, graph.GetNodeById("1"), graph.GetNodeById("4")));

            var cost = graph.FindCost(rateCard);

            Assert.That(cost, Is.EqualTo(180));
        }

        [Test]
        public void SumOfCostsVerge_Edge()
        {
            var rateCard = new RateCard { TrenchVergeRateCard = 8 };
            var graph = new Graph();
            var n = 0;
            for (int i = 0; i < 10; i++)
                graph.AppendNode(new GigaclearNode((++n).ToString(), GigaclearNodeType.Chamber));

            graph.AppendEdge(new GigaclearEdge(10, GigaclearEdgeType.Verge, graph.GetNodeById("1"), graph.GetNodeById("2") ));
            graph.AppendEdge(new GigaclearEdge(30, GigaclearEdgeType.Verge,graph.GetNodeById("1"), graph.GetNodeById("3")));
            graph.AppendEdge(new GigaclearEdge(30, GigaclearEdgeType.Road, graph.GetNodeById("1"), graph.GetNodeById("4")));

            var cost = graph.FindCost(rateCard);

            Assert.That(cost, Is.EqualTo(320));
        }

       

        [TestCase("B", 70)]
        [TestCase("C", 200)]
        [TestCase("D", 350)]
        public void SumOfDistanceToCabinet_Node(string nodeId, int expectedDistance)
        {
            //var graph = ReadDotFileHelper("A [type=Cabinet];B [type=Pot];C [type=Pot];D [type=Pot];E [type=Chamber];F [type=Chamber];G [type=Chamber];A -- E  [length=50, material=verge];B -- E  [length=20, material=verge];C -- F  [length=50, material=road];D -- G  [length=100, material=road];E -- F  [length=100, material=road];F -- G  [length=100, material=verge];");
            var graph = ReadDotFileHelper("A [type=Cabinet];B [type=Pot];C [type=Pot];D [type=Pot];E [type=Chamber];F [type=Chamber];G [type=Chamber];A -- E  [length=50, material=verge];B -- E  [length=20, material=verge];C -- F  [length=50, material=road];D -- G  [length=100, material=road];E -- F  [length=100, material=road];F -- G  [length=100, material=verge];");

            var distance = graph.LengthOfCabinetFromNode(graph.GetNodeById(nodeId));

            Assert.That(distance, Is.EqualTo(expectedDistance));
        }

        private Graph ReadDotFileHelper(string dotFileLines)
        {
            string contents = $"strict graph \"\" {{\r\n{dotFileLines}\r\n}}\r\n";
            string filename = Path.GetTempFileName();
            File.WriteAllText(filename, contents);
            return Graph.ReadDotFile(filename);
        }
    }
}
