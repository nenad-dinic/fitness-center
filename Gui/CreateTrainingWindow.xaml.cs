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
    /// <summary>
    /// Interaction logic for CreateTrainingWindow.xaml
    /// </summary>
    public partial class CreateTrainingWindow : Window
    {
        List<DataTypes.User> trainers;
        DataTypes.User trainer;
        public CreateTrainingWindow(DataTypes.User trainer)
        {
            InitializeComponent();

            this.trainer = trainer;

            if (trainer == null)
            {
                trainers = DataController.GetAllTrainers();
                foreach (DataTypes.User t in trainers)
                {
                    TrainerField.Items.Add(t.name + " - " + t.jmbg);
                }
            }
            else
            {
                TrainerField.IsEnabled = false;
                TrainerField.Items.Add(trainer.name + " - " + trainer.jmbg);
                TrainerField.SelectedIndex = 0;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();

            DateTime date = DateTime.Now;
            int hour = -1;
            int minute = -1;
            int duration = -1;
            DataTypes.User trainer = null;


            if (!DateTime.TryParse(DateField.Text, out date))
            {
                errors.Add("Datum nije validan");
                date = DateTime.Now;
            }

            if(!int.TryParse(HourField.Text, out hour))
            {
                errors.Add("Broj sati nije validan");
                hour = -1;
            }

            if (!int.TryParse(MinuteField.Text, out minute))
            {
                errors.Add("Broj minuta nije validan");
                minute = -1;
            }

            if (!int.TryParse(DurationField.Text, out duration))
            {
                errors.Add("Vreme trajanja nije validno");
                duration = -1;
            }

            if(this.trainer != null)
            {
                trainer = this.trainer;
            }
            else
            {
                if(TrainerField.SelectedIndex != -1)
                {
                    trainer = trainers[TrainerField.SelectedIndex];   
                }
            }

            if(trainer == null)
            {
                errors.Add("Morate izabrati trenera");
            }

            if(date <= DateTime.Now)
            {
                errors.Add("Datum mora biti u buducnosti");
            }

            if(hour < 0 || hour >= 24)
            {
                errors.Add("Opseg sati nije validan");
            }

            if(minute < 0 || minute >= 60 || minute % 5 != 0)
            {
                errors.Add("Opseg minuta nije validan (0-59min) i mora biti deljiv sa 5");
            }

            if(duration < 20 || duration > 120 || duration %10 != 0)
            {
                errors.Add("Vreme trajanja nije validno (20-120min) i mora biti deljivo sa 10");
            }

            if(trainer != null){
                List<DataTypes.Training> trainings = DataController.GetAllTrainingsForTrainer(trainer.id);

                DateTime trainginStartAt = date.AddHours(hour).AddMinutes(minute);
                DateTime trainingEndAt = trainginStartAt.AddMinutes(duration);
                foreach(DataTypes.Training t in trainings){
                    DateTime tempStartAt = t.date;
                    DateTime tempEndAt = t.date.AddMinutes(t.duration);

                    if( (trainginStartAt >= tempStartAt && trainginStartAt < tempEndAt) || (trainingEndAt > tempStartAt && trainingEndAt <= tempEndAt) || 
                    (tempStartAt >= trainginStartAt && tempStartAt < trainingEndAt) || (tempEndAt > trainginStartAt && tempEndAt <= trainingEndAt) ){
                        errors.Add("Vremenski period je vec zauzet!");
                        break;
                    }

                }

            }

            if (errors.Count > 0)
            {
                string message = "";
                foreach(string error in errors)
                {
                    message += (error + "\n");
                }
                MessageBox.Show(message);
                return;
            }

            date = date.AddHours(hour);
            date = date.AddMinutes(minute);

            DataController.CreateTraining(date, duration, trainer, null);

            MessageBox.Show("Trening uspesno kreiran");
            this.Close();

        }
    }
}
