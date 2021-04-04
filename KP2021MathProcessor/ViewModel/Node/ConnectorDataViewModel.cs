using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using KP2021MathProcessor.Connector;
using MVVM;

namespace KP2021MathProcessor.ViewModel.Node
{
    class ConnectorDataViewModel : ObservableObject, IConnectorViewModel
    {
        public ConnectorDataViewModel(IConnector connector, INodeViewModel node)
        {
            this.connector = connector;
            this.node = node;
        }
        INodeViewModel node;
        IConnector connector;
        public string Header => connector.Name;

        public Color Color => connector.ConnectorMetadata.Color;

        public int TypeID => connector.ConnectorMetadata.IdType;

        public bool IsInput { get; set; }
        private Point point;
        public Point Anchor { get => point; set => SetProperty(ref point, value); }

        bool isConnect;
        public bool IsConnect { get => isConnect; set => SetProperty(ref isConnect, value); }

        public INodeViewModel Node => node;

        public IConnector Connector => connector;
    }
}
