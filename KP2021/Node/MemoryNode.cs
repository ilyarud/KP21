using System;
using KP2021MathProcessor.Attributes;
using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;

namespace KP2021MathProcessor.Node
{
    [NodeInfo("Область памяти")]
    class MemoryNode : ANode
    {

        public MemoryNode()
        {
            AddOutputConnector(new MemoryConnector(this, () => this) { Name = "" });
        }
        public override string Header => "Область памяти";
    
        public override bool IsExecuted { get => false; }

        int cicle = int.MaxValue;
        bool read;
        bool write;
        public override void Initialize()
        {
            base.Initialize();
            cicle = int.MaxValue;
        }

        public void Read()
        {
            if (RunTimeInfo.NumberCicle != cicle)
            {
                cicle = RunTimeInfo.NumberCicle;
                read = write = false;
            }
            if (write) throw new Exception("Ошибка чтения");
            read = true;
        }
        public void Write()
        {
            if (RunTimeInfo.NumberCicle != cicle)
            {
                cicle = RunTimeInfo.NumberCicle;
                read = write = false;               
            }
            if (write || read) throw new Exception("Ошибка записи");
            write = true;
        }
    }
}
