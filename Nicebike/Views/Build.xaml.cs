using Nicebike.Models;
using System.Data;
using MySql.Data.MySqlClient;
using Nicebike.ViewModels;
namespace Nicebike.Views;

public partial class Build : ContentPage
{
    public int BuilderNumber { get; set; }
    public List<Bike> BikesForBuilder { get; set; }

    public Build(int builder)
    {
        InitializeComponent();

        BuilderNumber = builder;
        BuildManagement buildManagement = new BuildManagement();
        BikesForBuilder = buildManagement.BikesForBuilder(BuilderNumber);

        BindingContext = this;

        UpdateLabelText();
    }

    private void UpdateLabelText()
    {
        technicianLabel.Text = $"Technicien {BuilderNumber}";
    }
    private async void OnFinishedClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var idBike = (int)button.CommandParameter;
        BuildManagement buildManagement = new BuildManagement();

        buildManagement.FinishBike(idBike, BuilderNumber);

        var buildPage = new Build(BuilderNumber);
        

        await Navigation.PushAsync(buildPage);

        Navigation.RemovePage(this);
    }


}
