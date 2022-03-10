using System;
using System.Collections.Generic;
using System.Text;
using Gigaclear_code_challenge;
using NUnit.Framework;

namespace Gigaclear_code_challenge_unitTest
{
    public class GigaclearEdgeTests
    {
        [Test]
        public void SameNodes()
        {
            var node = new GigaclearNode("A", GigaclearNodeType.Chamber);
            Assert.Throws<ArgumentException>(() => new GigaclearEdge(1, GigaclearEdgeType.None,node, node));
        }
    }
}
