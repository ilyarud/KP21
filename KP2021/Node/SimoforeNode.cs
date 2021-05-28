using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Семафор")]
    class SimoforeNode : ANode
    {
        class SimoforeData
        {
            public int MaxCount {
                get; 
                set; }
            public int InitCount
            {
                get;
                set;
            }
        }

        int count = 0;

        public override string Header => "Семафор";
        public override Type TypePropertys => typeof(SimoforeData);

        SimoforeData sd = new SimoforeData();
        public override object Props { get => sd; set => 
                sd = (SimoforeData)value; }
        public override bool IsExecuted { get => false; }
        public SimoforeNode()
        {
            AddOutputConnector(new SimoforeConnector(this, () => this) { Name = "" });
        }
        public override void Initialize()
        {
            base.Initialize();
            count = sd.InitCount;
        }
        public void Release()
        {
            count++;
            if (count > sd.MaxCount)
            {
                throw new SemaphoreFullException();
            }
        }
        public bool OneWait()
        {
            if (count <= 0)
            {
                return false;
            }
            count--;
            return true;
        }
    }
}
