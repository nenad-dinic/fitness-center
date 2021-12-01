using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR44_2020_POP2021
{
    public class DataTypes
    {
        public class Address
        {
            public int Id;
            public string Street;
            public string HouseNum;
            public string City;
            public string Country;
        }

        public class FitnessCenter
        {
            public int Id;
            public string Name;
            public Address Address;
        }

        public enum Genders
        {
            male, female, other
        }

        public enum UserTypes
        {
            admin, trainer, trainee
        }

        public class User
        {
            public int Id;
            public string Name;
            public string Surname;
            public string jbmg;
            public Genders genders;
            public Address address;
            public string email;
            public UserTypes userTypes;
            public DateTime lockedUntil;
        }

        public class Training
        {
            public int id;
            public DateTime date;
            public DateTime startTime;
            public int duration;
            public bool isReserved;
            public int idTrainer;
            public int idTrainee;

        }
    }
}
