using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System.Collections.Generic;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Выход из секции")]
    class ReleaseNode : ANode
    {
        private SimoforeConnector simoforeConnector;
        public ReleaseNode()
        {
            simoforeConnector = new SimoforeConnector(this, null) { Name = "Семафор" };

            AddInputConnector(new FlowConnector(this));
            AddInputConnector(simoforeConnector);
            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Выход из секции";

        public override bool IsExecuted { get => true; }

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            var simofore = (SimoforeNode)simoforeConnector.GetValue();
            simofore.Release();
            return true;
        }
    }

}
