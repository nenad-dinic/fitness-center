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
    /// Interaction logic for TraineeWindow.xaml
    /// </summary>
    public partial class TraineeWindow : Window
    {

        DataTypes.User user;
    
        public TraineeWindow(DataTypes.User user)
        {
            InitializeComponent();
            this.user = user;
            showInfo();
        }

        void showInfo()
        {
            WelcomeLabel.Content = user.name + " " + user.surname;
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        UpdateUserWindow updateUserWindow = null;

        private void ViewAndEditDataBtn_Click(object sender, RoutedEventArgs e)
        {
            if(updateUserWindow == null)
            {
                updateUserWindow = new UpdateUserWindow(user);
                updateUserWindow.Closing += delegate { updateUserWindow = null; };
                updateUserWindow.Show();
            }
            else
            {
                updateUserWindow.Focus();
            }

        }


        ViewTrainersWindow viewTrainersWindow = null;

        private void ViewTrainerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (viewTrainersWindow == null)
            {
                viewTrainersWindow = new ViewTrainersWindow(user);
                viewTrainersWindow.Show();
                viewTrainersWindow.Closing += delegate { viewTrainersWindow = null; };
            }
            else
            {
                viewTrainersWindow.Focus();
            }
        }

        ViewReservationsWindow viewReservationsWindow = null;
        private void ViewReservationBtn_Click(object sender, RoutedEventArgs e)
        {
            if(viewReservationsWindow == null)
            {
                viewReservationsWindow = new ViewReservationsWindow(user);
                viewReservationsWindow.Show();
                viewReservationsWindow.Closed += delegate { viewReservationsWindow = null; };
            }
            else
            {
                viewReservationsWindow.Focus();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(updateUserWindow != null)
            {
                updateUserWindow.Close();
            }
            
            if(viewTrainersWindow != null)
            {
                viewTrainersWindow.Close();
            }

            if(viewReservationsWindow != null)
            {
                viewReservationsWindow.Close();
            }

        }

    }
}
