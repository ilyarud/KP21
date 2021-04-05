using KP2021MathProcessor.Node;
using System;

namespace KP2021MathProcessor.Connector
{
    class IntegerConnector : AConnector
    {
        public IntegerConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue; }
        public override ConnectorType ConnectorType => ConnectorType.Data;
        ConnectorMetadata metadata = new ConnectorMetadata() { IdType = 4, Color = System.Windows.Media.Colors.MediumAquamarine };
        public override ConnectorMetadata ConnectorMetadata => metadata;
        public override string Name { get; set; }

        Func<object> getValue;
        Action<object> setValue;
        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
