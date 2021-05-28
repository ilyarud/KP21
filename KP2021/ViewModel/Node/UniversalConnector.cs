using KP2021MathProcessor.Connector;
using MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Linq;

namespace KP2021MathProcessor.ViewModel.Node
{
    class UniversalConnector : ObservableObject, IConnectorViewModel
    {
        public UniversalConnector(KnotNodeViewModel knotNodeViewModel)
        {
            node = knotNodeViewModel;
            connector = node.Node.InputConnectors.ElementAt(0);          
        }
        public string Header => throw new NotImplementedException();

        private Color color = Colors.Gray;
        public Color Color { get => color; set => SetProperty(ref color, value); }

        public int TypeID {get; set;}
        public bool IsInput { get; set; }
        private Point point;
        public Point Anchor { get => point; set => SetProperty(ref point, value); }

        bool isConnect;
        private INodeViewModel node;
        private IConnector connector;

        public bool IsConnect { get => isConnect; set
            {
                if (!value) Color = Colors.Gray;
                SetProperty(ref isConnect, value);
            } }

        public INodeViewModel Node => node;
        public IConnector Connector => connector;

    }
}
