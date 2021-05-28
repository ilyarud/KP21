using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System.Collections.Generic;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Вход в секцию")]
    class OneWaitNode : ANode
    {
        SimoforeConnector simoforeConnector;
        public OneWaitNode()
        {
            
            AddInputConnector(new FlowConnector(this));
            simoforeConnector = new SimoforeConnector(this, null) { Name = "Семафор" };
            AddInputConnector(simoforeConnector);

            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Вход в секцию";

        public override bool IsExecuted => true; 

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            var simofore = (SimoforeNode)simoforeConnector.GetValue();
            return simofore.OneWait();
        }
    }
}
