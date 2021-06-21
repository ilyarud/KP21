using KP2021MathProcessor.Node;
using System;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class MemoryConnector : AConnector
    {
        public MemoryConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue; }
        public override ConnectorType ConnectorType => ConnectorType.Data;
        public override Color Color => System.Windows.Media.Colors.DodgerBlue;
        public override int IdType => 6;

        private Func<object> getValue;

        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
