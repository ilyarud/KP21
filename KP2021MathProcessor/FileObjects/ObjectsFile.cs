using System;
using System.Collections.Generic;
using System.Text;

namespace KP2021MathProcessor.FileObjects
{
    class ObjectsFile
    {
        public List<NodeFileObject> Nodes { get; set; }
        public List<ConnectionFileObject> Connections { get; set; }
    }
}
