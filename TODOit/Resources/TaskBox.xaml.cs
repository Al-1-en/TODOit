using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace TODOit.Resources
{
    /// <summary>
    /// Логика взаимодействия для Task.xaml
    /// </summary>
    public partial class TaskBox : UserControl
    {

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TaskBox));
        //public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Task));
        public static readonly DependencyProperty isCompletedProperty = DependencyProperty.Register("isCompleted", typeof(bool), typeof(TaskBox));
        public static readonly DependencyProperty idProperty = DependencyProperty.Register("id", typeof(int), typeof(TaskBox));
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public int Id
        {
            get => (int)GetValue(idProperty);
            set => SetValue(idProperty, value);
        }
        /*public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }*/
        public bool isCompleted
        {
            get => (bool)GetValue(isCompletedProperty);
            set => SetValue(isCompletedProperty, value);
        }

        public TaskBox()
        {
            InitializeComponent();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            checkBox.ToolTip = "Remove mark";
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox.ToolTip = "Complete";
        }
    }
}
