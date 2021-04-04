using KP2021MathProcessor.ViewModel.Node;
using System.Windows;

namespace KP2021MathProcessor.ViewModel
{
    class CreateNodeInfoViewModel
    {
        public CreateNodeInfoViewModel(NodeContexInfo info, Point location)
        {
            Info = info;
            Location = location;
        }

        public NodeContexInfo Info { get; }
        public Point Location { get; }
    }
}
