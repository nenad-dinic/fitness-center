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
    /// Interaction logic for UpdateCenterInfo.xaml
    /// </summary>
    public partial class UpdateCenterInfo : Window
    {
        DataTypes.FitnessCenter center = null;
        public UpdateCenterInfo()
        {
            InitializeComponent();
            center = DataController.GetFitnessCenterInfo();
            LoadData();
        }

        void LoadData()
        {
            CenterNameField.Text = center.name;
            StreetField.Text = center.address.street;
            HouseNumField.Text = center.address.houseNum;
            CityField.Text = center.address.city;
            CountryField.Text = center.address.country;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

            List<string> errors = new List<string>();

            string name = CenterNameField.Text;
            string street = StreetField.Text;
            string houseNum = HouseNumField.Text;
            string city = CityField.Text;
            string country = CountryField.Text;

            if (!Regex.Match(name, "^[A-Z][a-zA-Z -]*$").Success)
            {
                errors.Add("Ime centra nije validno");
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

            DataController.UpdateCenterInfo(name);
            DataController.UpdateAddress(center.address.id, street, houseNum, city, country);
            MessageBox.Show("Informacije uspesno sacuvane");
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
