using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Вывод")]
    class OutputConsole : ANode
    {
        StringConnector stringConnector;
        public OutputConsole()
        {
            AddInputConnector(new FlowConnector(this));
            stringConnector = new StringConnector(this, null) { Name = "Сообщение" };
            AddInputConnector(stringConnector);
            AddOutputConnector(new FlowConnector(this));
        }

        public override string Name => "Вывод";
        public override object Props => null;
        public override bool IsExecuted { get => true; }

        public override bool Execute(Contex contex)
        {
            var data = (string)stringConnector.GetValue();
            contex.PublicString(data + "\n");
            return true;
        }
    }

}
