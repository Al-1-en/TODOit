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
using System.Windows.Shapes;
using System.Configuration;

namespace TODOit
{
    public partial class Settings : Window
    {
        private string Theme;
        private RadioButton ThemeRadioButton;
        public Settings()
        {
            InitializeComponent();
            Theme = Properties.Settings.Default.Theme;
            object Object = StackPanel.FindName(Theme);
            if (Object is RadioButton)
            {
                ThemeRadioButton = Object as RadioButton;
                ThemeRadioButton.IsChecked = true;
            }
            else
            {
                MessageBox.Show("Возникла ошибка. Перезапустите приложение");
                this.Close();
            }
        }
        private void ThemeChange()
        {
            Properties.Settings.Default.Theme = Theme;
            Properties.Settings.Default.Save();
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("Resources/Themes/" + Theme + ".xaml", UriKind.Relative);
        }
        private void Light_Checked(object sender, RoutedEventArgs e)
        {
            Theme = "Light";
            ThemeChange();
        }

        private void Dark_Checked(object sender, RoutedEventArgs e)
        {
            Theme = "Dark";
            ThemeChange();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Blue_Checked(object sender, RoutedEventArgs e)
        {
            Theme = "Blue";
            ThemeChange();
        }
    }
}
