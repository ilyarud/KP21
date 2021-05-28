using System;
using System.Collections.Generic;
using System.Text;
using KP2021MathProcessor.Node;
using KP2021MathProcessor.ViewModel.Node;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Reflection;
using KP2021MathProcessor.FileObjects;
using IO = System.IO;
using System.Windows;

namespace KP2021MathProcessor.ViewModel
{
    static class Utils
    {
        public static IEnumerable<INodeViewModel> GetNodesNotKnot(IEnumerable<INodeViewModel> nodeViewModels)
        {
            return nodeViewModels.Where(x => !x.IsKnot);
        }
        public static IEnumerable<ConnectionViewModel> BuildConnection(IEnumerable<INodeViewModel> nodeViewModels, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            var listConnect = new List<ConnectionViewModel>();
            foreach (var connection in connectionViewModels)
            {
                if (!(connection.Input.Node.IsKnot || connection.Output.Node.IsKnot)) listConnect.Add(connection);
            }
            foreach (var node in nodeViewModels.Where(x => x.IsKnot))
            {
                var conector = node.Input.ElementAt(0);
                var inputs = connectionViewModels.Where(x => x.Input == conector);
                var outputs = connectionViewModels.Where(x => x.Output == conector);
                foreach (var input in inputs)
                {
                    foreach (var output in outputs)
                    {
                        listConnect.Add(ConnectionCreate(input.Output, output.Input, true));
                    }
                }
            }
            return listConnect; 
        }
        public static INodeViewModel CreateNodeViewModel(INode node, Point location)
        {
            INodeViewModel nodeViewModel = CreateNodeViewModel(node);
            if (nodeViewModel != null) nodeViewModel.Location = location;
            return nodeViewModel;
        }
        public static INodeViewModel CreateNodeViewModel(INode node)
        {
            INodeViewModel nodeViewModel = null;
            if (node is KnotNode kn)
            {
                nodeViewModel = new KnotNodeViewModel(kn);
            }
            else if (node is ANode)
            {
                nodeViewModel = new NodeViewModel(node);
            }
            return nodeViewModel;
        }
        public static List<Type> NodeTypes { get; private set; } = new List<Type>();
        public static void InitNodesList()
        {
            NodeTypes.Clear();
            var assembly = Assembly.GetAssembly(typeof(Utils));
            NodeTypes = new List<Type>(assembly.GetTypes().Where(
            (t) => t.GetInterfaces().FirstOrDefault((i) => i == typeof(INode)) == typeof(INode) && t.IsAbstract == false));
        }
        public static IEnumerable<ConnectionFileObject> GetConnectionFileObjects(IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            List<ConnectionFileObject> connections = new List<ConnectionFileObject>();
            foreach (var item in connectionViewModels)
            {
                connections.Add( new ConnectionFileObject()
                {
                    Input = new ConnectionFileObject.ConnectorInfo()
                    { IDConnector = item.Input.Connector.ID, IDNode = item.Input.Node.Node.Id },
                    Output = new ConnectionFileObject.ConnectorInfo()
                    { IDConnector = item.Output.Connector.ID, IDNode = item.Output.Node.Node.Id }
                });
            }
            return connections;
        }
        public static IEnumerable<NodeFileObject> NodesViewModelToNodesFIle(IEnumerable<INodeViewModel> nodeViewModels)
        {
            List<NodeFileObject> nodes = new List<NodeFileObject>();
            foreach (var node in nodeViewModels) nodes.Add(new NodeFileObject()
            {
                Location = node.Location,
                Props = node.Props,
                Type = node.Node.GetType().FullName,
                ID = node.Node.Id
            });
            return nodes;
        }
        public static void SavetoFile(IEnumerable<INodeViewModel> nodeViewModels, IEnumerable<ConnectionViewModel> connectionViewModels, string path)
        {
            string save = ConvertToJson(nodeViewModels, connectionViewModels);
            if (!IO.File.Exists(path))
            {
                IO.File.Create(path).Close();
            }
            IO.File.WriteAllText(path, save);
        }
        public static string ConvertToJson(IEnumerable<INodeViewModel> nodeViewModels, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            int countNode = 0;
            foreach (var node in nodeViewModels) node.Node.Id = ++countNode;
            var nodes = NodesViewModelToNodesFIle(nodeViewModels).ToList();
            var connections = GetConnectionFileObjects(connectionViewModels).ToList();
            var fileObject = new ObjectsFile() { Nodes = nodes, Connections = connections };
            var save = JsonSerializer.Serialize<ObjectsFile>(fileObject, new JsonSerializerOptions() { WriteIndented = true });
            return save;
        }
        public static void OpenFile( ref List<INodeViewModel> nodeViewModels, ref List<ConnectionViewModel> connectionViewModels, string path)
        {
            var open = IO.File.ReadAllText(path);
            ToObjects(nodeViewModels, connectionViewModels, open);
        }
        public static void ToObjects(List<INodeViewModel> nodeViewModels, List<ConnectionViewModel> connectionViewModels, string open)
        {
            var obj = JsonSerializer.Deserialize<ObjectsFile>(open);
            foreach (var item in obj.Nodes)
            {
                var t = NodeTypes.FirstOrDefault((x) => x.FullName == item.Type);
                if (t != null)
                {
                    var n = CreateNode(t);
                    n.Location = item.Location;
                    if (item.Props != null)
                    {
                        n.Props = JsonSerializer.Deserialize(item.Props.ToString(), n.TypePropertys);
                    }

                    n.Id = item.ID;
                    nodeViewModels.Add(CreateNodeViewModel(n));
                }
            }
            foreach (var item in obj.Connections)
            {
                var connection = ConnectionCreate(
                    FindConnector(nodeViewModels, item.Input.IDNode, item.Input.IDConnector),
                    FindConnector(nodeViewModels, item.Output.IDNode, item.Output.IDConnector)
                    );
                if (connection != null) connectionViewModels.Add(connection);
            }
        }
        private static IConnectorViewModel FindConnector(IEnumerable<INodeViewModel> nodeViewModels, int nodeId, int connectorId)
        {
            var node = nodeViewModels.FirstOrDefault((x) => x.Node.Id == nodeId);
            if(node != null)
            {
                var connector = node.Output.FirstOrDefault((x) => x.Connector.ID == connectorId);
                if (connector == null) connector = node.Input.FirstOrDefault((x) => x.Connector.ID == connectorId);
                return connector;
            }
            return null;
        }
        public static INode CreateNode(Type t)
        {
            Type[] argT = { };
            var construct = t.GetConstructor(argT);
            return (INode)construct.Invoke(null);
        }
        public static bool CanConnect(IConnectorViewModel Source, IConnectorViewModel Target)
        {
            if (Source.IsInput == Target.IsInput || Target.TypeID != Source.TypeID)
            {
                return false;
            }
            return true;
        }
        public static ConnectionViewModel ConnectionCreate(IConnectorViewModel Source, IConnectorViewModel Target, bool connectIgnore = false)
        {
            if (Source == null || Target == null) return null;
            if (Source.Node.IsKnot || Target.Node.IsKnot)  KnotCreateConnect(Source, Target);
            if (!CanConnect(Source, Target)) return null;
            ConnectionViewModel connectionViewModel = new ConnectionViewModel();
            if (Source.IsInput)
            {
                connectionViewModel.Input = Source;
                connectionViewModel.Output = Target;
            }
            else
            {
                connectionViewModel.Input = Target;
                connectionViewModel.Output = Source;
            }
            if (!connectIgnore)
            {
                if (connectionViewModel.Input.IsConnect == true && !connectionViewModel.Input.Node.IsKnot) return null;
                Target.IsConnect = true;
                Source.IsConnect = true;
            }

            connectionViewModel.Connection = new Connector.Connection { Input = Target.Connector, Output = Source.Connector };
            return connectionViewModel;
        }
        private static void KnotCreateConnect(IConnectorViewModel source, IConnectorViewModel target)
        {
            UniversalConnector universalConnector;
            IConnectorViewModel connector = source;
            universalConnector = IsKnot(source, target, ref connector);
            universalConnector.IsInput = !connector.IsInput;
            if (!universalConnector.IsConnect)
            {
                universalConnector.TypeID = connector.TypeID;
                universalConnector.Color = connector.Color;
            }
        }

        private static UniversalConnector IsKnot(IConnectorViewModel source, IConnectorViewModel target, ref IConnectorViewModel connector)
        {
            UniversalConnector universalConnector;
            if (target is UniversalConnector uc) universalConnector = uc;
            else
            {
                universalConnector = (UniversalConnector)source;
                connector = target;
            }

            return universalConnector;
        }

        public static IEnumerable<ConnectionViewModel> GetConnectionForNodeViewModel(IEnumerable<INodeViewModel> nodeViewModels, IEnumerable<ConnectionViewModel> connectionViewModels)
        {
            var listCon = new List<ConnectionViewModel>();
            foreach (var node in nodeViewModels)
            {
                listCon.AddRange(connectionViewModels.Where((x) => node.Input.Any((y => x.Input == y))));               
            }
            var ListResult = new List<ConnectionViewModel>();
            foreach (var node in nodeViewModels)
            {
                ListResult.AddRange(listCon.Where(x => node.Output.Any(y => x.Output == y)));
            }
                
            return ListResult;
        }
    }
}
