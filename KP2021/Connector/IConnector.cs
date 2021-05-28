using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace KP2021MathProcessor.Connector
{
    enum ConnectorType
    {
        Data,
        Flow
    }
    interface IConnector
    {

        int ID { get; }
        string Name { get;  }
        ConnectorType ConnectorType { get; }
        INode Node { get; }
        Func<object> ValueGetFunction { get; }
        Func<object> GetValue { get; set; }
        public int IdType { get; }
        public Color Color { get; }

        void SetId(int id);
    }
}
