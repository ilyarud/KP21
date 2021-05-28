using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Обращение к ресурсу")]
    class GetResourceNode : ANode
    {
        ResourceConnector resourceConnector;
        public GetResourceNode()
        {

            AddInputConnector(new FlowConnector(this));
            resourceConnector = new ResourceConnector(this, null) { Name = "Ресурс" };
            AddInputConnector(resourceConnector);

            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Обращение к ресурсу";

        public override bool IsExecuted => true;

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            var resource = (ResourceNode)resourceConnector.GetValue();
            resource.TestGet();
            return true;
        }
    }
}
