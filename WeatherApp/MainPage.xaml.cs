using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void On_Load(object sender, RoutedEventArgs e)
        {
            RootObject myIP = await API_JSON_Conversion.getIP();
            var lat = myIP.loc.Split(',')[0];
            var lon = myIP.loc.Split(',')[1];

            RootObject myWeather = await API_JSON_Conversion.getWeather(lat, lon);
  
            //Temp is set to Fahrenheit by default. This is due to me specifying units as imperial for API call in API_JSON_Conversion.
            //Casted temp as an int and then converted to string to remove decimals. (char)176 represents the degree symbol.
            WeatherData.Text = myIP.city + ", " + myIP.region + "\n" + ((int)myWeather.main.temp).ToString()+ (char)176 + "\n" + myWeather.weather[0].description;
        }
    }
}
