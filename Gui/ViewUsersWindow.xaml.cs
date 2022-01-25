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

    class Row
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
        public string email { get; set; }
    }

    public partial class ViewUsersWindow : Window
    {
        public ViewUsersWindow()
        {
            InitializeComponent();
            ShowUsers();
        }

        void ShowUsers()
        {
            UsersTable.Items.Clear();
            List<DataTypes.User> users = DataController.GetAllUsers();
            foreach (DataTypes.User user in users)
            {
                string address = user.address.street + " " + user.address.houseNum + ", " + user.address.city + ", " + user.address.country;
                UsersTable.Items.Add(new Row() { id = user.id, name = user.name, surname = user.surname, address = address, email = user.email });
            }
        }

        UpdateUserWindow updateWindow = null;

        void UpdateSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            Row row = (Row)((Button)e.OriginalSource).DataContext;
            DataTypes.User user = DataController.GetUserById(row.id);
            if(updateWindow == null)
            {
                updateWindow = new UpdateUserWindow(user);
                updateWindow.Closed += delegate { 
                    updateWindow = null;
                    ShowUsers();
                };
                updateWindow.Show();
            }
            else
            {
                updateWindow.Focus();
            }
        }

        void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            Row row = (Row)((Button)e.OriginalSource).DataContext;
            DataController.DeleteUser(row.id);
            ShowUsers();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(updateWindow != null)
            {
                updateWindow.Close();
            }
        }
    }

}
