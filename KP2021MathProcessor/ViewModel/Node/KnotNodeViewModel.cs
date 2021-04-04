using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using MVVM;

namespace KP2021MathProcessor.ViewModel.Node
{
    class KnotNodeViewModel : ObservableObject, INodeViewModel
    {
        KnotNode node;
        public KnotNodeViewModel(KnotNode node)
        {
            this.node = node;
            Connector = new UniversalConnector(this);
            connectorViewModels = new List<IConnectorViewModel>();
            connectorViewModels.Add(Connector);
        }
        public string Title => throw new NotImplementedException();

        public INode Node => node;
        public IConnectorViewModel Connector { get; set; }
        List<IConnectorViewModel> connectorViewModels;

        public IEnumerable<IConnectorViewModel> Input => connectorViewModels;

        public IEnumerable<IConnectorViewModel> Output => connectorViewModels;

        public bool IsExecute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Location
        {
            get => node.Location; set
            {
                if (!Equals(node.Location, value))
                {
                    node.Location = value;
                    IsDirty = true;
                    OnPropertyChanged("Location");
                }
            }
        }
        public bool IsSelected { get; set; }

        public object Props => null;

        public bool IsKnot => true;

        public event INodeViewModel.ChangeDelegate Changed;
    }
}
