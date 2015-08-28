using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AlexOdsSdkTest.Pages
{
    public class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            Children.Add(new PlayerView(){Title = "Player"});
            Children.Add(new ContentPage() { Title = "Another page" });
        }
    }
}
