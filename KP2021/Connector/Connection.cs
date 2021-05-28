using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.Connector
{
    class Connection
    {
        public IConnector Input { get; set; }
        public IConnector Output { get; set; }
    }
}
