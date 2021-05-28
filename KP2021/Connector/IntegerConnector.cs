using KP2021MathProcessor.Node;
using System;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class IntegerConnector : AConnector
    {
        public IntegerConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue; }
        public override ConnectorType ConnectorType => ConnectorType.Data;
        public override Color Color => System.Windows.Media.Colors.MediumAquamarine;
        public override int IdType => 4;

        Func<object> getValue;
        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
