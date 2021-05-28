using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Чтение")]
    class ReadMemoryNode : ANode
    {
        MemoryConnector memoryConnector;
        public ReadMemoryNode()
        {

            AddInputConnector(new FlowConnector(this));
            memoryConnector = new MemoryConnector(this, null) { Name = "Область памяти" };
            AddInputConnector(memoryConnector);

            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Чтение";

        public override bool IsExecuted => true;

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            var resource = (MemoryNode)memoryConnector.GetValue();
            resource.Read();
            return true;
        }
    }
}
