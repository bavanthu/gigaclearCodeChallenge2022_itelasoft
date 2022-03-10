using System;
using System.Collections.Generic;
using System.Text;

namespace Gigaclear_code_challenge
{
    public struct GigaclearEdge
    {
        public int Length { get; }
        public GigaclearEdgeType Type { get; }
        public GigaclearNode StartNode { get; }
        public GigaclearNode EndNode { get; }



        public GigaclearEdge(int length, GigaclearEdgeType type, GigaclearNode startNode, GigaclearNode endNode )
        {
            if (startNode.Equals(endNode))
                throw new ArgumentException("Both can't be same");
            Length = length;
            Type = type;
            StartNode = startNode;
            EndNode = endNode;
        }
    }
}
