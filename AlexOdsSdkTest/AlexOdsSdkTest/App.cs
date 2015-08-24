using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexOdsSdkTest.Controls;
using ODS.SDK.Mobile.Shared.Controls;
using ODS.Infrastructure.HyperMedia;

using Xamarin.Forms;

namespace AlexOdsSdkTest
{
	public class App : Application
	{
	    private Player mediaPlayer;

		public App ()
		{
		    MainPage = CreateTestPage();
		}

	    private ContentPage CreateTestPage()
	    {
	        var clipTitleLabel = new Label()
	        {
	            Text = "CLIP TITLE",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Color.White,
                BackgroundColor = Color.Black,
                XAlign = TextAlignment.Center
	        };
            var button = new Button()
            {
                Text = "Play",
                TextColor = Color.White,
                BackgroundColor = Color.Gray,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            button.Clicked += OnButtonClicked;

	        var playerSlider = new Slider()
	        {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
	            Value = 0
	        };

            mediaPlayer = new Player();
            mediaPlayer.CurrentStateChanged += delegate(object sender, StateChangedEventArgs e)
            {
                if (e.NewAction == HyperMediaStateAction.PlayerPlayedToEndOfVideo)
                {
                    mediaPlayer.Next();
                }
            };

	        mediaPlayer.PlaylistItemLoaded += delegate(object sender, EventArgs e)
	        {
                if (mediaPlayer.CurrentItem == null) return;
                playerSlider.Maximum = mediaPlayer.CurrentVideoDuration.TotalMilliseconds;
                if (mediaPlayer.CurrentItem==null) return;
	            clipTitleLabel.Text = mediaPlayer.CurrentItem.Title;

	        };

	        mediaPlayer.PositionChanged += delegate(object sender, PositionChangedArgs e)
	        {
	            playerSlider.Value = e.NewPosition.TotalMilliseconds;
	        };


            return new ContentPage
            {
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        clipTitleLabel,
						button,
                        playerSlider,
                        mediaPlayer,
					}
                },
                Padding = new Thickness(0, 20, 0, 0)
            };
	        
	    }

	    private void OnButtonClicked(object sender, EventArgs e)
	    {
            var items = new List<PlaylistItem>();

            items.Add(new PlaylistItem
            {
                Title = "Test1",
                ServerAddress = "http://synergygrid.smartarrow.aware.net/wms/2/177639/106547085.mp4",
                AutoPlay = true
            });
            items.Add(new PlaylistItem
            {
                Title = "Test2",
                ServerAddress = "http://synergygrid.smartarrow.aware.net/wms/2/177635/106490070.mp4",
                AutoPlay = true
            });
            items.Add(new PlaylistItem
            {
                Title = "Test3",
                ServerAddress = "http://synergygrid.smartarrow.aware.net/wms/2/177635/106490481.mp4",
                AutoPlay = true
            });

            var hyperPlaylist = new ODS.Infrastructure.HyperMedia.Playlist { Items = items.ToArray() };

            mediaPlayer.LoadPlayList(hyperPlaylist);
            mediaPlayer.Play();

	    }


	}
}
