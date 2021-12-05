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
            foreach (DataTypes.User u in DataController.users)
            {
                if (u.isDeleted == false)
                {
                    MessageBox.Show(u.name);
                }
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteUserWindow delWin = new DeleteUserWindow();
            delWin.Show();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWin = new RegisterWindow();
            regWin.Show();
        }
    }
}
