using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Время со старта(мс)")]
    class TimeForStartNode : ANode
    {
        public TimeForStartNode()
        {
            AddOutputConnector(new IntegerConnector(this, () => RunTimeInfo.Time) { Name = "" });
        }
        public override string Header => "Время со старта(мс)";

        public override bool IsExecuted { get => false; }
    }
}
