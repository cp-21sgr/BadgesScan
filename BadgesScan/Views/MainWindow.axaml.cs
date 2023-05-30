using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BadgesScan.Views
{
    public partial class MainWindow : Window
    {
        public DateTime newOddScan { get; set; }
        bool isOddScan = true;

        public MainWindow()
        {
            InitializeComponent();

            CultureInfo ci = new CultureInfo("en-GB");
            string date = DateTime.Now.ToString("dddd, dd MMMM yyyy", ci);
            Date.Content = char.ToUpper(date[0]) + date.Substring(1);

            TimeElapsed.IsVisible = false;
        }

        public async void ScanSimulation_Click(object sender, RoutedEventArgs e)
        {
            //do a scan
            ScanSimulation.IsEnabled = false;
            if (isOddScan)
            {
                TimeElapsed.IsVisible = false;
                newOddScan = DateTime.Now;
                ScanTime.Content = "Scan Time : " + newOddScan.ToString("HH:mm:ss");
                
                await Task.Delay(2500);

            } else
            {
                TimeElapsed.IsVisible = true;
                DateTime newEvenScan = DateTime.Now;
                ScanTime.Content = "Scan Time : " + newEvenScan.ToString("HH:mm:ss");
                TimeSpan timeElapsed = newEvenScan - newOddScan;
                TimeElapsed.Content = "Time Elapsed : " + $"{timeElapsed:hh\\:mm\\:ss}";
               
                await Task.Delay(4000);
            }
            isOddScan = !isOddScan;
            ScanTime.Content = "Time of the scan";
            TimeElapsed.IsVisible = false;
            ScanSimulation.IsEnabled = true;
        }
    }
}