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
using System.Text.RegularExpressions;

namespace SR44_2020_POP2021.Gui
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {
        DataTypes.User user;

        public UpdateUserWindow(DataTypes.User user)
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
            PassField.Password = user.password;
            RepeatPassField.Password = user.password;
            GenderField.SelectedIndex = (int)user.gender;
            StreetField.Text = user.address.street;
            HouseNumField.Text = user.address.houseNum;
            CityField.Text = user.address.city;
            CountryField.Text = user.address.country;
            EmailField.Text = user.email;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

            List<string> errors = new List<string>();

            string name = NameField.Text;
            string surname = SurnameField.Text;
            string jmbg = JmbgField.Text;
            string password = PassField.Password;
            string repeatPassword = RepeatPassField.Password;
            DataTypes.Genders gender = (DataTypes.Genders)GenderField.SelectedIndex;
            string street = StreetField.Text;
            string houseNum = HouseNumField.Text;
            string city = CityField.Text;
            string country = CountryField.Text;
            string email = EmailField.Text;

            if (!Regex.Match(name, "^[A-Z][a-z]*$").Success)
            {
                errors.Add("Ime nije validno");
            }

            if (!Regex.Match(surname, "^[A-Z][a-z]*$").Success)
            {
                errors.Add("Prezime nije validno");
            }

            if (!Regex.Match(street, "^[A-Z][a-zA-Z -]*$").Success)
            {
                errors.Add("Ime ulice nije validno");
            }

            if (!Regex.Match(houseNum, "^((\\d+[a-z]{1})|(\\d+))(\\/\\d+)?$").Success)
            {
                errors.Add("Broj mesta stanovanja nije validan");
            }

            if (!Regex.Match(city, "^[A-Z][a-zA-Z ]*$").Success)
            {
                errors.Add("Ime grada nije validno");
            }

            if (!Regex.Match(country, "^[A-Z][a-zA-Z -]*$").Success)
            {
                errors.Add("Ime drzave nije validno");
            }

            if (!Regex.Match(email, "^[a-z0-9]+([.]?[[a-z0-9])+\\@[a-z]+(\\.[a-z]{2,})+$").Success)
            {
                errors.Add("Email nije validan");
            }

            if (password.Length < 5)
            {
                errors.Add("Duzina sifre mora biti preko 4 karaktera");
            }

            if (password != repeatPassword)
            {
                errors.Add("Sifre nisu iste");
            }

            if (errors.Count > 0)
            {
                string message = "";
                foreach (string error in errors)
                {
                    message += (error + "\n");
                }
                MessageBox.Show(message);
                return;
            }

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
