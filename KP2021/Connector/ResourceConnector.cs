using KP2021MathProcessor.Node;
using System;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class ResourceConnector : AConnector
    {
        public ResourceConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue; }
        public override ConnectorType ConnectorType => ConnectorType.Data;
        public override Color Color => System.Windows.Media.Colors.LimeGreen;
        public override int IdType => 5;
        private Func<object> getValue;

        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
