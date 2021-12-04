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
            public int id;
            public string street;
            public string houseNum;
            public string city;
            public string country;
            public Address(int id, string street, string houseNum, string city, string country)
            {
                this.id = id;
                this.street = street;
                this.houseNum = houseNum;
                this.city = city;
                this.country = country;
            }
        }

        public class FitnessCenter
        {
            public int id;
            public string name;
            public Address address;
            public FitnessCenter(int id, string name, Address address)
            {
                this.id = id;
                this.name = name;
                this.address = address;
            }
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
            public int id;
            public string name;
            public string surname;
            public string jmbg;
            public Genders gender;
            public Address address;
            public string email;
            public UserTypes userTypes;
            public DateTime lockedUntil;

            public User(int id, string name, string surname, string jmbg, Genders gender, Address address, string email, UserTypes userTypes, DateTime lockedUntil)
            {
                this.id = id;
                this.name = name;
                this.surname = surname;
                this.jmbg = jmbg;
                this.gender = gender;
                this.address = address;
                this.email = email;
                this.userTypes = userTypes;
                this.lockedUntil = lockedUntil;
            }
        }

        public class Training
        {
            public int id;
            public DateTime date;
            public TimeSpan startTime;
            public int duration;
            public bool isReserved;
            public User trainer;
            public User trainee;

            public Training(int id, DateTime date, TimeSpan startTime, int duration, bool isReserved, DataTypes.User trainer, DataTypes.User trainee)
            {
                this.id = id;
                this.date = date;
                this.startTime = startTime;
                this.duration = duration;
                this.isReserved = isReserved;
                this.trainer = trainer;
                this.trainee = trainee;
            }
        }
    }
}