using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Задержка")]
    class DelayNode : ANode
    {
        public DelayNode()
        {
            AddInputConnector(new FlowConnector(this));
            AddOutputConnector(new FlowConnector(this));
        }
        class DelayRandomData
        {
            public int Min { get; set; } = 10;
            public int Max { get; set; } = 100;
        }
        private DelayRandomData data = new DelayRandomData();
        public override object Props { get => data; set => data = (DelayRandomData)value; }
        public override Type TypePropertys => typeof(DelayRandomData);

        public override string Header => "Задержка";

        public override bool IsExecuted => false;

        public override bool Execute(Contex contex)
        {
            RunTimeInfo.Time += TimeExecCalculate(data.Min, data.Max);
            return true;
        }
    }
}
