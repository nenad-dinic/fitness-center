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
    public partial class ViewTrainers : Window
    {

        class Item{
            public int id { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string address { get; set; }
            public string email { get; set; }
        }

        public ViewTrainers()
        {
            InitializeComponent();
            //TrainerTable.Columns[0].Visibility = Visibility.Hidden;

            ShowTrainers();
        }

        void ShowTrainers()
        {
            List<DataTypes.User> trainers = DataController.GetAllTrainers();
            foreach (DataTypes.User trainer in trainers)
            {
                string address = trainer.address.street + " " + trainer.address.houseNum + ", " + trainer.address.city + ", " + trainer.address.country;
                TrainerTable.Items.Add(new Item() { id = trainer.id, name = trainer.name, surname = trainer.surname, address = address, email = trainer.email });
            }
        }

    }
}
