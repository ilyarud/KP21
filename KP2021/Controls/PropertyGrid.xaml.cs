using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;

namespace KP2021MathProcessor.Controls
{
    /// <summary>
    /// Логика взаимодействия для PropertyGrid.xaml
    /// </summary>
    public partial class PropertyGrid : UserControl
    {
        #region Static var
        private static Regex regexIsInt = new Regex("^[0-9-]+");
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
            nameof(ItemNode), 
            typeof(object), 
            typeof(PropertyGrid),
            new PropertyMetadata(null, propertyChangedCallback ));
        #endregion

        public PropertyGrid()
        {
            InitializeComponent();
        }

        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PropertyGrid propertyGrid)
            {
                propertyGrid.build(propertyGrid.ItemNode);
            }
        }

        public object ItemNode { get => GetValue(ItemProperty); 
            set
            {
                SetValue(ItemProperty, value); 
            } 
        }
        Tuple<Type, TypeProperty>[] supportTypes = 
            { 
            new Tuple<Type, TypeProperty>(typeof(Int32), TypeProperty.Integer),
            new Tuple<Type, TypeProperty>(typeof(string), TypeProperty.String)
            };
        public void build(object target)
        {
            stack.Items.Clear();
            if (target == null) return;
            foreach (var property in target.GetType().GetProperties())
            {
                var propertyInfo = supportTypes.FirstOrDefault((x) => property.PropertyType == x.Item1);
                if (propertyInfo != null)
                {
                    if (propertyInfo.Item2 == TypeProperty.Integer)
                    {
                        stack.Items.Add(new PropertyViewModel()
                        {
                            Name = property.Name,
                            TypeProperty = propertyInfo.Item2,
                            Data = property.GetMethod.Invoke(target, null),
                            SetData = (x => property.SetMethod.Invoke(target, new object[] { ((string)x) == "" ? 0 : Convert.ToInt32(x) }))
                        });
                    }
                    else { 
                        stack.Items.Add(new PropertyViewModel()
                        {
                            Name = property.Name,
                            TypeProperty = propertyInfo.Item2,
                            Data = property.GetMethod.Invoke(target, null),
                            SetData = (
                            x => property.SetMethod.Invoke(target, new object[] { ((string)x)}))
                        });
                    }
                }
            } 
            
        }

        private void IntegetTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !regexIsInt.IsMatch(e.Text);
        }


    }
}
