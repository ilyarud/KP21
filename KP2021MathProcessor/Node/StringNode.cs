using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;
using System.Collections.Generic;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Строка")]
    class StringNode : ANode
    {
        class StringData
        {
            public string Data { get;
                set; }

        }
        public StringNode()
        {
            AddOutputConnector(new StringConnector(this, () => sd.Data) { Name = "" });
        }
        public override string Name => "Строка";

        StringData sd = new StringData();
        public override object Props { get => sd; set => sd = (StringData)value; }

        public override bool IsExecuted { get => false; }

        public override bool Execute(Contex contex)
        {
            throw new System.NotImplementedException();
        }

        public override Type TypePropertys => typeof(StringData);
    }
}
