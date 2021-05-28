using KP2021MathProcessor.Node;
using System;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    class MutexConnector : AConnector
    {
        public MutexConnector(INode node, Func<object> getValue) : base(node) { this.getValue = getValue; }
        public override ConnectorType ConnectorType => ConnectorType.Data;
        public override Color Color => System.Windows.Media.Colors.BlueViolet;
        public override int IdType => 7;
        Func<object> getValue;

        public override Func<object> ValueGetFunction { get => getValue; }
    }
}
