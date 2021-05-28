using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class UniConnector : AConnector
    {
        public UniConnector(INode node) : base(node) { }

        public override ConnectorType ConnectorType => throw new NotImplementedException();


        public override Func<object> ValueGetFunction => throw new NotImplementedException();

        public override int IdType => throw new NotImplementedException();

        public override Color Color => throw new NotImplementedException();
    }
}
