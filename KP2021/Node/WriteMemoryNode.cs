using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Запись")]
    class WriteMemoryNode : ANode
    {
        MemoryConnector memoryConnector;
        public WriteMemoryNode()
        {

            AddInputConnector(new FlowConnector(this));
            memoryConnector = new MemoryConnector(this, null) { Name = "Область памяти" };
            AddInputConnector(memoryConnector);

            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Запись";

        public override bool IsExecuted => true;

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            var resource = (MemoryNode)memoryConnector.GetValue();
            resource.Write();
            return true;
        }
    }
}
