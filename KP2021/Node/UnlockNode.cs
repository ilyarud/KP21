using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Разблокировать")]
    class UnlockNode : ANode
    {
        private MutexConnector mutexConnector;
        public UnlockNode()
        {
            AddInputConnector(new FlowConnector(this));
            mutexConnector = new MutexConnector(this, null) { Name = "Мьютекс" };
            AddInputConnector(mutexConnector);
            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Разблокировать";

        public override bool IsExecuted { get => true; }

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            var mutex = (MutexNode)mutexConnector.GetValue();
            mutex.Unlock();
            return true;
        }
    }

}
