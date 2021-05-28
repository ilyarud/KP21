using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class SimoforeConnector : AConnector
    {
        public SimoforeConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue;}
        public override ConnectorType ConnectorType => ConnectorType.Data;

        public override Color Color => System.Windows.Media.Colors.CadetBlue;
        public override int IdType => 3;

        Func<object> getValue;
  
        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
