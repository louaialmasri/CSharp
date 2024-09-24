using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apiKey = "your_api_key";

        private string requestUrl = "https://api.openweathermap.org/data/2.5/weather";
        public MainWindow()
        {
            InitializeComponent();
            UpdateUI("Ingolstadt");
        }

        public void UpdateUI(string city)
        {
            WeatherMapResponse result = GetWeatherData(city);

            string finalImage = "sun.png";
            string currentWeather = result.weather[0].main.ToLower();

            if (currentWeather.Contains("cloud"))
            {
                finalImage = "cloud.png";
            }
            else if (currentWeather.Contains("rain"))
            {
                finalImage = "rain.png";
            }
            else if (currentWeather.Contains("snow"))
            {
                finalImage = "snow.png";
            }

            backgroundImage.ImageSource = new BitmapImage(new Uri("Images/" + finalImage, UriKind.Relative));

            labelTemperature.Content = result.main.temp.ToString("F1") + "°C";
            Info.Content = result.weather[0].main;
        }

        public WeatherMapResponse GetWeatherData(string city)
        {
            HttpClient httpClient = new HttpClient();

            var finalUri = requestUrl + "?q=" + city + "&appid=" + apiKey + "&units=metric";
            HttpResponseMessage httpresponse = httpClient.GetAsync(finalUri).Result;

            string response = httpresponse.Content.ReadAsStringAsync().Result;

            WeatherMapResponse weatherMapResponse = JsonConvert.DeserializeObject<WeatherMapResponse>(response);

            return weatherMapResponse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = textBoxQuery.Text;
            UpdateUI(query);
        }
    }
}
