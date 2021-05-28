using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Номер цикла")]
    class CicleNode : ANode
    {
        public CicleNode()
        {
            AddOutputConnector(new IntegerConnector(this, () => RunTimeInfo.NumberCicle) { Name = "" });
        }
        public override string Header => "Номер цикла";    

        public override bool IsExecuted { get => false; }

   
    }

}
