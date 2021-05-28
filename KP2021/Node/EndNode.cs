using KP2021MathProcessor.Connector;
using KP2021MathProcessor.Runner;


namespace KP2021MathProcessor.Node
{
    class EndNode : ANode
    {
        public EndNode()
        {
            AddOutputConnector(new FlowConnector(this));
            AddOutputConnector(new IntegerConnector(this, () => time) { Name = "Время" });
            AddOutputConnector(new IntegerConnector(this, () => cicle) {  Name = "Колличество циклов"});
        }
        private int time;
        private int cicle;
        public override string Header => "Последний поток";
        public override bool IsExecuted => true;

        public override bool Execute(Contex contex)
        {
            time = RunTimeInfo.Time;
            cicle = RunTimeInfo.NumberCicle;
            return true;
        }
    }
}
