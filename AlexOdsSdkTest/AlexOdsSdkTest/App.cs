using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexOdsSdkTest.Controls;
using AlexOdsSdkTest.Helpers;
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

            var currentStateLabel = new Label() { TextColor = Color.Red, BackgroundColor = Color.Black };
            //currentStateLabel.BindingContext = mediaPlayer;
            //currentStateLabel.SetBinding(Label.TextProperty, "CurrentPlayerState", BindingMode.Default, new PlayerStateConverter());

            var playPauseBtn = new Button() {Text = "Pause"};
	        var nextBtn = new Button() {Text = "Next"};
            var prevBtn = new Button() { Text = "Prev" };

	        var informLayout = new StackLayout
	        {
                Spacing = 20,
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                IsVisible = false,
                Children = { clipTitleLabel, currentStateLabel, playPauseBtn, nextBtn, prevBtn }
	        };

            var button = new Button()
            {
                Text = "Load play list and play",
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

            playerSlider.BindingContext = mediaPlayer;
            playerSlider.SetBinding(Slider.MaximumProperty, "CurrentVideoDuration", BindingMode.Default, new TimeConverter());
            //playerSlider.SetBinding(Slider.ValueProperty, "Position", BindingMode.TwoWay, new TimeConverter());


            mediaPlayer.CurrentStateChanged += delegate(object sender, StateChangedEventArgs e)
            {
                if (e.NewAction == HyperMediaStateAction.PlayerPlayedToEndOfVideo)
                {
                    mediaPlayer.Next();
                }
                currentStateLabel.Text = mediaPlayer.CurrentPlayerState.ToString();
                if (mediaPlayer.CurrentPlayerState == HyperMediaPlayerState.Playing)
                    playPauseBtn.Text="Pause";
                else playPauseBtn.Text = "Play"; ;
            };

	        mediaPlayer.PlaylistItemLoaded += delegate(object sender, EventArgs e)
	        {
	            if (!informLayout.IsVisible) informLayout.IsVisible = true;
                if (mediaPlayer.CurrentItem == null) return;
	            clipTitleLabel.Text = mediaPlayer.CurrentItem.Title;
	        };

	        mediaPlayer.PositionChanged += delegate(object sender, PositionChangedArgs e)
	        {
	            playerSlider.Value = e.NewPosition.TotalMilliseconds;
	        };

	        nextBtn.Clicked += delegate(object sender, EventArgs e)
	        {
                mediaPlayer.Next();
	        };
            prevBtn.Clicked += delegate(object sender, EventArgs e)
            {
                mediaPlayer.Prev();
            };

            playPauseBtn.Clicked += delegate(object sender, EventArgs e)
            {
                if (mediaPlayer.CurrentPlayerState == HyperMediaPlayerState.Playing)
                    mediaPlayer.Pause();
                else mediaPlayer.Play();
            };

            return new ContentPage
            {
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        informLayout,
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
