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
    /// Interaction logic for ViewTrainers.xaml
    /// </summary>
    public partial class ViewTrainersWindow : Window
    {

        class Row{
            public int id { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string address { get; set; }
            public string email { get; set; }
        }

        DataTypes.User user;

        public ViewTrainersWindow(DataTypes.User user)
        {
            InitializeComponent();
            this.user = user;
            if(user == null)
            {
                TrainerTable.Columns[5].Visibility = Visibility.Hidden;   
            }
            ShowTrainers();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowTrainers(NameField.Text, SurnameField.Text, AddressField.Text, EmailField.Text);
        }

        void ShowTrainers(string nameFilter = "", string surnameFilter = "", string addressFilter = "", string emailFilter = "")
        {
            TrainerTable.Items.Clear();
            List<DataTypes.User> trainers = DataController.GetAllTrainers();
            foreach (DataTypes.User trainer in trainers)
            {
                string address = trainer.address.street + " " + trainer.address.houseNum + ", " + trainer.address.city + ", " + trainer.address.country;
                if (trainer.name.ToLower().Contains(nameFilter.ToLower()) && trainer.surname.ToLower().Contains(surnameFilter.ToLower()) && address.ToLower().Contains(addressFilter.ToLower()) && trainer.email.ToLower().Contains(emailFilter.ToLower()))
                {
                    TrainerTable.Items.Add(new Row() { id = trainer.id, name = trainer.name, surname = trainer.surname, address = address, email = trainer.email });
                }
            }
        }


        ViewTrainingsWindow viewTrainingsWindow = null; 
        public void ViewTrainingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Row row = (Row)((Button)e.OriginalSource).DataContext;
            DataTypes.User trainer = DataController.GetUserById(row.id);
            if(viewTrainingsWindow == null)
            {
                viewTrainingsWindow = new ViewTrainingsWindow(user, trainer);
                viewTrainingsWindow.Closed += delegate { this.viewTrainingsWindow = null; };
                viewTrainingsWindow.Show();
            }
            else
            {
                viewTrainingsWindow.Focus();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(viewTrainingsWindow != null)
            {
                viewTrainingsWindow.Close();
            }
        }

    }
}
