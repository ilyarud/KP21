using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Удлинитель")]
    class KnotNode : INode
    {
        public KnotNode()
        {
            var con = new UniConnector(this);
            con.SetId(1);
            connectors = new List<IConnector>();
            connectors.Add(con);
        }
        public string Header => throw new NotImplementedException();
        private List<IConnector> connectors;
        public IEnumerable<IConnector> InputConnectors => connectors;

        public IEnumerable<IConnector> OutputConnectors => connectors;

        public bool IsExecuted => throw new NotImplementedException();

        public Point Location { get; set; }
        public object Props { get => null; set => throw new NotImplementedException(); }
        public int Id { get; set; }

        public Type TypePropertys => typeof(object);

        public bool Execute(Contex contex)
        {
            throw new NotImplementedException();
        }

        public void Initialize(RunTimeInfo runTimeInfo)
        {
           
        }
    }
}
