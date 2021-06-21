using KP2021MathProcessor.ViewModel;
using KP2021MathProcessor.ViewModel.Node;
using System.Collections.Generic;

namespace KP2021MathProcessor.Runner
{
    class Model
    {
        public Model(IList<Chain> chains, Chain endChain)
        {
            Chains = chains;
            EndChain = endChain;
        }
        public IList<Chain> Chains { get; private set; }
        public Chain EndChain { get; private set; }
    }
}
