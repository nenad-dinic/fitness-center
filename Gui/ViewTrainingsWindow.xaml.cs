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
    public partial class ViewTrainingsWindow : Window
    {

        class Row
        {
            public int id { get; set; }
            public string trainer { get; set; }
            public DateTime date { get; set; }
            public int duration { get; set; }
            public string status { get; set; }
        }

        DataTypes.User trainer;
        DataTypes.User viewer;
        public ViewTrainingsWindow(DataTypes.User viewer, DataTypes.User trainer)
        {
            InitializeComponent();
            this.trainer = trainer;
            this.viewer = viewer;

            ShowTrainings();

        }

        void ShowTrainings()
        {
            List<DataTypes.Training> trainings;

            TrainingsTable.Items.Clear();

            if (viewer.userTypes == DataTypes.UserTypes.admin)
            {
                trainings = DataController.GetAllTrainings();
            }
            else
            {
                trainings = DataController.GetAllTrainingsForTrainer(trainer.id);
            }


            foreach (DataTypes.Training t in trainings)
            {
                TrainingsTable.Items.Add(new Row()
                {
                    id = t.id,
                    trainer = t.trainer.name + " " + t.trainer.surname,
                    date = t.date,
                    duration = t.duration,
                    status = (t.trainee != null ? "rezervisan" : "slobodan")
                });
            }

        }

        CreateTrainingWindow createTrainingWindow = null;
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (createTrainingWindow == null)
            {
                createTrainingWindow = new CreateTrainingWindow(trainer);
                createTrainingWindow.Show();
            }
            else
            {
                createTrainingWindow.Focus();
            }
        }

        public void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Row row = (Row)((Button)e.OriginalSource).DataContext;
            DataController.DeleteTraining(row.id);
            ShowTrainings();
        }

        public void ReserveBtn_Click(object sender, RoutedEventArgs e)
        {
            Row row = (Row)((Button)e.OriginalSource).DataContext;
            DataController.ReserveTraining(row.id, viewer);
            ShowTrainings();
        }

    }
}
