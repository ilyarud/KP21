using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class NodeInfoAttribute : Attribute
    {
        private readonly string nameNode;

        public NodeInfoAttribute(string nameNode)
        {
            this.nameNode = nameNode;

        }
        public string Category { get; set; }
        public string NameNode
        {
            get { return nameNode; }
        }

    }
}
