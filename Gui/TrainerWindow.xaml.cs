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
    /// Interaction logic for TrainerWindow.xaml
    /// </summary>
    public partial class TrainerWindow : Window
    {
        DataTypes.User user;
        public TrainerWindow(DataTypes.User user)
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

        private void Window_Closed(object sender, EventArgs e)
        {
            if (updateWindow != null)
            {
                updateWindow.Close();
            }

            if(viewTrainingsWindow != null)
            {
                viewTrainingsWindow.Close();
            }

        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ViewTrainingsWindow viewTrainingsWindow = null;
        private void ViewTrainingBtn_Click(object sender, RoutedEventArgs e)
        {
            if(viewTrainingsWindow == null)
            {
                viewTrainingsWindow = new ViewTrainingsWindow(user, user);
                viewTrainingsWindow.Closed += delegate { viewTrainingsWindow = null; };
                viewTrainingsWindow.Show();
            }
            else
            {
                viewTrainingsWindow.Focus();
            }
        }
    }
}
