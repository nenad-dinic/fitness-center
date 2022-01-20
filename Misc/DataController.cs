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

        static readonly string filePath = "../../Files/";

        static DataTypes.FitnessCenter center;

        static List<DataTypes.User> users;

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
            ReadAllTrainings();
        }

        static void CreateFile(string path){
            if(!Directory.Exists(Path.GetDirectoryName(path))){
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            File.Create(path);
        }

        public static void WriteAllAddresses()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.Address a in addresses)
            {
                lines.Add(a.id + "," + a.street + "," + a.houseNum + "," + a.city + "," + a.country + "," + a.isDeleted);
            }

            File.WriteAllLines(filePath + "addresses.txt", lines);
        }
        
        public static void WriteAllUsers()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.User u in users)
            {
                lines.Add(u.id + "," + u.name + "," + u.surname + "," + u.jmbg + "," + u.password + "," + u.gender + "," + u.address.id + "," + u.email + "," + u.userTypes + "," + u.isDeleted);
            }

            File.WriteAllLines(filePath + "users.txt", lines);
        }

        public static void WriteAllTrainings()
        {
            List<string> lines = new List<string>();

            foreach (DataTypes.Training t in trainings)
            {
                lines.Add(t.id + "," + t.date + "," + t.startTime + "," + t.duration + "," + t.isReserved + "," + t.trainer + "," + t.trainee + "," + t.isDeleted);
            }

            File.WriteAllLines(filePath + "trainings.txt", lines);
        }

        private static void ReadAllAddresses()
        {
            if (!File.Exists(filePath + "addresses.txt"))
            {
                CreateFile(filePath + "addresses.txt");
                return;
            }

            string[] lines = File.ReadAllLines(filePath + "addresses.txt");

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
            if (!File.Exists(filePath + "users.txt"))
            {
                CreateFile(filePath + "users.txt");
                return;
            }

            string[] lines = File.ReadAllLines(filePath + "users.txt");

            foreach (string l in lines)
            {
                string[] fields = l.Split(',');
                int id = int.Parse(fields[0]);
                string name = fields[1];
                string surname = fields[2];
                string jmbg = fields[3];
                string password = fields[4];
                DataTypes.Genders gender = (DataTypes.Genders) Enum.Parse(typeof(DataTypes.Genders), fields[5]);
                DataTypes.Address address = GetAddressById(int.Parse(fields[6]));
                string email = fields[7];
                DataTypes.UserTypes userType = (DataTypes.UserTypes) Enum.Parse(typeof(DataTypes.UserTypes), fields[8]);
                bool isDeleted = bool.Parse(fields[9]);

                users.Add(new DataTypes.User(id, name, surname, jmbg, password, gender, address, email, userType, isDeleted));
            }
        }

        private static void ReadAllTrainings()
        {
            if(!File.Exists(filePath + "trainings.txt")){
                CreateFile(filePath + "trainings.txt");
                return;
            }

            string[] lines = File.ReadAllLines(filePath + "trainings.txt");

            foreach(string l in lines){
                string[] fields = l.Split(',');
                int id = int.Parse(fields[0]);
                DateTime date = DateTime.Parse(fields[1]);
                TimeSpan startTime = TimeSpan.Parse(fields[2]);
                int duration = int.Parse(fields[3]);
                bool isReserved = bool.Parse(fields[4]);
                DataTypes.User trainer = GetUserById(int.Parse(fields[5]));
                DataTypes.User trainee = GetUserById(int.Parse(fields[6]));
                bool isDeleted = bool.Parse(fields[7]);
                trainings.Add(new DataTypes.Training(id, date, startTime, duration, isReserved, trainer, trainee, isDeleted));
            }
        }

        public static DataTypes.User CreateUser(string name, string surname, string jmbg, string password, DataTypes.Genders gender, DataTypes.Address address, string email, DataTypes.UserTypes userTypes)
        {
            DataTypes.User user = new DataTypes.User(GetLastUserId() + 1, name, surname, jmbg, password, gender, address, email, userTypes, false);
            users.Add(user);
            WriteAllUsers();
            return user;
        }

        public static DataTypes.Address CreateAddress(string street, string houseNum, string city, string country, bool isDeleted){
            DataTypes.Address address = new DataTypes.Address(GetLastAddressId() + 1, street, houseNum, city, country, isDeleted);
            addresses.Add(address);
            WriteAllAddresses();
            return address;
        }

        public static DataTypes.Training CreateTraining(DateTime date, TimeSpan startTime, int duration, bool isDeserved, DataTypes.User trainer, DataTypes.User trainee)
        {
            DataTypes.Training training = new DataTypes.Training(GetLastTrainingId() + 1, date, startTime, duration, isDeserved, trainer, trainee, false);
            trainings.Add(training);
            WriteAllTrainings();
            return training;
        }

        public static bool UpdateUser(int id, string name, string surname, string jmbg, string password, DataTypes.Genders gender, string email)
        {
            DataTypes.User user = GetUserById(id);
            if(user == null){
                return false;
            }

            user.Update(name, surname, jmbg, password, gender, email);

            WriteAllUsers();
            return true;
        }

        public static bool UpdateAddress(int id, string street, string houseNum, string city, string country){
            DataTypes.Address address = GetAddressById(id);

            if(address == null){
                return false;
            }

            address.Update(street, houseNum, city, country);

            WriteAllAddresses();
            return true;
        }

        public static bool UpdateTraining(int id, DateTime date, TimeSpan startTime, int duration, bool isReserved, DataTypes.User trainer, DataTypes.User trainee){
            DataTypes.Training training = GetTrainingById(id);

            if(training == null){
                return false;
            }

            training.Update(date, startTime, duration, isReserved, trainer, trainee);


            WriteAllTrainings();
            return true;
        }

        public static bool DeleteUser(int id)
        {
            foreach (DataTypes.User u in users)
            {
                if (u.id == id)
                {
                    u.Delete();
                    DeleteAddress(u.address.id);
                    WriteAllUsers();
                    return true;
                }
            }
            return false;
        }

        public static bool DeleteTraining(int id)
        {
            foreach (DataTypes.Training t in trainings)
            {
                if (t.id == id)
                {
                    t.Delete();
                    WriteAllTrainings();
                    return true;
                }
            }
            return false;
        }

        public static bool DeleteAddress(int id){
            foreach (DataTypes.Address a in addresses)
            {
                if (a.id == id)
                {
                    a.Delete();
                    WriteAllAddresses();
                    return true;
                }
            }
            return false;
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

        public static DataTypes.Training GetTrainingById(int id){
            foreach (DataTypes.Training t in trainings)
            {
                if (id == t.id)
                {
                    return t;
                }
            }
            return null;
        }

        public static DataTypes.Address GetAddressById(int id){
            foreach (DataTypes.Address a in addresses)
            {
                if (id == a.id)
                {
                    return a;
                }
            }
            return null;
        }

        public static DataTypes.User LoginUser(string jmbg, string password){
            foreach(DataTypes.User u in users){
                if (u.jmbg == jmbg && u.password == password){
                    return u;
                }
            }
            return null;
        }

        public static List<DataTypes.User> GetAllTrainers()
        {
            List<DataTypes.User> usr = new List<DataTypes.User>();

            foreach(DataTypes.User u in users)
            {
                if(u.userTypes == DataTypes.UserTypes.trainer)
                {
                    usr.Add(u);
                }
            }
            return usr;
        }

    }

}
