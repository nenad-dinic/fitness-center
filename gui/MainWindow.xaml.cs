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

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWin = new RegisterWindow();
            regWin.Show();
        }

        private void Login_Click(object sender, RoutedEventArgs e){
            DataTypes.User user;
            user = DataController.LoginUser(JmbgField.Text, PassField.Text);
            if(user.userTypes == DataTypes.UserTypes.trainee)
            {
                TraineeWindow window = new TraineeWindow(user, this);
                window.Show();
            }
            this.Hide();
        }

    }
}