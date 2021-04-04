using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using KP2021MathProcessor.ViewModel.Node;
using KP2021MathProcessor.Connector;

namespace KP2021MathProcessor.ViewModel
{
    class ConnectionViewModel
    {
        public IConnectorViewModel Input { get; set; }
        public IConnectorViewModel Output { get; set; }
        public Color Color { get => Output.Color; }
        public Connection Connection { get; set; }
    }
}
