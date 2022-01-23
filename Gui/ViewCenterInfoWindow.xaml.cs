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
    /// Interaction logic for ViewCenterInfoWindow.xaml
    /// </summary>
    public partial class ViewCenterInfoWindow : Window
    {
        public ViewCenterInfoWindow()
        {
            InitializeComponent();
            DataTypes.FitnessCenter center = DataController.GetFitnessCenterInfo();
            CenterNameLabel.Content = center.name;
            CenterAddressLabel.Content = center.address.street + " " + center.address.houseNum + ", " + center.address.city + ", " + center.address.country;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
