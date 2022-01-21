﻿using System;
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

        UpdateWindow updateWindow = null;

        private void ViewAndEditDataBtn_Click(object sender, RoutedEventArgs e)
        {
            if(updateWindow == null)
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


        ViewTrainers viewTrainersWindow = null;

        private void ViewTrainerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (viewTrainersWindow == null)
            {
                viewTrainersWindow = new ViewTrainers();
                viewTrainersWindow.Show();
                viewTrainersWindow.Closing += delegate { viewTrainersWindow = null; };
            }
            else
            {
                viewTrainersWindow.Focus();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(updateWindow != null)
            {
                updateWindow.Close();
            }
            
            if(viewTrainersWindow != null)
            {
                viewTrainersWindow.Close();
            }

        }

    }
}