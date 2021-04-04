using System;
using System.Collections.Generic;
using System.Text;
using KP2021MathProcessor.ViewModel.Node;
using KP2021MathProcessor.ViewModel;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace KP2021MathProcessor.Runner
{
    class Chain
    {
        public INodeViewModel StartNode { get; set; }
        public IList<INodeViewModel> NodeViewModels { get; set; } = new List<INodeViewModel>();
        public IEnumerator<INodeViewModel> Enumerator { get; set; }
        public bool isNotStop { get; set; } = true;
    }
    class Interpreter
    {

        Contex contex;
        IList<Chain> Chains { get; } = new List<Chain>();
        IEnumerable<ConnectionViewModel> connectionViewModels;
        bool isStop = false;
        Mutex mutex = new Mutex();
        RunTimeInfo runTimeInfo = new RunTimeInfo();
        public int Delay { get; set; } = 2000;
        public Interpreter(IEnumerable<INodeViewModel> nodeViewModels, IEnumerable<ConnectionViewModel> connectionViewModels, Contex contex)
        {
            this.connectionViewModels = connectionViewModels;
            this.contex = contex;
            foreach (var item in nodeViewModels)
            {
                buildArg(item);
                if (item.Node.GetType() == typeof(Node.StartTheard))
                {
                    var chain = new Chain() { StartNode = item };
                    BuildChain(chain, connectionViewModels);
                    chain.Enumerator = chain.NodeViewModels.GetEnumerator();
                    Chains.Add(chain);
                }
            }
        }
        void buildArg(INodeViewModel nodeViewModel)
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
        public void InitNode(IEnumerable<INodeViewModel> nodeViewModels)
        {
            foreach (var node in nodeViewModels) node.Node.Initialize(runTimeInfo);
        }

        public void Stop()
        {
            isStop = true;
        }
        public void Pause()
        {
            mutex.WaitOne();
        }
        public void Res()
        {
            mutex.ReleaseMutex();
        }
        public void Start()
        {
            List<Chain> chainsEx = Chains.ToList();
            IList<Chain> chainsDelete = new List<Chain>();
            while (chainsEx.Count != 0)
            {
                foreach (var item in chainsEx)
                {                   
                    if (item.Enumerator.Current != null) item.Enumerator.Current.IsExecute = false;
                    if (item.isNotStop) item.Enumerator.MoveNext();
                    if (item.Enumerator.Current != null)
                    {
                        item.isNotStop = item.Enumerator.Current.Node.Execute(contex);
                        item.Enumerator.Current.IsExecute = true;
                    }
                    else chainsDelete.Add(item);                 
                }
                chainsEx.RemoveAll((x) => 
                {
                    foreach (var item in chainsDelete)
                    {
                        if (item == x) return true;
                    }
                    return false;
                });
                var t = Task.Delay(Delay);
                t.Wait();
                if (isStop) return;
                mutex.WaitOne();
                mutex.ReleaseMutex();
            }
        }
        public void Refresh()
        {
            foreach (var chain in Chains)
            {
                foreach (var node in chain.NodeViewModels)
                {
                    node.IsExecute = false;
                }
            }
        }

        bool BuildChain(Chain chain, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            var currentNode = chain.StartNode;
            bool flag = false;
            do
            {
                chain.NodeViewModels.Add(currentNode);
                currentNode = genNextNode(currentNode, connectionViewModels);
                flag = currentNode != null ? true : false;
            } while (flag);
            return true;
        }
        INodeViewModel genNextNode(INodeViewModel nodeViewModel, IEnumerable<ConnectionViewModel> connectionViewModels)
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
