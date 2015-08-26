using System;
using System.Collections.Generic;
using System.Text;
using ODS.SDK.Mobile.Shared.Controls;
using Xamarin.Forms;

namespace AlexOdsSdkTest.Controls
{
    public class Player : HyperMediaPlayer
    {
        /// <summary>
        /// Occurs when position changed from outside of player.
        /// </summary>
        public event EventHandler<PositionChangedArgs> PositionChangedManually;

        public Player()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Green;
        }

        public void Next()
        {
            var playlist = Playlist;
            if (playlist != null)
            {
                var nextItemIndex = (playlist.CurrentItemIndex + 1) % playlist.ItemCount;
                playlist.CurrentItem = playlist.Items[nextItemIndex];
                Play();
            }
        }

        public void MoveByIndex(int index)
        {
            if (index >= Playlist.Items.Length) return;
            CurrentItem = Playlist.Items[index];
            Play();
        }

        public void Prev()
        {
            if (Playlist.CurrentItemIndex == 0) return;
            CurrentItem = Playlist.Items[Playlist.CurrentItemIndex - 1];
            Play();
        }

        public void Replay()
        {
            while (Position.TotalMilliseconds > 0) SeekBy(new TimeSpan(0, 0, -3));
            if (CurrentPlayerState != HyperMediaPlayerState.Playing)
            {
                var handler = PositionChangedManually;
                if (handler != null)
                    handler(this, new PositionChangedArgs(Position));
                Play();
            }
        }

        public void JumpBySeconds(int sec, double durationMilliseconds)
        {
            var timeSeek = sec < 0
                ? (Position.TotalMilliseconds < -sec * 1000
                    ? new TimeSpan((long)(-Position.TotalMilliseconds * TimeSpan.TicksPerMillisecond))
                    : new TimeSpan(0, 0, sec))
                : (Position.TotalMilliseconds + 3000 > durationMilliseconds
                    ? new TimeSpan(
                        (long)
                            ((durationMilliseconds - Position.TotalMilliseconds) *
                             TimeSpan.TicksPerMillisecond))
                    : new TimeSpan(0, 0, sec));
            SeekBy(timeSeek);
            if (CurrentPlayerState != HyperMediaPlayerState.Playing)
            {
                var handler = PositionChangedManually;
                if (handler != null)
                    handler(this, new PositionChangedArgs(Position));
            }
        }

    }    
}
