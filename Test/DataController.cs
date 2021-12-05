using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SR44_2020_POP2021
{
    public class DataController
    {
        static DataTypes.FitnessCenter center;

        public static List<DataTypes.User> users {get; private set;}

        static List<DataTypes.Training> trainings;

        static List<DataTypes.Address> addresses;
        public static void Init()
        {
            center = new DataTypes.FitnessCenter(0, "Genericki Fitnes Centar", new DataTypes.Address(0, "Ulica", "5", "Novi Sad", "Srbija", false));
            users = new List<DataTypes.User>();
            trainings = new List<DataTypes.Training>();
            addresses = new List<DataTypes.Address>();

            ReadAllAddresses();
            ReadAllUsers();
        }

        public static void WriteAllAddresses()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.Address a in addresses)
            {
                lines.Add(a.id + "," + a.street + "," + a.houseNum + "," + a.city + "," + a.country + "," + a.isDeleted);
            }

            File.WriteAllLines("addresses.txt", lines);
        }
        
        public static void WriteAllUsers()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.User u in users)
            {
                lines.Add(u.id + "," + u.name + "," + u.surname + "," + u.jmbg + "," + u.gender + "," + u.address.id + "," + u.email + "," + u.userTypes + "," + u.lockedUntil + "," + u.isDeleted);
            }

            File.WriteAllLines("users.txt", lines);
        }

        public static void WriteAllTrainings()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.Training t in trainings)
            {
                lines.Add(t.id + "," + t.date + "," + t.startTime + "," + t.duration + "," + t.isReserved + "," + t.trainer + "," + t.trainee + "," + t.isDeleted);
            }

            File.WriteAllLines("trainings.txt", lines);
        }

        private static void ReadAllAddresses()
        {
            if (!File.Exists("addresses.txt"))
            {
                return;
            }

            string[] lines = File.ReadAllLines("addresses.txt");

            foreach (string l in lines)
            {
                string[] fields = l.Split(',');
                int id = int.Parse(fields[0]);
                string street = fields[1];
                string houseNum = fields[2];
                string city = fields[3];
                string country = fields[4];
                bool isDeleted = bool.Parse(fields[5]);

                addresses.Add(new DataTypes.Address(id, street, houseNum, city, country, isDeleted));
            }
        }

        private static void ReadAllUsers()
        {
            if (!File.Exists("users.txt"))
            {
                return;
            }

            string[] lines = File.ReadAllLines("users.txt");

            foreach (string l in lines)
            {
                string[] fields = l.Split(',');
                int id = int.Parse(fields[0]);
                string name = fields[1];
                string surname = fields[2];
                string jmbg = fields[3];
                DataTypes.Genders gender = (DataTypes.Genders) Enum.Parse(typeof(DataTypes.Genders), fields[4]);
                int idAddress = int.Parse(fields[5]);
                string email = fields[6];
                DataTypes.UserTypes userType = (DataTypes.UserTypes) Enum.Parse(typeof(DataTypes.UserTypes), fields[7]);
                DateTime lockedUntil = DateTime.Parse(fields[8]);
                bool isDeleted = bool.Parse(fields[9]);

                DataTypes.Address address = null;
                foreach (DataTypes.Address a in addresses)
                {
                    if (a.id == idAddress)
                    {
                        address = a;
                        break;
                    }
                }

                users.Add(new DataTypes.User(id, name, surname, jmbg, gender, address, email, userType, lockedUntil, isDeleted));
            }
        }

        private static void ReadAllTrainings()
        {
            //Add read all trainings
        }


        public static void CreateUser(int id, string name, string surname, string jmbg, DataTypes.Genders gender, DataTypes.Address address, string email, DataTypes.UserTypes userTypes, DateTime lockedUntil)
        {
            addresses.Add(address);
            DataTypes.User user = new DataTypes.User(id, name, surname, jmbg, gender, address, email, userTypes, lockedUntil, false);
            users.Add(user);

            WriteAllUsers();
            WriteAllAddresses();
        }

        public static void CreateTraining(int id, DateTime date, TimeSpan startTime, int duration, bool isDeserved, DataTypes.User trainer, DataTypes.User trainee)
        {
            DataTypes.Training training = new DataTypes.Training(id, date, startTime, duration, isDeserved, trainer, trainee, false);
            trainings.Add(training);
            WriteAllTrainings();
        }

        public static void UpdateUser(int id, string name, string surname, string jmbg, DataTypes.Genders gender, DataTypes.Address address, string email, DataTypes.UserTypes userTypes, DateTime lockedUntil)
        {

        }

        public static void DeleteUser(int id)
        {
            foreach (DataTypes.User u in users)
            {
                if (u.id == id)
                {
                    u.Delete();
                    u.address.Delete();
                    WriteAllAddresses();
                    WriteAllUsers();
                    return;
                }
            }
        }

        public static void DeleteTraining(int id)
        {
            foreach (DataTypes.Training t in trainings)
            {
                if (t.id == id)
                {
                    t.Delete();
                    WriteAllTrainings();
                    return;
                }
            }
        }

        public static int GetLastUserId()
        {
            if (users.Count == 0)
            {
                return - 1;
            }
            return users[users.Count - 1].id;
        }

        public static int GetLastAddressId()
        {
            if (addresses.Count == 0)
            {
                return - 1;
            }
            return addresses[addresses.Count - 1].id;
        }

        public static int GetLastTrainingId()
        {
            if (trainings.Count == 0)
            {
                return - 1;
            }
            return trainings[trainings.Count - 1].id;
        }

        public static DataTypes.User GetUserById(int id)
        {
            foreach (DataTypes.User u in users)
            {
                if (id == u.id)
                {
                    return u;
                }
            }
            return null;
        }
    }

}
