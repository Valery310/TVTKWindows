using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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

namespace TVTKWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {

        public IPAddress iPAddressServer;
        public Uri addressServer;
        public IPAddress iPAddressLocal;
        public int portServer;

        public MainWindow()
        {
            InitializeComponent();

            portServer = Properties.Settings.Default.Port;
            addressServer = new UriBuilder("https",Properties.Settings.Default.Server, portServer).Uri;
            tbxIPServer.Text = Properties.Settings.Default.Server;

            var t = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            iPAddressLocal = t.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork 
            && !IPAddress.IsLoopback(ip) 
            && !ip.ToString().StartsWith("169.254."));
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Server = iPAddressServer.ToString();

            // webClient.DownloadFile(new Uri("https://yadi.sk/d/BvZh_cvnseoyF"), System.IO.Path.Combine(path.Text, System.IO.Path.GetFileName("zerahypt.zip")));
            //var FullVimeoUrl = "https://pdlvimeocdn-a.akamaihd.net/36507/517/262124023.mp4?token2=1428493810_25ff23eaeb9489b76649aaaf3e7fa438&aksessionid=028dcbd66a5e2a5a"
            //mediaElement.Source = new Uri(FullVimeoUrl.ToString(), UriKind.RelativeOrAbsolute);
            //mediaElement.Play();

            //  meContent.Source=

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(addressServer + "getmultimedia");

            if (response.IsSuccessStatusCode)
            {
               var product = await response.Content.ReadAsStringAsync();
                var obj = System.Text.Json.JsonSerializer.Deserialize<List<List<MultimediaFile>>>(product);
            }

            //using (HttpClient client = new HttpClient()) для скачивания файлов 
            //{
            //    using (HttpResponseMessage response = await client.GetAsync(url))
            //    using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
            //    {
            //    }
            //}

            //var values = new Dictionary<string, string> для пост запросов
            //{
            //    { "thing1", "hello" },
            //    { "thing2", "world" }
            //};

            //var content = new FormUrlEncodedContent(values);

            //var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

            //var responseString = await response.Content.ReadAsStringAsync();



            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri($"http://{iPAddressServer}:{portServer}");
            //HttpResponseMessage response = client.GetAsync("getmultimedia").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    string result = response.Content.ReadAsStringAsync().Result;
            //    var obj = System.Text.Json.JsonSerializer.Deserialize<List<List<MultimediaFile>>>(result);
            //}
            //else
            //{
            //    MessageBox.Show(response.StatusCode.ToString());
            //}

            //lvwServer.ItemsSource = obj.servers;
            //lvwServer.DisplayMemberPath = "dns";
        }

        private void btnSaveServer_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Server = tbxIPServer.Text;
            Properties.Settings.Default.Port = int.Parse(tbxPortServer.Text);
        }
    }
}
