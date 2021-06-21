using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Заблокировать")]
    class LockNode : ANode
    {
        private MutexConnector mutexConnector;
        public LockNode()
        {
            AddInputConnector(new FlowConnector(this));
            mutexConnector = new MutexConnector(this, null) { Name = "Мьютекс" };
            AddInputConnector(mutexConnector);
            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Заблокировать";

        public override bool IsExecuted { get => true; }

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            var mutex = (MutexNode)mutexConnector.GetValue();
            return mutex.Lock();
        }
    }
}
