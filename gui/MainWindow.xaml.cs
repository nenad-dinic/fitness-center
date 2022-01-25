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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataController.Init();
            
        }

        RegisterWindow registerWindow = null;

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (registerWindow == null)
            {
                registerWindow = new RegisterWindow(DataTypes.UserTypes.trainee);
                registerWindow.Closing += delegate { registerWindow = null; };
                registerWindow.Show();
            }
            else
            {
                registerWindow.Focus();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e){
            DataTypes.User user;
            user = DataController.LoginUser(JmbgField.Text, PassField.Text);
            if(user == null)
            {
                MessageBox.Show("Uneli ste pogresan JMBG ili sifru");
                return;
            }

            Window window = null;
            if(user.userTypes == DataTypes.UserTypes.trainee)
            {
                window = new TraineeWindow(user);
            }else if(user.userTypes == DataTypes.UserTypes.admin)
            {
                window = new AdminWindow(user);
            } else if (user.userTypes == DataTypes.UserTypes.trainer)
            {
                window = new TrainerWindow(user);
            }

            window.Show();
            window.Closing += delegate {
                JmbgField.Text = "";
                PassField.Text = "";
                this.Show(); 
            };
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

            if(registerWindow != null)
            {
                registerWindow.Close();
            }

            if(viewCenterInfoWindow != null)
            {
                viewCenterInfoWindow.Close();
            }

            if(viewTrainersWindow != null)
            {
                viewTrainersWindow.Close();                    
            }
            
            Application.Current.Shutdown();

        }

        ViewTrainersWindow viewTrainersWindow = null;
        private void ViewTrainersBtn_Click(object sender, RoutedEventArgs e)
        {
            if(viewTrainersWindow == null)
            {
                viewTrainersWindow = new ViewTrainersWindow(null);
                viewTrainersWindow.Closed += delegate { viewTrainersWindow = null; };
                viewTrainersWindow.Show();
            }
            else
            {
                viewTrainersWindow.Focus();
            }
        }

        ViewCenterInfoWindow viewCenterInfoWindow = null;
        private void BasicCenterInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (viewCenterInfoWindow == null)
            {
                viewCenterInfoWindow = new ViewCenterInfoWindow();
                viewCenterInfoWindow.Closed += delegate { viewCenterInfoWindow = null; };
                viewCenterInfoWindow.Show();
            }
            else
            {
                viewCenterInfoWindow.Focus();
            }
        }
    }
}