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
using TODOit.Resources;

namespace TODOit
{
    public partial class MainWindow : Window
    {
        private DB dB;
        private List<TaskBox> TaskBoxes = new List<TaskBox>();
        
        private void TasksFill()
        {
            TaskBox taskBox;
            for (int i = 0; i < dB.Tasks.Count; i++)
            {
                taskBox = new TaskBox
                {
                    isCompleted = dB.Tasks[i].IsCompleted,
                    Title = dB.Tasks[i].Text, 
                    Padding = new Thickness(0, 5, 0, 5),
                    Id = i
                };
                taskBox.LostFocus += new RoutedEventHandler(TaskBoxLostFocus);
                taskBox.checkBox.Checked += new RoutedEventHandler(TaskBoxChecked);
                taskBox.checkBox.Unchecked += new RoutedEventHandler(TaskBoxUnChecked);
                taskBox.DeleteTaskButton.Click += new RoutedEventHandler(DeleteTaskBoxClick);
                TaskBoxes.Add(taskBox);
                TasksStackPanel.Children.Add(TaskBoxes[i]);
            }
            
           
        }
        void DeleteTaskBoxClick(object sender, RoutedEventArgs e)
        {
            TaskBox o = ((sender as Button).Parent as Grid).Parent as TaskBox;
            if (o == null)
            {
                return;
            }
            TaskBoxes.RemoveAt(o.Id);
            TasksStackPanel.Children.RemoveAt(o.Id);
            dB.DeleteTask(o.Id);
            
        }
        void TaskBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TaskBox o = sender as TaskBox;
            dB.TaskChange(o.Id, o.textBox.Text);
        }
        void TaskBoxChecked(object sender, RoutedEventArgs e)
        {
            TaskBox o = ((sender as CheckBox).Parent as Grid).Parent as TaskBox;
            if (o == null)
            {
                MessageBox.Show("ERROR");
                return;
            }
            dB.CompletedChange(o.Id, true);
        }
        void TaskBoxUnChecked(object sender, RoutedEventArgs e)
        {
            TaskBox o = ((sender as CheckBox).Parent as Grid).Parent as TaskBox;
            if (o == null)
            {
                MessageBox.Show("ERROR");
                return;
            }
            dB.CompletedChange(o.Id, false);
        }
        
        public MainWindow()
        {

            InitializeComponent();
            dB = new DB();
            TasksFill();
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("Resources/Themes/" + Properties.Settings.Default["Theme"] + ".xaml", UriKind.Relative);
        }
        void NewDeleteTaskBoxClick(object sender, RoutedEventArgs e)
        {
            TaskBox o = ((sender as Button).Parent as Grid).Parent as TaskBox;
            if (o == null)
            {
                return;
            }
            o.LostFocus -= NewTaskBoxLostFocus;
            TaskBoxes.RemoveAt(o.Id);
            TasksStackPanel.Children.RemoveAt(o.Id);
            AddTaskButton.IsEnabled = true;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            dB.CloseConnection();
        }
        void NewTaskBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TaskBox o = sender as TaskBox;
            TaskBoxes[dB.Tasks.Count].LostFocus -= new RoutedEventHandler(NewTaskBoxLostFocus);
            TaskBoxes[dB.Tasks.Count].LostFocus += new RoutedEventHandler(TaskBoxLostFocus);
            dB.AddTask(TaskBoxes[dB.Tasks.Count].Title);
            AddTaskButton.IsEnabled = true;
        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskBox taskBox = new TaskBox
            {
                isCompleted = false,
                Title = "",
                Padding = new Thickness(0, 5, 0, 5),
                Id = dB.Tasks.Count
            };
            taskBox.LostFocus += new RoutedEventHandler(NewTaskBoxLostFocus);
            taskBox.DeleteTaskButton.Click += new RoutedEventHandler(NewDeleteTaskBoxClick);
            TaskBoxes.Add(taskBox);
            TasksStackPanel.Children.Add(TaskBoxes[TaskBoxes.Count - 1]);
            AddTaskButton.IsEnabled = false;
        }
    }
}
