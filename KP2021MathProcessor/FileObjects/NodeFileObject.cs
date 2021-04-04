using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KP2021MathProcessor.FileObjects
{
    class NodeFileObject
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public Point Location { get; set; }
        public object Props { get; set; }
    }
}
