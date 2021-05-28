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
        public INodifyCommand ConnectionCreateCommand { get; set; }
        public INodifyCommand DisconnectCommand { get; set; }
        public INodifyCommand DeleteSelectionCommand { get; set; }
        public INodifyCommand ExecuteCommand { get; set; }
        public INodifyCommand SaveFileCommand { get; set; }
        public INodifyCommand OpenFileCommand { get; set; }
        public INodifyCommand NewFileCommand { get; set; }
        public INodifyCommand CopyCommand { get; set; }
        public INodifyCommand PasteCommand { get; set; }
        public INodifyCommand PauseCommand { get; set; }
        public INodifyCommand SettingsOpenCommand { get; set; }
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
        View.ProtertyWindow settingsWindow = null;
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
            CreateNodeCommand = new DelegateCommand<CreateNodeInfoViewModel>(CreateNode);
            ConnectionCreateCommand = new DelegateCommand<(object Source, object Target)>((x) => СonCreate((IConnectorViewModel)x.Source, (IConnectorViewModel)x.Target));
            DisconnectCommand = new DelegateCommand<IConnectorViewModel>(DeleteConnection);
            DeleteSelectionCommand = new DelegateCommand(DelSelection);
            ExecuteCommand = new DelegateCommand(Execute);
            SaveFileCommand = new DelegateCommand(Save);
            OpenFileCommand = new DelegateCommand(Open);
            NewFileCommand = new DelegateCommand(NewFile);
            CopyCommand = new DelegateCommand(Copy);
            PasteCommand = new DelegateCommand(Paste);
            PauseCommand = new DelegateCommand(Pause);
            SettingsOpenCommand = new DelegateCommand(SettingsOpen);

            Utils.InitNodesList();
            foreach (var item in Utils.NodeTypes)
            {
                string name = "";
                if (item.GetCustomAttribute<NodeInfoAttribute>() is NodeInfoAttribute attribute && attribute != null)
                    name = attribute.NameNode;
                else name = item.Name;
                ContextMenuItems.Add(new NodeContexInfo() { Type = item, Name = name });
                ContextMenuItems = new NodifyObservableCollection<NodeContexInfo>(ContextMenuItems.OrderBy((x) => x.Name));
            }
        }

        private void СonCreate(IConnectorViewModel source, IConnectorViewModel target)
        {
            var connect = Utils.ConnectionCreate(source, target);
            if (connect != null)
            {
                Connections.Add(connect);
            }
        }
        private void CreateNode(CreateNodeInfoViewModel info)
        {
            var node = Utils.CreateNodeViewModel(Utils.CreateNode(info.Info.Type), info.Location );
            node.Changed += Change;
            Nodes.Add(node);
        }
        private void DeleteConnection(IConnectorViewModel connector)
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
        private void DelSelection()
        {
            var selected = Nodes.Where(o => o.IsSelected).ToList();
            selected.ForEach(o => {
                Nodes.Remove(o);
                o.Changed -= Change;
                o.Input.ForEach(i => DeleteConnection(i));
                o.Output.ForEach(i => DeleteConnection(i));
                });
            
        }
        private void Change(INodeViewModel source)
        {
            if (source.IsSelected)
            {
                SelectedItem = source;
            }
        }
        private void Execute()
        {
            if (isNotExecute)
            {
                Status = "Сборка";
                OutText = "";
                IEnumerable<INodeViewModel> nodes;
                Runner.Model model;
                var buildTask = new Task(() =>
                {
                    nodes = Utils.GetNodesNotKnot(Nodes);
                    model = Runner.Builder.Build(nodes, Utils.BuildConnection(Nodes, Connections));
                    interpreter = new Runner.Interpreter(model, new Runner.Contex() { PublicString = (x) => OutText += x });
                    interpreter.InitNode(nodes);
                    interpreter.Delay = Convert.ToInt32(Delay);
                });
                buildTask.RunSynchronously();
                if(buildTask.IsFaulted)
                {
                    MessageBox.Show(buildTask.Exception.Message, buildTask.Exception.Source, MessageBoxButton.OK, MessageBoxImage.Error);
                    IsNotExecute = true;
                    Status = "Готово";
                    executeBrash(false);
                    return;
                }

                
                
                executTask = new Task(interpreter.Start);
                executTask.ContinueWith(
                    (x) => {
                        if(x.IsFaulted)
                        {
                            MessageBox.Show(x.Exception.Message, x.Exception.Source, MessageBoxButton.OK, MessageBoxImage.Error );                           
                        }
                        IsNotExecute = true;
                        Status = "Готово";
                        interpreter.Refresh();
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
                if (IsPaused) Pause();
                interpreter.Stop();
                interpreter.Refresh();
            }

        }
        private void Pause()
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
        private void Save()
        {
            if (path == null)
            {
                var d = new SaveFileDialog();
                d.Filter = "JSON (*json)|*.json|Все файлы (*.*)|*.*";
                if (d.ShowDialog() == true) path = d.FileName;
                else return;
            }
            Utils.SavetoFile(Nodes, Connections, path);
        }
        private void NewFile()
        {
            clear();
            path = null;
        }
        private void clear()
        {
            foreach (var item in Nodes)
            {
                item.IsSelected = false;
                item.Changed -= Change;
            }
            Nodes.Clear();
            Connections.Clear();
        }
        private void Open()
        {
            clear();
            var d = new OpenFileDialog();
            d.Filter = "JSON (*json)|*.json|Все файлы (*.*)|*.*";
            if (d.ShowDialog() == true) path = d.FileName;
            else return;

            List<INodeViewModel> nodeViewModels = new List<INodeViewModel>();
            List<ConnectionViewModel> connections = new List<ConnectionViewModel>();
            Utils.OpenFile(ref nodeViewModels, ref connections, path);
            foreach (var item in nodeViewModels)
            {
                item.Changed += Change;
            }
            Nodes.AddRange(nodeViewModels);
            Connections.AddRange(connections);
        }
        private void Copy()
        {
            var listSelect = Nodes.Where((node) => node.IsSelected == true);
            var connects = Utils.GetConnectionForNodeViewModel(listSelect, Connections);
            Clipboard.SetData("KP21", (object)Utils.ConvertToJson(listSelect, connects));
            
        }
        private void Paste()
        {
            if (Clipboard.ContainsData("KP21"))
            {
                string json = Clipboard.GetData("KP21").ToString();
                List<INodeViewModel> nodeViewModels = new List<INodeViewModel>();
                List<ConnectionViewModel> connectionViewModels = new List<ConnectionViewModel>();
                Utils.ToObjects(nodeViewModels, connectionViewModels, json);
                foreach (var item in nodeViewModels)
                {
                    item.Changed += Change;
                    item.IsSelected = true;
                }
                Nodes.AddRange(nodeViewModels);
                Connections.AddRange(connectionViewModels);
            }
        }
        
        private void SettingsOpen()
        {
            if (settingsWindow == null)
            {
                settingsWindow = new View.ProtertyWindow();
                settingsWindow.Closed += SettingsWindow_Closed;
            }
            settingsWindow.Show();
            settingsWindow.Focus();
        }

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            settingsWindow.Closed -= SettingsWindow_Closed;
            settingsWindow = null;
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
