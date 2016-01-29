using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    //conversion for JSON data from API to C#
    public class API_JSON_Conversion
    {
        //obtains the IP address for the device accessing the app.
            public async static Task<RootObject> getIP()
            {
                var http = new HttpClient();
                var request = await http.GetAsync("http://ipinfo.io");
                var result = await request.Content.ReadAsStringAsync();
                var sr = new DataContractJsonSerializer(typeof(RootObject));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                var data = (RootObject)sr.ReadObject(ms);

                return data;
            }

        //Receives lat and lon after IP address check and uses this information to call the API.
            public async static Task<RootObject> getWeather(string lat, string lon)
            {
                var appID = "e2f9b02869ff22b8ba97de34f637cbab";
                var http = new HttpClient();
            //Set units as imperial by default.
                var request = await http.GetAsync("http://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+"&units=imperial&APPID="+appID);
                var result = await request.Content.ReadAsStringAsync();
                var sr = new DataContractJsonSerializer(typeof(RootObject));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                var data = (RootObject)sr.ReadObject(ms);

                return data;
            }
        }

        [DataContract]
        public class Weather
        {
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string main { get; set; }
            [DataMember]
            public string description { get; set; }
            [DataMember]
            public string icon { get; set; }
        }

        //primary interest -- has temperature
        [DataContract]
        public class Main
        {
            [DataMember]
            public double temp { get; set; }
            [DataMember]
            public int pressure { get; set; }
            [DataMember]
            public int humidity { get; set; }
            [DataMember]
            public double temp_min { get; set; }
            [DataMember]
            public double temp_max { get; set; }
        }

        [DataContract]
        public class RootObject
        {
            [DataMember]
            public List<Weather> weather { get; set; }
            [DataMember]
            public string @base { get; set; }
            [DataMember]
            public Main main { get; set; }
            [DataMember]
            public int dt { get; set; }
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int cod { get; set; }
            //Start of objects from ipinfo.io
            [DataMember]
            public string ip { get; set; }
            [DataMember]
            public string hostname { get; set; }
            [DataMember]
            public string city { get; set; }
            [DataMember]
            public string region { get; set; }
            [DataMember]
            public string country { get; set; }
            [DataMember]
            //loc contains the lat and lon of the current device's location
            public string loc { get; set; }
            [DataMember]
            public string org { get; set; }
            [DataMember]
            public string postal { get; set; }
        }
}
