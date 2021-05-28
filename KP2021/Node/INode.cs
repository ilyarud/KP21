using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using KP2021MathProcessor.Connector;
using System.Text.Json.Serialization;

namespace KP2021MathProcessor.Node
{
    interface INode
    {
        string Header { get; }
        IEnumerable<IConnector> InputConnectors { get; }  
        IEnumerable<IConnector> OutputConnectors { get; }
        bool IsExecuted { get;}    
        Point Location { get; set; }     
        object Props { get; set; }
        int Id { get; set; }

        bool Execute(Runner.Contex contex);
        void Initialize(Runner.RunTimeInfo runTimeInfo);

        Type TypePropertys { get;  }
    }
}
