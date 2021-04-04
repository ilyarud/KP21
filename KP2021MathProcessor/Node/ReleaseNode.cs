using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System.Collections.Generic;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Выход из секции")]
    class ReleaseNode : ANode
    {
        SimoforeConnector simoforeConnector;
        public ReleaseNode()
        {
            AddInputConnector(new FlowConnector(this));
            simoforeConnector = new SimoforeConnector(this, null) { Name = "Семафор" };
            AddInputConnector(simoforeConnector);
            AddOutputConnector(new FlowConnector(this));
        }
        public override string Name => "Выход из секции";

        public override bool IsExecuted { get => true; }

        public override bool Execute(Contex contex)
        {
            var simofore = (Simofore)simoforeConnector.GetValue();
            simofore.Release();
            return true;
        }
    }

}
