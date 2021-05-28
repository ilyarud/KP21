using System.Collections.Generic;
using KP2021MathProcessor.ViewModel.Node;

namespace KP2021MathProcessor.Runner
{
    class Chain
    {
        public INodeViewModel StartNode { get; set; }
        public IList<INodeViewModel> NodeViewModels { get; set; } = new List<INodeViewModel>();
        public IEnumerator<INodeViewModel> Enumerator { get; set; }
        public bool IsNotStop { get; set; } = true;
    }
}
