using KP2021MathProcessor.ViewModel;
using KP2021MathProcessor.ViewModel.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KP2021MathProcessor.Runner
{
    class Builder
    {
        public static Model Build(IEnumerable<INodeViewModel> nodeViewModels, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            IList<Chain> chains = new List<Chain>();
            Chain endChain = null;
            foreach (var item in nodeViewModels)
            {
                BuildArg(item, connectionViewModels);
                if (item.Node.GetType() == typeof(Node.StartTheardNode))
                {
                    var chain = new Chain() { StartNode = item };
                    BuildChain(chain, connectionViewModels);
                    chain.Enumerator = chain.NodeViewModels.GetEnumerator();
                    chains.Add(chain);
                }
                if (item.Node.GetType() == typeof(Node.EndNode))
                {
                    if (endChain != null) throw new Exception("Завершающая нода может быть только одна");
                    endChain = new Chain() { StartNode = item };
                    BuildChain(endChain, connectionViewModels);
                    endChain.Enumerator = endChain.NodeViewModels.GetEnumerator();
                }
            }
            return new Model(chains, endChain);
        }
        static bool BuildChain(Chain chain, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            var currentNode = chain.StartNode;
            bool flag = false;
            do
            {
                chain.NodeViewModels.Add(currentNode);
                currentNode = GenNextNode(currentNode, connectionViewModels);
                flag = currentNode != null ? true : false;
            } while (flag);
            return true;
        }
        static void BuildArg(INodeViewModel nodeViewModel, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            foreach (var item in nodeViewModel.Input)
            {
                if (item.Connector.ConnectorType != Connector.ConnectorType.Flow)
                {
                    var con = connectionViewModels.FirstOrDefault((x) => x.Input == item);
                    item.Connector.GetValue = con.Output.Connector.ValueGetFunction;
                }
            }
        }
        static INodeViewModel GenNextNode(INodeViewModel nodeViewModel, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            foreach (var item in connectionViewModels)
            {
                if (item.Output.Node == nodeViewModel)
                {
                    return item.Input.Node;
                }
            }
            return null;
        }
    }
}
