using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexOdsSdkTest.Controls;
using AlexOdsSdkTest.Helpers;
using AlexOdsSdkTest.Pages;
using ODS.SDK.Mobile.Shared.Controls;
using ODS.Infrastructure.HyperMedia;

using Xamarin.Forms;

namespace AlexOdsSdkTest
{
	public class App : Application
	{
	    public App ()
		{
		    MainPage = new MainTabbedPage();
		}

	}
}
