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
            public string date { get; set; }
            public int duration { get; set; }
            public string status { get; set; }
            public string trainee { get; set; }
            public bool canReserve { get; set; }
            public bool canDelete { get; set; }
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

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;

            string dateString;

            if(!DateTime.TryParse(DateField.Text, out date))
            {
                dateString = "";
            }
            else
            {
                dateString = date.Date.ToString();
            }

            ShowTrainings(NameField.Text, dateString, HourField.Text, MinuteField.Text, DurationField.Text);
        }

        void ShowTrainings(string trainerNameFilter = "", string dateFilter = "", string hourFilter = "", string minuteFilter = "", string durationFilter = "")
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

            if(viewer.userTypes == DataTypes.UserTypes.admin || viewer.userTypes == DataTypes.UserTypes.trainer)
            {
                TrainingsTable.Columns[7].Visibility = Visibility.Hidden;
            }else if(viewer.userTypes == DataTypes.UserTypes.trainee)
            {
                CreateBtn.Visibility = Visibility.Hidden;
                TrainingsTable.Columns[6].Visibility = Visibility.Hidden;
            }

            foreach (DataTypes.Training t in trainings)
            {
                if (t.trainer.name.ToLower().Contains(trainerNameFilter.ToLower()) && t.date.Date.ToString().Contains(dateFilter) && t.date.Hour.ToString().Contains(hourFilter) && t.date.Minute.ToString().Contains(minuteFilter) && t.duration.ToString().Contains(durationFilter))
                {
                    TrainingsTable.Items.Add(new Row()
                    {
                        id = t.id,
                        trainer = t.trainer.name + " " + t.trainer.surname,
                        date = t.date.ToString(),
                        duration = t.duration,
                        status = (t.trainee != null ? "rezervisan" : "slobodan"),
                        trainee = (t.trainee != null ? t.trainee.name + " " + t.trainee.surname : "/"),
                        canReserve = (t.trainee == null ? true : false),
                        canDelete = (viewer.userTypes == DataTypes.UserTypes.admin ? true : (t.trainee == null ? true : false)),
                    });
                }
            }

        }

        CreateTrainingWindow createTrainingWindow = null;
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (createTrainingWindow == null)
            {
                createTrainingWindow = new CreateTrainingWindow(trainer);
                createTrainingWindow.Closed += delegate { 
                    createTrainingWindow = null;
                    ShowTrainings();
                };
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

        private void Window_Closed(object sender, EventArgs e)
        {
            if(createTrainingWindow != null)
            {
                createTrainingWindow.Close();
            }
        }
    }
}
