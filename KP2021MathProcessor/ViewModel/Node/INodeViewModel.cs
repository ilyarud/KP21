using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.ViewModel.Node
{
    interface INodeViewModel
    {
        string Title { get; }
        delegate void ChangeDelegate(INodeViewModel source);

        KP2021MathProcessor.Node.INode Node { get; }
        IEnumerable<IConnectorViewModel> Input { get; }
        IEnumerable<IConnectorViewModel> Output { get; }
        event ChangeDelegate Changed;
        bool IsExecute { get; set; }
        bool IsKnot { get; }
        System.Windows.Point Location { get; set; }

        bool IsSelected { get; set; }
        object Props { get; }
    }
}
