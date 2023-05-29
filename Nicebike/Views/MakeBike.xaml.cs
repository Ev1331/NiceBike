using Nicebike.Models;
using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.ViewModels;

namespace Nicebike.Views
{
    public partial class MakeBike : ContentPage
    {
        public int TechnicianNumber { get; set; }
        public List<Bike> Bikes { get; set; }
        public MakeBike(int technicianNumber)
        {
            MakeBikeManagement makeBikeManagement = new MakeBikeManagement();

            InitializeComponent();

            Bikes = makeBikeManagement.BikesToBuild();
            TechnicianNumber = technicianNumber;
            BindingContext = this;

            UpdateLabelText();
        }

        private void UpdateLabelText()
        {
            technicianLabel.Text = $"Technicien {TechnicianNumber}";
        }

        public void OnProcessingClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var idBike = (int)button.CommandParameter;
            MakeBikeManagement makeBikeManagement = new MakeBikeManagement();

            makeBikeManagement.ProcessBike(idBike, TechnicianNumber);
        }
    }
}