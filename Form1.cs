using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string APIKey = "0f70bc0b4720a557cd133e4fc4b1a61a";

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetWeather();
        }




        public void GetWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}",TBCity.Text,APIKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                PicIcon.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                labCondition.Text = Info.weather[0].main;
                LabDetails.Text = Info.weather[0].description;
                LabSunset.Text = ConvertDateTime(Info.sys.sunset).ToShortTimeString();
                LabSunrise.Text = ConvertDateTime(Info.sys.sunrise).ToShortTimeString();
                LabWindSpeed.Text = Info.wind.speed.ToString();
                LabPressure.Text = Info.main.pressure.ToString();

            }
        }
        DateTime ConvertDateTime(long millisec)
        {
            DateTime dt = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc).ToLocalTime();
            dt = dt.AddSeconds(millisec).ToLocalTime();
            return dt;
        }
        
    }
}
