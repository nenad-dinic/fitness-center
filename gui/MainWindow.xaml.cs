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

namespace SR44_2020_POP2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataCotroller.Init();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Koj meseg hoces da pokazes!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataCotroller.CreateUser(0, "Nenad", "Dinic", "0806001762022",DataTypes.Genders.male, new DataTypes.Address(0, "Street", "number", "City", "Country") , "nenad.d.dinic01@gmail.com", DataTypes.UserTypes.admin, new DateTime(0));
        }
    }
}
