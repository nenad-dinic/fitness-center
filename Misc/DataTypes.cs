﻿using System;
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
            public int id { get; private set; }
            public string street { get; private set; }
            public string houseNum { get; private set; }
            public string city { get; private set; }
            public string country { get; private set; }
            public bool isDeleted { get; private set; }
            public Address(int id, string street, string houseNum, string city, string country, bool isDeleted)
            {
                this.id = id;
                this.street = street;
                this.houseNum = houseNum;
                this.city = city;
                this.country = country;
                this.isDeleted = isDeleted;
            }

            public void Delete()
            {
                isDeleted = true;
            }

            public void Update(string street, string houseNum, string city, string country)
            {
                this.street = street;
                this.houseNum = houseNum;
                this.city = city;
                this.country = country;
            }
        }

        public class FitnessCenter
        {
            public string name { get; private set; }
            public Address address { get; private set; }
            public FitnessCenter(string name, Address address)
            {
                this.name = name;
                this.address = address;
            }

            public void Update(string name)
            {
                this.name = name;
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
            public int id { get; private set; }
            public string name { get; private set; }
            public string surname { get; private set; }
            public string jmbg { get; private set; }
            public string password {get; private set; }
            public Genders gender { get; private set; }
            public Address address { get; private set; }
            public string email { get; private set; }
            public UserTypes userTypes { get; private set; }
            public bool isDeleted { get; private set; }

            public User(int id, string name, string surname, string jmbg, string password, Genders gender, Address address, string email, UserTypes userTypes, bool isDeleted)
            {
                this.id = id;
                this.name = name;
                this.surname = surname;
                this.jmbg = jmbg;
                this.password = password;
                this.gender = gender;
                this.address = address;
                this.email = email;
                this.userTypes = userTypes;
                this.isDeleted = isDeleted;
            }

            public void Update(string name, string surname, string jmbg, string password, DataTypes.Genders gender, string email)
            {
                this.name = name;
                this.surname = surname;
                this.jmbg = jmbg;
                this.password = password;
                this.gender = gender;
                this.email = email;
            }

            public void Delete()
            {
                isDeleted = true;
            }
        }

        public class Training
        {
            public int id { get; private set; }
            public DateTime date { get; private set; }
            public int duration { get; private set; }
            public User trainer { get; private set; }
            public User trainee { get; private set; }
            public bool isDeleted { get; private set; }

            public Training(int id, DateTime date, int duration, DataTypes.User trainer, DataTypes.User trainee, bool isDeleted)
            {
                this.id = id;
                this.date = date;
                this.duration = duration;
                this.trainer = trainer;
                this.trainee = trainee;
                this.isDeleted = isDeleted;
            }

            public void Update(DateTime date, int duration, User trainer, User trainee){
                this.date = date;
                this.duration = duration;
                this.trainer = trainer;
                this.trainee = trainee;
            }

            public void Delete()
            {
                isDeleted = true;
            }
        }
    }
}