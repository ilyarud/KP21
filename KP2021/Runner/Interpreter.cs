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
    class Interpreter
    {

        Contex contex;
        Model model;

        bool isStop = false;
        Mutex mutex = new Mutex();
        RunTimeInfo runTimeInfo = new RunTimeInfo();
        public int Delay { get; set; } = 2000;
        public Interpreter(Model model, Contex contex)
        {
            this.model = model;
            this.contex = contex;
        }
        private void NextNode(Chain item)
        {
            if (item.Enumerator.Current != null) item.Enumerator.Current.IsExecute = false;
            if (item.IsNotStop) item.Enumerator.MoveNext();
            if (item.Enumerator.Current != null)
            {
                item.IsNotStop = item.Enumerator.Current.Node.Execute(contex);
                item.Enumerator.Current.IsExecute = true;
            }

        }
        public void InitNode(IEnumerable<INodeViewModel> nodeViewModels)
        {
            runTimeInfo = new RunTimeInfo();
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
            List<Chain> chainsEx = model.Chains.ToList();
            IList<Chain> chainsDelete = new List<Chain>();
            while (chainsEx.Count != 0)
            {
                foreach (var item in chainsEx)
                {
                    NextNode(item);
                    if (item.Enumerator.Current == null)  chainsDelete.Add(item);
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
                runTimeInfo.NumberCicle += 1;
            }
            if (model.EndChain != null)
            {
                do
                {
                    NextNode(model.EndChain);
                    var t = Task.Delay(Delay);
                    t.Wait();
                    if (isStop) return;
                    mutex.WaitOne();
                    mutex.ReleaseMutex();
                    runTimeInfo.NumberCicle += 1;
                } while (model.EndChain.Enumerator.Current != null);
            }

        }
        public void Refresh()
        {
            foreach (var chain in model.Chains)
            {
                foreach (var node in chain.NodeViewModels)
                {
                    node.IsExecute = false;
                }
            }
        }



            
    }
}
