using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Connector
{
    class DoubleConnector : AConnector
    {
        public DoubleConnector(INode node) : base(node) { }
        public override ConnectorType ConnectorType => ConnectorType.Data;
        ConnectorMetadata metadata = new ConnectorMetadata() { IdType = 1, Color = System.Windows.Media.Colors.ForestGreen };
        public override ConnectorMetadata ConnectorMetadata => metadata;
        public override string Name { get; set; }
        public override Func<object> ValueGetFunction => throw new NotImplementedException();
    }
}
