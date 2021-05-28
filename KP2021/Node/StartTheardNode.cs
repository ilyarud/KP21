using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;
using System.Collections.Generic;
using System.Text;


namespace KP2021MathProcessor.Node
{
    [NodeInfo("Старт потока")]
    class StartTheardNode : ANode
    {
        public StartTheardNode()
        {
            AddOutputConnector(new FlowConnector(this));
        }
        public override string Header => "Старт потока";

        public override bool IsExecuted { get => true; }

        public override bool Execute(Contex contex)
        {
            base.Execute(contex);
            return true;
        }
    }
}
