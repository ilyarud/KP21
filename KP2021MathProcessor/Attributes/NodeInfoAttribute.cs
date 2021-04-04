using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class NodeInfoAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string nameNode;

        // This is a positional argument
        public NodeInfoAttribute(string nameNode)
        {
            this.nameNode = nameNode;

            // TODO: Implement code here
        }

        public string NameNode
        {
            get { return nameNode; }
        }

    }
}
