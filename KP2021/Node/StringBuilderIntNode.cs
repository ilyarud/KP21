using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Формат строки(integer)")]
    class StringBuilderIntNode : ANode
    {
        class StringBuilderData
        {
            public string Text
            {
                get;
                set;
            }

        }
        private IntegerConnector integerConnector;
        public StringBuilderIntNode()
        {
            integerConnector = new IntegerConnector(this, null) { Name = "Данные(int)" };
            AddInputConnector(integerConnector);
            AddOutputConnector(new StringConnector(this, () => String.Format(sd.Text, integerConnector.GetValue())) { Name = "Вывод" });
        }
        public override string Header => "Формат строки(integer)";

        private StringBuilderData sd = new StringBuilderData();
        public override object Props { get => sd; set => sd = (StringBuilderData)value; }

        public override bool IsExecuted { get => false; }

        public override Type TypePropertys => typeof(StringBuilderData);
    }
}
