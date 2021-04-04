using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Connector
{
    class StringConnector : AConnector
    {
        public StringConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue;}
        public override ConnectorType ConnectorType => ConnectorType.Data;
        ConnectorMetadata metadata = new ConnectorMetadata() { IdType = 2, Color = System.Windows.Media.Colors.IndianRed};
        public override ConnectorMetadata ConnectorMetadata => metadata;
        public override string Name { get; set; }

        Func<object> getValue;
        Action<object> setValue;
        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
