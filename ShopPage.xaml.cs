using GhireanDariaLab7.Models;
using Microsoft.Maui.Devices.Sensors;
using Plugin.LocalNotification;
using System.Threading.Tasks.Dataflow;

namespace GhireanDariaLab7;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}
	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var shop = (Shop)BindingContext;
		await App.Database.SaveShopAsync(shop);
		await Navigation.PopAsync();
	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
        var shop = (Shop)BindingContext;
		bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this item?", "Yes", "No"); 
		if (confirm) {
			await App.Database.DeleteShopAsync(shop);
			await Navigation.PopAsync();
		}

    }

	async void OnShowMapButtonClicked(object sender, EventArgs e)
	{
		var shop = (Shop)BindingContext;
		var address = shop.Adress;
		var options = new MapLaunchOptions { Name = "Magazinul meu preferat" };
        var shoplocation = new Location(46.7492379, 23.5745597); //windows machine

        var myLocation = new Location(46.7731796289, 23.6213886738); //windows machine
        var distance = myLocation.CalculateDistance(myLocation, DistanceUnits.Kilometers); 
		if (distance < 5) 
		{
			var request = new NotificationRequest
			{
				Title = "Ai de facut cumparaturi in apropiere!",
				Description = address,
				Schedule = new NotificationRequestSchedule 
				{
					NotifyTime = DateTime.Now.AddSeconds(1)
				}
			};
			LocalNotificationCenter.Current.Show(request);
		}

        await Map.OpenAsync(shoplocation, options);
    }

}