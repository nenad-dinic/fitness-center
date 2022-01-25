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
    public partial class ViewReservationsWindow : Window
    {

        class Row
        {
            public int id { get; set; }
            public string trainer { get; set; }
            public DateTime date { get; set; }
            public int duration { get; set; }
        }

        DataTypes.User trainee;

        public ViewReservationsWindow(DataTypes.User trainee)
        {
            InitializeComponent();
            this.trainee = trainee;
            ShowTrainings();
        }

        public void ShowTrainings()
        {
            List<DataTypes.Training> trainings = DataController.GetAllTrainingsForTrainee(trainee.id);

            TrainingsTable.Items.Clear();

            foreach (DataTypes.Training t in trainings)
            {
                TrainingsTable.Items.Add(new Row()
                {
                    id = t.id,
                    trainer = t.trainer.name + " " + t.trainer.surname,
                    date = t.date,
                    duration = t.duration
                });
            }

        }

        public void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Row row = (Row)((Button)e.OriginalSource).DataContext;
            DataController.CancelTraining(row.id);
            ShowTrainings();
        }
    }



}
