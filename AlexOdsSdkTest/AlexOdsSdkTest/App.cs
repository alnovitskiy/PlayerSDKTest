using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODS.SDK.Mobile.Shared.Controls;
using ODS.Infrastructure.HyperMedia;

using Xamarin.Forms;

namespace AlexOdsSdkTest
{
	public class App : Application
	{
	    private HyperMediaPlayer mediaPlayer;

		public App ()
		{
		    var button = new Button()
		    {
		        Text = "Play", 
                TextColor = Color.White, 
                BackgroundColor = Color.Gray,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
		    };

		    button.Clicked += OnButtonClicked;

            mediaPlayer = new HyperMediaPlayer();
            mediaPlayer.BackgroundColor = Color.Green;
		    mediaPlayer.VerticalOptions = LayoutOptions.FillAndExpand;
		    mediaPlayer.HorizontalOptions = LayoutOptions.FillAndExpand;
            mediaPlayer.CurrentStateChanged += delegate(object sender, StateChangedEventArgs e)
            {
                if (e.NewAction == HyperMediaStateAction.PlayerPlayedToEndOfVideo)
                {
                    var playlist = mediaPlayer.Playlist;
                    if (playlist != null)
                    {
                        var nextItemIndex = (playlist.CurrentItemIndex + 1) % playlist.ItemCount;
                        playlist.CurrentItem = playlist.Items[nextItemIndex];
                    }
                }
            };


			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
					Children = {
						button,
                        mediaPlayer
					}
				},
                Padding = new Thickness(0,20,0,0)
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
