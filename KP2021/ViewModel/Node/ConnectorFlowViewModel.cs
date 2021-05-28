using KP2021MathProcessor.Connector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using MVVM;

namespace KP2021MathProcessor.ViewModel.Node 
{
    class ConnectorFlowViewModel : ObservableObject, IConnectorViewModel
    {
        public ConnectorFlowViewModel(IConnector connector, INodeViewModel node)
        {
            this.connector = connector;
            this.node = node;
        }
        INodeViewModel node;
        IConnector connector;
        public string Header => connector.Name;

        public Color Color => connector.Color;

        public int TypeID => connector.IdType;

        public bool IsInput { get; set; }

        private Point point;
        public Point Anchor { get => point;
            set => SetProperty(ref point, value); }

        bool isConected;
        public bool IsConnect { get => isConected; set => SetProperty(ref isConected, value); }

        public INodeViewModel Node => node;
        public IConnector Connector => connector;
    }
}
