using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Connector
{
    class SimoforeConnector : AConnector
    {
        public SimoforeConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue;}
        public override ConnectorType ConnectorType => ConnectorType.Data;
        ConnectorMetadata metadata = new ConnectorMetadata() { IdType = 3, Color = System.Windows.Media.Colors.CadetBlue };
        public override ConnectorMetadata ConnectorMetadata => metadata;
        public override string Name { get; set; }
        Func<object> getValue;
  
        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
