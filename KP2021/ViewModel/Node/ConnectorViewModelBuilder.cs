using KP2021MathProcessor.Connector;
using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.ViewModel.Node
{
    class ConnectorViewModelBuilder
    {
        public static IConnectorViewModel Build(IConnector connector,INodeViewModel baseNode, bool isInput)
        {           
            IConnectorViewModel connectorView = null;
            switch (connector.ConnectorType)
            {
                case ConnectorType.Data:
                    connectorView = new ConnectorDataViewModel(connector, baseNode);
                    break;
                case ConnectorType.Flow:
                    connectorView = new ConnectorFlowViewModel(connector, baseNode);
                    break;                   
                default:
                    return null;
                    break;
            }
            if (connectorView != null) connectorView.IsInput = isInput;
            return connectorView;
        }
    }
}
