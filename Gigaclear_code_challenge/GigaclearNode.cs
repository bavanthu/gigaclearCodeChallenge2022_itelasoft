using System;
using System.Collections.Generic;
using System.Text;

namespace Gigaclear_code_challenge
{
    public struct GigaclearNode
    {
        public GigaclearNodeType Type { get; }

        public string Id { get; }

            public GigaclearNode(string id, GigaclearNodeType type)
            {
                 Type = type;

                 Id = id;
            }
        
    }
}
