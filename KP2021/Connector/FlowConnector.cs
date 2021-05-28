using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class FlowConnector : AConnector
    {
        public FlowConnector(INode node) : base(node) { }
        public override ConnectorType ConnectorType => ConnectorType.Flow;
        public override Color Color => System.Windows.Media.Colors.AliceBlue;
        public override int IdType => 0;

        public override Func<object> ValueGetFunction => throw new NotImplementedException();
    }
}
