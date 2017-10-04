using AirlineSurfaceDemo.FlightXML3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Flurl;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AirlineSurfaceDemo
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

        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async System.Threading.Tasks.Task FindFlight()
        {
            try
            {
                FlightXML3SoapClient client = new FlightXML3SoapClient();
                client.ClientCredentials.UserName.UserName = "domwill";
                client.ClientCredentials.UserName.Password = "###########";
                
                //Code locks here. Unsure why but UWP forces me to use the Async version of FlightInfoStatus
                FlightInfoStatusResponse response = await client.FlightInfoStatusAsync("VA912", false, "", 1, 0);

                foreach (var flight in response.FlightInfoStatusResult.flights)
                {
                    FlightDetails.Text = (String.Format("{0} ({1})\t{2} ({3}) Est Departure {4}, Est Arrival {5}", flight.ident, flight.aircrafttype,
                        flight.origin.airport_name, flight.origin.code, flight.estimated_departure_time.time, flight.estimated_arrival_time.time));
                }
            }
            catch (Exception ex)
            {
                FlightDetails.Text = String.Format("Error {0}", ex.Message);
            }
        }

        private void SearchFlight(object sender, RoutedEventArgs e)
        {
            FindFlight().Wait();

            #region hide test code
            //try
            //{
            //    string fxmlUrl = "http://flightxml.flightaware.com/json/FlightXML3";
            //    string username = "domwill";
            //    string apiKey = "94cbda5fc45127c0bb257d948ce2101c9641df78";

            //    var uriBuilder = new UriBuilder(fxmlUrl);
            //    var requestUrl = fxmlUrl
            //                    .AppendPathSegment("AirportInfo")
            //                    .SetQueryParams(new
            //                    {
            //                        airport_code = "KIAH"
            //                    });

            //    ProcessRepositories(requestUrl, username, apiKey).Wait();

            //}
            //catch (Exception ex)
            //{
            //    FlightDetails.Text = "Error retreving flight. Please try again";
            //}
            #endregion
        }

        private async Task ProcessRepositories(string apiUrl, string username, string apiKey)
        {
            var serializer = new DataContractJsonSerializer(typeof(AirportInfoResult));
            var client = new HttpClient();
            var credentials = Encoding.ASCII.GetBytes(username + ":" + apiKey);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));

            var streamTask = client.GetStreamAsync(apiUrl);
            var airportInfo = serializer.ReadObject(await streamTask) as AirportInfoResult;
            FlightDetails.Text = airportInfo.AirportResult.Name.ToString();

            //Console.WriteLine(airportInfo.AirportResult.Code);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
