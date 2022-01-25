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
using SR44_2020_POP2021;

namespace SR44_2020_POP2021.Gui
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        DataTypes.UserTypes userType;
        public RegisterWindow(DataTypes.UserTypes userType)
        {
            InitializeComponent();
            this.userType = userType;

            if(userType == DataTypes.UserTypes.trainer)
            {
                this.Title = "Dodaj Trenera";
                RegisterBtn.Content = "Dodaj";
            }

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GenderField.Items.Add("Musko");
            GenderField.Items.Add("Zensko");
            GenderField.Items.Add("Drugo");
            GenderField.SelectedIndex = 0;
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();

            string name = NameField.Text;
            string surname = SurnameField.Text;
            string jmbg = JmbgField.Text;
            string password = PassField.Password;
            string repeatPassword = RepeatPassField.Password;
            DataTypes.Genders gender = (DataTypes.Genders) GenderField.SelectedIndex;
            string street = StreetField.Text;
            string houseNum = HouseNumField.Text;
            string city = CityField.Text;
            string country = CountryField.Text;
            string email = EmailField.Text;

            if(!Regex.Match(name, "^[A-Z][a-z]*$").Success)
            {
                errors.Add("Ime nije validno");
            }

            if (!Regex.Match(surname, "^[A-Z][a-z]*$").Success)
            {
                errors.Add("Prezime nije validno");
            }

            if (!Regex.Match(jmbg, "^\\d{8}$").Success)
            {
                errors.Add("JMBG mora imati 8 cifara");
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

            int lastAddressId = DataController.GetLastAddressId();

            DataTypes.Address address =  DataController.CreateAddress(street, houseNum, city, country, false);
            DataController.CreateUser(name, surname, jmbg, password, gender, address, email, userType);
            
            MessageBox.Show("Registracija uspesna!");
            this.Close();
        }   
    }       
}
