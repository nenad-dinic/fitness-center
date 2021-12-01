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
        List<DataTypes.User> users;
        public static void CreateFile()
        {
            File.Create("test.txt");
        }
    }
}
