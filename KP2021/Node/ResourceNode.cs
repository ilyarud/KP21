using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Ресурс")]
    class ResourceNode : ANode
    {
        class ResourceData
        {
            public int num
            {
                get;
                set;
            } = 1;
        }
        public ResourceNode()
        {
            AddOutputConnector(new ResourceConnector(this, () => this) { Name = "" });
        }
        public override string Header => "Ресурс";
        ResourceData sd = new ResourceData();
        public override object Props { get => sd; set => sd = (ResourceData)value; }
        public override bool IsExecuted { get => false; }
        int count;
        int maxCount;
        int cicle = int.MaxValue;
        public override bool Execute(Contex contex)
        {
            throw new System.NotImplementedException();
        }
        public override void Initialize()
        {
            base.Initialize();
            if (sd.num < 0) throw new ArgumentException("Значение должно быть больше 0");
            count = maxCount = sd.num;
            cicle = int.MaxValue;
        }
        public void TestGet()
        {
            if (RunTimeInfo.NumberCicle != cicle)
            {
                cicle = RunTimeInfo.NumberCicle;
                count = maxCount;
                count--;
            }
            else
            {
                if (count == 0) throw new Exception("Ошибка обращения к ресурсу");
                count--;
                
            }
        }
        public override Type TypePropertys => typeof(ResourceData);
    }
}
