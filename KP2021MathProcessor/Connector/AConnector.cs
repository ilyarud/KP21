using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Connector
{
    abstract class AConnector : IConnector
    {
        public AConnector(INode node)
        {
            this.node = node;
        }
        INode node;
        public abstract string Name { get; set; }

        public abstract ConnectorType ConnectorType { get; }

        public abstract ConnectorMetadata ConnectorMetadata { get; }

        public INode Node => node;

        public abstract Func<object> ValueGetFunction { get; }

        public int ID { get; private set; }
        public Func<object> GetValue { get; set; }

        public void SetId(int id)
        {
            ID = id;
        }
    }
}
