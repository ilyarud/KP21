using KP2021MathProcessor.Node;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using MVVM;

namespace KP2021MathProcessor.ViewModel.Node
{
    class NodeViewModel : ObservableObject, INodeViewModel
    {
        public NodeViewModel(INode node)
        {
            this.node = node;
            foreach (var item in this.node.InputConnectors) input.Add(ConnectorViewModelBuilder.Build(item, this, true));
            foreach (var item in this.node.OutputConnectors) output.Add(ConnectorViewModelBuilder.Build(item, this, false));
        }
        NodifyObservableCollection<IConnectorViewModel> input = new NodifyObservableCollection<IConnectorViewModel>();
        NodifyObservableCollection<IConnectorViewModel> output = new NodifyObservableCollection<IConnectorViewModel>();

        INode node;

        public event INodeViewModel.ChangeDelegate Changed;

        public string Title => node.Name;

        public IEnumerable<IConnectorViewModel> Input => input.AsEnumerable();

        public IEnumerable<IConnectorViewModel> Output => output.AsEnumerable();

        public INode Node => node;
        bool selected;
        public bool IsSelected { get => selected;
            set
            {
                selected = value;
                Changed(this);
            }
        }
        
        public Point Location { get => node.Location; set
            {
                if (!Equals(node.Location, value))
                {
                    node.Location = value;
                    IsDirty = true;
                    OnPropertyChanged("Location");
                }
            }
        }
        bool isExecute = false;
        public bool IsExecute { get => isExecute; set => SetProperty(ref isExecute, value); }

        public object Props => node.Props;

        public bool IsKnot => false;
    }
}
