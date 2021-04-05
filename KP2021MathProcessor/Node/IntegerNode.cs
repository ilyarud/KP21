using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Число(Integer)")]
    class IntegerNode : ANode
    {
        class IntData
        {
            public int Value
            {
                get;
                set;
            }

        }
        public IntegerNode()
        {
            AddOutputConnector(new IntegerConnector(this, () => sd.Value) { Name = "" });
        }
        public override string Name => "Число(Integer)";

        IntData sd = new IntData();
        public override object Props { get => sd; set => sd = (IntData)value; }

        public override bool IsExecuted { get => false; }

        public override bool Execute(Contex contex)
        {
            throw new System.NotImplementedException();
        }

        public override Type TypePropertys => typeof(IntData);
    }
}
