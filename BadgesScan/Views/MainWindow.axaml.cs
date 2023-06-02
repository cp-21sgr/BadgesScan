using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Drawing;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BadgesScan.Views
{
    public partial class MainWindow : Window
    {
        public DateTime newOddScan { get; set; }
        bool isOddScan = true;

        string apiUri = "https://157.26.121.123:7236/api/TimeWorks";
        HttpClient client;

        public MainWindow()
        {
            InitializeComponent();

            CultureInfo ci = new CultureInfo("en-GB");
            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy", ci);
            Date.Content = char.ToUpper(date[0]) + date.Substring(1);


            ScanSuccess.IsVisible = false;
            GreenScreen.IsVisible = false;

            client = new HttpClient();
        }

        public async void ScanSimulation_Click(object sender, RoutedEventArgs e)
        {
            //do a scan
            ScanSuccess.IsVisible = true;
            GreenScreen.IsVisible = true;
            ScanSimulation.IsEnabled = false;
            if (isOddScan)
            {
                newOddScan = DateTime.Now;
                ScanTime.Content = "Scan Time : " + newOddScan.ToString("HH:mm:ss");

                //await client.PostAsJsonAsync(apiUri, newOddScan);

                await Task.Delay(2500);

            } else
            {
                DateTime newEvenScan = DateTime.Now;
                ScanTime.Content = "Scan Time : " + newEvenScan.ToString("HH:mm:ss");
                TimeSpan timeElapsed = newEvenScan - newOddScan;
                TimeElapsed.Content = "Time Elapsed : " + $"{timeElapsed:hh\\:mm\\:ss}";

                //await client.PostAsJsonAsync(apiUri, newEvenScan);

                await Task.Delay(2500);
            }
            isOddScan = !isOddScan;
            ScanTime.Content = "Time of the scan";
            TimeElapsed.Content = "Time Elapsed";
            ScanSuccess.IsVisible = false;
            GreenScreen.IsVisible = false;
            ScanSimulation.IsEnabled = true;
        }
    }
}