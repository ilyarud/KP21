using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.FileObjects
{

    public class ConnectionFileObject
    {
        public struct ConnectorInfo
        {
            public int IDNode { get; set; }
            public int IDConnector { get; set; }
        }
        public ConnectorInfo Input { get; set; }
        public ConnectorInfo Output { get; set; }
    }
}
