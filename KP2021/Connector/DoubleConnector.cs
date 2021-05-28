using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class DoubleConnector : AConnector
    {
        public DoubleConnector(INode node) : base(node) { }
        public override ConnectorType ConnectorType => ConnectorType.Data;
        public override Color Color => System.Windows.Media.Colors.ForestGreen;
        public override int IdType => 1;
        public override Func<object> ValueGetFunction => throw new NotImplementedException();
    }
}
