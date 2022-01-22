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
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        DataTypes.User user;

        public UpdateWindow(DataTypes.User user)
        {
            InitializeComponent();

            GenderField.Items.Add("Musko");
            GenderField.Items.Add("Zensko");
            GenderField.Items.Add("Drugo");
            GenderField.SelectedIndex = 0;

            this.user = user;
            if (ValidateUser())
            {
                LoadData();
            } 
            else
            {
                this.Close();
            }
        }

        private bool ValidateUser()
        {
            if (user == null)
            {
                MessageBox.Show("Nije moguce izmeniti ovog korisnika!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void LoadData()
        {
            NameField.Text = user.name;
            SurnameField.Text = user.surname;
            JmbgField.Text = user.jmbg;
            PassField.Text = user.password;
            GenderField.SelectedIndex = (int)user.gender;
            StreetField.Text = user.address.street;
            HouseNumField.Text = user.address.houseNum;
            CityField.Text = user.address.city;
            CountryField.Text = user.address.country;
            EmailField.Text = user.email;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = NameField.Text;
            string surname = SurnameField.Text;
            string jmbg = JmbgField.Text;
            string password = PassField.Text;
            DataTypes.Genders gender = (DataTypes.Genders)GenderField.SelectedIndex;
            string street = StreetField.Text;
            string houseNum = HouseNumField.Text;
            string city = CityField.Text;
            string country = CountryField.Text;
            string email = EmailField.Text;
   
            DataController.UpdateUser(user.id, name, surname, jmbg, password, gender, email);
            DataController.UpdateAddress(user.address.id, street, houseNum, city, country);
            MessageBox.Show("Izmene uspesno sacuvane");
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
