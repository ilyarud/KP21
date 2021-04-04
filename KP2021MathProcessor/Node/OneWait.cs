using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System.Collections.Generic;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Вход в секцию")]
    class OneWait : ANode
    {
        SimoforeConnector simoforeConnector;
        public OneWait()
        {
            
            AddInputConnector(new FlowConnector(this));
            simoforeConnector = new SimoforeConnector(this, null) { Name = "Семафор" };
            AddInputConnector(simoforeConnector);

            AddOutputConnector(new FlowConnector(this));
        }
        public override string Name => "Вход в секцию";

        public override bool IsExecuted => true; 

        public override bool Execute(Contex contex)
        {
            var simofore = (Simofore)simoforeConnector.GetValue();
            return simofore.OneWait();
        }
    }

}
