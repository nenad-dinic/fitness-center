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

namespace SR44_2020_POP2021.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataController.Init();
            
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idField.Text);
            DataController.DeleteUser(id);
            MessageBox.Show("Uspesno obrisan!");
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWin = new RegisterWindow();
            regWin.Show();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(idField.Text);
            UpdateWindow updateWin = new UpdateWindow(DataController.GetUserById(id));
            updateWin.Show();
        }
    }
}