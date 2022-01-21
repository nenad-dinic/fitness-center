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

namespace SR44_2020_POP2021.Gui
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        DataTypes.User user;
        public AdminWindow(DataTypes.User user)
        {
            InitializeComponent();
            this.user = user;
            showInfo();
        }

        void showInfo()
        {
            WelcomeLabel.Content = user.name + " " + user.surname;
        }

        UpdateWindow updateWindow = null;

        private void ViewAndEditDataBtn_Click(object sender, RoutedEventArgs e)
        {
            if (updateWindow == null)
            {
                updateWindow = new UpdateWindow(user);
                updateWindow.Closing += delegate { updateWindow = null; };
                updateWindow.Show();
            }
            else
            {
                updateWindow.Focus();
            }
        }

        ViewUsersWindow viewUsersWindow = null;
        private void ViewAndEditUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            if(viewUsersWindow == null)
            {
                viewUsersWindow = new ViewUsersWindow();
                viewUsersWindow.Closing += delegate { viewUsersWindow = null; };
                viewUsersWindow.Show();
            }
            else
            {
                viewUsersWindow.Focus();
            }
        }

        RegisterWindow registerWindow = null;

        private void CreateTrainerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (registerWindow == null)
            {
                registerWindow = new RegisterWindow(DataTypes.UserTypes.trainer);
                registerWindow.Closing += delegate { registerWindow = null; };
                registerWindow.Show();
            }
            else
            {
                registerWindow.Focus();
            }
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (updateWindow != null)
            {
                updateWindow.Close();
            }

            if (registerWindow != null)
            {
                registerWindow.Close();
            }

            if(viewUsersWindow != null)
            {
                viewUsersWindow.Close();
            }

        }

    }
}
