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
using SR44_2020_POP2021;

namespace SR44_2020_POP2021.Gui
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
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
            string name = NameField.Text;
            string surname = SurnameField.Text;
            string jmbg = JmbgField.Text;
            string password = PassField.Text;
            DataTypes.Genders gender = (DataTypes.Genders) GenderField.SelectedIndex;
            string street = StreetField.Text;
            string houseNum = HouseNumField.Text;
            string city = CityField.Text;
            string country = CountryField.Text;
            string email = EmailField.Text;

            int lastAddressId = DataController.GetLastAddressId();

            DataController.CreateUser(name, surname, jmbg, password, gender, DataController.CreateAddress(street, houseNum, city, country, false), email, DataTypes.UserTypes.trainee, new DateTime(0));
            
        }   
    }       
}
