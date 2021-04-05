using System;
using System.Collections.Generic;
using System.Text;
using MVVM;
using KP2021MathProcessor.ViewModel.Node;
using System.Reflection;
using System.Linq;
using KP2021MathProcessor.Node;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows;
using KP2021MathProcessor.Attributes;
using System.Windows.Media;

namespace KP2021MathProcessor.ViewModel
{
    class EditorViewModel : ObservableObject
    {
        #region Commands
        public INodifyCommand CreateNodeCommand { get; set; }
        public INodifyCommand ConnectionCreate { get; set; }
        public INodifyCommand Disconnect { get; set; }
        public INodifyCommand DeleteSelection { get; set; }
        public INodifyCommand Execute { get; set; }
        public INodifyCommand SaveFile { get; set; }
        public INodifyCommand OpenFile { get; set; }
        public INodifyCommand NewFileCommand { get; set; }
        public INodifyCommand CopyCommand { get; set; }
        public INodifyCommand PasteCommand { get; set; }
        public INodifyCommand PauseCommand { get; set; }
        #endregion
        #region Vars
        string outText = "";
        INodeViewModel selctedNode;
        string path = null;
        bool isNotExecute = true;
        uint delayChain = 2000;
        bool isPaused = false;
        Task executTask;
        Runner.Interpreter interpreter;
        string status = "Готово";
        #endregion
        #region Property
        public uint Delay { get => delayChain; set => delayChain = value; }
        public bool IsPaused { get => isPaused; set => SetProperty(ref isPaused, value); }
        public string OutText { get => outText; set => SetProperty(ref outText, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }
        public bool IsNotExecute { get => isNotExecute; set => SetProperty(ref isNotExecute, value); }
        public object SelectedItem { get => selctedNode; set => SetProperty(ref selctedNode, (INodeViewModel)value); }
        public NodifyObservableCollection<ConnectionViewModel> Connections { get; } = new NodifyObservableCollection<ConnectionViewModel>();
        public NodifyObservableCollection<INodeViewModel> Nodes { get; } = new NodifyObservableCollection<INodeViewModel>();
        public NodifyObservableCollection<NodeContexInfo> ContextMenuItems { get; } = new NodifyObservableCollection<NodeContexInfo>();
        #endregion
        public EditorViewModel()
        {
            CreateNodeCommand = new DelegateCommand<CreateNodeInfoViewModel>(createNode);
            ConnectionCreate = new DelegateCommand<(object Source, object Target)>((x) => conCreate((IConnectorViewModel)x.Source, (IConnectorViewModel)x.Target));
            Disconnect = new DelegateCommand<IConnectorViewModel>(deleteConnection);
            DeleteSelection = new DelegateCommand(delSelection);
            Execute = new DelegateCommand(execute);
            SaveFile = new DelegateCommand(save);
            OpenFile = new DelegateCommand(open);
            NewFileCommand = new DelegateCommand(newFile);
            CopyCommand = new DelegateCommand(copy);
            PasteCommand = new DelegateCommand(paste);
            PauseCommand = new DelegateCommand(pause);

            Utilite.InitNodesList();
            foreach (var item in Utilite.NodeTypes)
            {
                string name = "";
                if (item.GetCustomAttribute<NodeInfoAttribute>() is NodeInfoAttribute attribute && attribute != null)
                    name = attribute.NameNode;
                else name = item.Name;
                ContextMenuItems.Add(new NodeContexInfo() { Type = item, Name = name });
            }
        }

        private void conCreate(IConnectorViewModel source, IConnectorViewModel target)
        {
            var connect = Utilite.ConnectionCreate(source, target);
            if (connect != null)
            {
                Connections.Add(connect);
            }
        }
        private void createNode(CreateNodeInfoViewModel info)
        {
            var node = Utilite.CreateNodeViewModel(Utilite.CreateNode(info.Info.Type), info.Location );
            node.Changed += change;
            Nodes.Add(node);
        }
        private void deleteConnection(IConnectorViewModel connector)
        {
            if (connector == null)
            {
                return;
            }
            var cons = Connections.Where((i) => i.Input == connector || i.Output == connector).ToList();
            foreach (var item in cons)
            {
                Connections.Remove(item);
                item.Input.IsConnect = false;
                if (!Connections.Any((x) => x.Output == item.Output)) item.Output.IsConnect = false;
            }          
        }
        private void delSelection()
        {
            var selected = Nodes.Where(o => o.IsSelected).ToList();
            selected.ForEach(o => {
                Nodes.Remove(o);
                o.Changed -= change;
                o.Input.ForEach(i => deleteConnection(i));
                o.Output.ForEach(i => deleteConnection(i));
                });
            
        }
        private void change(INodeViewModel source)
        {
            if (source.IsSelected)
            {
                SelectedItem = source;
            }
        }
        private void execute()
        {
            if (isNotExecute)
            {
                Status = "Сборка";
                OutText = "";
                var nodes = Utilite.GetNodesNotKnot(Nodes);
                interpreter = new Runner.Interpreter(nodes, Utilite.BuildConnection(Nodes, Connections), new Runner.Contex() { PublicString = (x) => OutText += x });
                interpreter.InitNode(nodes);
                interpreter.Delay = Convert.ToInt32(Delay);
                executTask = new Task(interpreter.Start);
                executTask.ContinueWith(
                    (x) => {
                        IsNotExecute = true;
                        Status = "Готово";
                        executeBrash(false);
                    });         
                executTask.Start();
                Status = "Выполнение";
                executeBrash(true);
                IsNotExecute = false;
            }
            else
            {
                Status = "Остановка";
                if (IsPaused) pause();
                interpreter.Stop();
                interpreter.Refresh();
            }

        }
        private void pause()
        {
            if (IsPaused)
            {
                interpreter.Res();
                IsPaused = false;
                Status = "Выполнение";
            }
            else
            {
                Status = "Пауза";
                interpreter.Pause();
                IsPaused = true;
            }
        }
        private void save()
        {
            if (path == null)
            {
                var d = new SaveFileDialog();
                d.Filter = "JSON (*json)|*.json|Все файлы (*.*)|*.*";
                if (d.ShowDialog() == true) path = d.FileName;
                else return;
            }
            Utilite.SavetoFile(Nodes, Connections, path);
        }
        private void newFile()
        {
            clear();
            path = null;
        }
        private void clear()
        {
            foreach (var item in Nodes)
            {
                item.IsSelected = false;
                item.Changed -= change;
            }
            Nodes.Clear();
            Connections.Clear();
        }
        private void open()
        {
            clear();
            var d = new OpenFileDialog();
            d.Filter = "JSON (*json)|*.json|Все файлы (*.*)|*.*";
            if (d.ShowDialog() == true) path = d.FileName;
            else return;

            List<INodeViewModel> nodeViewModels = new List<INodeViewModel>();
            List<ConnectionViewModel> connections = new List<ConnectionViewModel>();
            Utilite.OpenFile(ref nodeViewModels, ref connections, path);
            foreach (var item in nodeViewModels)
            {
                item.Changed += change;
            }
            Nodes.AddRange(nodeViewModels);
            Connections.AddRange(connections);
        }
        private void copy()
        {
            var listSelect = Nodes.Where((node) => node.IsSelected == true);
            var connects = Utilite.GetConnectionForNodeViewModel(listSelect, Connections);
            Clipboard.SetData("KP21", (object)Utilite.ConvertToJson(listSelect, connects));
            
        }
        private void paste()
        {
            if (Clipboard.ContainsData("KP21"))
            {
                string json = Clipboard.GetData("KP21").ToString();
                List<INodeViewModel> nodeViewModels = new List<INodeViewModel>();
                List<ConnectionViewModel> connectionViewModels = new List<ConnectionViewModel>();
                Utilite.ToObjects(nodeViewModels, connectionViewModels, json);
                foreach (var item in nodeViewModels)
                {
                    item.Changed += change;
                    item.IsSelected = true;
                }
                Nodes.AddRange(nodeViewModels);
                Connections.AddRange(connectionViewModels);
            }
        }
        private void executeBrash(bool flag)
        {
            if (flag)
            {
                Application.Current.Resources["BackgroundBorder"] = new SolidColorBrush(Color.FromRgb(202,81,0));
            }
            else 
            {
                Application.Current.Resources["BackgroundBorder"] = new SolidColorBrush(Color.FromRgb(28, 151, 234));
            }
        }
    }
}
