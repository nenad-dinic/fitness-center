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
            DateTime date = DateTime.Parse(DateField.Text);
            int hour = int.Parse(HourField.Text);
            int minute = int.Parse(MinuteField.Text);
            int duration = int.Parse(DurationField.Text);

            date = date.AddHours(hour);
            date = date.AddMinutes(minute);

            DataTypes.User trainer;

            if(this.trainer != null)
            {
                trainer = this.trainer;
            }
            else
            {
                trainer = trainers[TrainerField.SelectedIndex];
            }

            DataController.CreateTraining(date, duration, trainer, null);

            MessageBox.Show("Trening uspesno kreiran");
            this.Close();

        }
    }
}
