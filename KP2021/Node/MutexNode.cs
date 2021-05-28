using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Мьютекс")]
    class MutexNode : ANode
    {
       

        bool isLock;

        public override string Header => "Мьютекс";
        

        public override bool IsExecuted { get => false; }
        public MutexNode()
        {
            AddOutputConnector(new MutexConnector(this, () => this) { Name = "" });
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public void Unlock()
        {           
            isLock = false;
        }
        public bool Lock()
        {
            if (isLock)
            {
                return false;
            }
            isLock = true;
            return true;
        }
    }
}
