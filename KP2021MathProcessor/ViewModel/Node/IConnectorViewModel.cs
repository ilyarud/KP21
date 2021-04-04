using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using KP2021MathProcessor.Connector;
using Nodify;

namespace KP2021MathProcessor.ViewModel.Node
{
    interface IConnectorViewModel
    {
        string Header { get; }
        Color Color { get; }
        int TypeID { get; }   
        bool IsInput { get; set; }
        bool IsConnect { get; set; }    
        Point Anchor { get; set; }
        INodeViewModel Node { get; }
        IConnector Connector { get; }
    }
}
