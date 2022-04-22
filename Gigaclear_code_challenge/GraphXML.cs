/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Gigaclear_code_challenge
{
	

	[XmlRoot(ElementName = "data", Namespace = "http://graphml.graphdrawing.org/xmlns")]
	public class Data
	{
		[XmlAttribute(AttributeName = "key")]
		public string Key { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "node", Namespace = "http://graphml.graphdrawing.org/xmlns")]
	public class Node
	{
		[XmlElement(ElementName = "data", Namespace = "http://graphml.graphdrawing.org/xmlns")]
		public Data Data { get; set; }
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
	}

	[XmlRoot(ElementName = "edge", Namespace = "http://graphml.graphdrawing.org/xmlns")]
	public class Edge
	{
		[XmlElement(ElementName = "data", Namespace = "http://graphml.graphdrawing.org/xmlns")]
		public List<Data> Data { get; set; }
		[XmlAttribute(AttributeName = "source")]
		public string Source { get; set; }
		[XmlAttribute(AttributeName = "target")]
		public string Target { get; set; }
	}

	[XmlRoot(ElementName = "graph", Namespace = "http://graphml.graphdrawing.org/xmlns")]
	public class Graph
	{
		[XmlElement(ElementName = "node", Namespace = "http://graphml.graphdrawing.org/xmlns")]
		public List<Node> Node { get; set; }
		[XmlElement(ElementName = "edge", Namespace = "http://graphml.graphdrawing.org/xmlns")]
		public List<Edge> Edge { get; set; }
		[XmlAttribute(AttributeName = "edgedefault")]
		public string Edgedefault { get; set; }
	}

	[XmlRoot(ElementName = "graphml", Namespace = "http://graphml.graphdrawing.org/xmlns")]
	public class Graphml
	{
		
		[XmlElement(ElementName = "graph", Namespace = "http://graphml.graphdrawing.org/xmlns")]
		public Graph Graph { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
		[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
		[XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
		public string SchemaLocation { get; set; }
	}



}
