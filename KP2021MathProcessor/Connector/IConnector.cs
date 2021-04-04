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
    class ConnectorMetadata
    {
        public int IdType { get; set; }
        public Color Color { get; set; }
    }
    interface IConnector
    {
        int ID { get; }
        string Name { get;  }
        ConnectorType ConnectorType { get; }
        ConnectorMetadata ConnectorMetadata { get; }
        INode Node { get; }
        Func<object> ValueGetFunction { get; }
        Func<object> GetValue { get; set; }

        void SetId(int id);
    }
}
