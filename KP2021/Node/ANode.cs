using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KP2021MathProcessor.Node
{
    abstract class ANode : INode
    {
        private List<IConnector> inputConnectors = new List<IConnector>();
        private List<IConnector> outputConnectors = new List<IConnector>();
        private Point location;
        int countId = 0;
        public abstract string Header { get; }

        public virtual int TimeExec { get; } = 0;

        public IEnumerable<IConnector> InputConnectors => inputConnectors;
        public IEnumerable<IConnector> OutputConnectors => outputConnectors;

        public abstract bool IsExecuted { get; }
        
        public Point Location { get => location; set => location = value; }

        public virtual object Props { get; set; } = null;

        public int Id { get; set; }
        public RunTimeInfo RunTimeInfo {get; private set;}

        public virtual Type TypePropertys => typeof(object);

        protected void AddInputConnector(IConnector connector)
        {
            countId++;
            connector.SetId(countId);
            inputConnectors.Add(connector);    
        }
        protected void AddOutputConnector(IConnector connector)
        {
            countId++;
            connector.SetId(countId);
            outputConnectors.Add(connector);
        }
        public virtual bool Execute(Contex contex)
        {
            RunTimeInfo.Time += TimeExecCalculate(10, 100);
            return true;
        }

        protected int TimeExecCalculate(int min, int max)
        {
            Random random = new Random(DateTime.Now.Millisecond);
            return random.Next(min, max);
        }
        public virtual void Initialize()
        {

        }
        public virtual void Initialize(Runner.RunTimeInfo runTimeInfo)
        {
            RunTimeInfo = runTimeInfo;
            Initialize();
        }
    }
}
