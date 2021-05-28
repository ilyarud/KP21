using System;
using System.Collections.Generic;
using System.Text;
using MVVM;

namespace KP2021MathProcessor.Controls
{
    enum TypeProperty
    {
        Integer,
        String
    }
    class PropertyViewModel : ObservableObject
    {
        public Action<object> SetData { get; set; }
        private string name;
        private object data;
        private TypeProperty type;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public object Data { get => data; set { SetProperty(ref data, value); if (SetData != null) SetData(value); } }
        public TypeProperty TypeProperty { get => type; set => SetProperty(ref type, value); }
    }
}
