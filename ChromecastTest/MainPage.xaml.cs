using Sharpcaster;
using Sharpcaster.Interfaces;
using Sharpcaster.Models.Media;

namespace ChromecastTest;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

		IChromecastLocator locator = new MdnsChromecastLocator();
		var chromeCasts = await locator.FindReceiversAsync();

		var client = new ChromecastClient();

		await client.ConnectChromecast(chromeCasts.First());
		_ = client.LaunchApplicationAsync("CC1AD845");

        var media = new Media
        {
            ContentUrl = "https://commondatastorage.googleapis.com/gtv-videos-bucket/CastVideos/mp4/DesigningForGoogleCast.mp4"
        };
        _ = await client.GetChannel<IMediaChannel>().LoadAsync(media);

    }
}

