using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Connector
{
    class FlowConnector : AConnector
    {
        public FlowConnector(INode node) : base(node) { }
        public override ConnectorType ConnectorType => ConnectorType.Flow;

        ConnectorMetadata metadata = new ConnectorMetadata() { IdType = 0, Color = System.Windows.Media.Colors.AliceBlue };
        public override ConnectorMetadata ConnectorMetadata => metadata;
        public override string Name { get; set; }

        public override Func<object> ValueGetFunction => throw new NotImplementedException();
    }
}
