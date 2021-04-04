using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Connector
{
    class UniConnector : AConnector
    {
        public UniConnector(INode node) : base(node) { }
        public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override ConnectorType ConnectorType => throw new NotImplementedException();

        public override ConnectorMetadata ConnectorMetadata => throw new NotImplementedException();

        public override Func<object> ValueGetFunction => throw new NotImplementedException();
    }
}
