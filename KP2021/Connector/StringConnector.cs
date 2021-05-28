using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class StringConnector : AConnector
    {
        public StringConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue;}
        public override ConnectorType ConnectorType => ConnectorType.Data;
        public override Color Color => System.Windows.Media.Colors.IndianRed;
        public override int IdType => 2;

        Func<object> getValue;
        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
