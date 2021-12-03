using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SR44_2020_POP2021
{
    public class DataCotroller
    {
        static DataTypes.FitnessCenter center;

        static List<DataTypes.User> users;

        static List<DataTypes.Training> trainings;

        static List<DataTypes.Address> addresses;

        public static void WriteAllAddresses()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.Address a in addresses)
            {
                lines.Add(a.id + "," + a.street + "," + a.houseNum + "," + a.city + "," + a.country);
            }

            File.WriteAllLines("addresses.txt", lines);
        }
        
        public static void WriteAllUsers()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.User u in users)
            {
                lines.Add(u.id + "," + u.name + "," + u.surname + "," + u.jmbg + "," + u.gender + "," + u.address.id + "," + u.email + "," + u.userTypes + "," + u.lockedUntil);
            }

            File.WriteAllLines("users.txt", lines);
        }

        public static void Init()
        {
            center = new DataTypes.FitnessCenter(0, "Genericki Fitnes Centar", new DataTypes.Address(0, "Ulica", "5", "Novi Sad", "Srbija"));
            users = new List<DataTypes.User>();
            trainings = new List<DataTypes.Training>();
            addresses = new List<DataTypes.Address>();
        }

        public static void CreateUser(int id, string name, string surname, string jmbg, DataTypes.Genders gender, DataTypes.Address address, string email, DataTypes.UserTypes userTypes, DateTime lockedUntil)
        {
            addresses.Add(address);
            DataTypes.User user = new DataTypes.User(id, name, surname, jmbg, gender, address, email, userTypes, lockedUntil);
            users.Add(user);

            WriteAllUsers();
            WriteAllAddresses();
        }
    }

}
