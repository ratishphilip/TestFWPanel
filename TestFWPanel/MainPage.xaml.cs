using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WPFSpark;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestFWPanel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Random _random = new Random();
        private Brush[] _brushes;
        private double rows;
        private double columns;

        public MainPage()
        {
            this.InitializeComponent();

            _brushes = new Brush[] {
                                        new SolidColorBrush(Color.FromArgb(255, 76, 217, 100)),
                                        new SolidColorBrush(Color.FromArgb(255, 0, 122, 255)),
                                        new SolidColorBrush(Color.FromArgb(255, 255, 150, 0)),
                                        new SolidColorBrush(Color.FromArgb(255, 255, 45, 85)),
                                        new SolidColorBrush(Color.FromArgb(255, 88, 86, 214)),
                                        new SolidColorBrush(Color.FromArgb(255, 255, 204, 0)),
                                        new SolidColorBrush(Color.FromArgb(255, 142, 142, 147)),
                                      };

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new ObservableCollection<UIElement>();
            var count = 12;
            for (var i = 0; i < count; i++)
            {
                var brush = _brushes[_random.Next(_brushes.Length)];

                var ctrl = new FluidItemControl
                {
                    Width = 10,
                    Height = 10,
                    Fill = brush,
                    Data = (i + 1).ToString()
                };

                items.Add(ctrl);
            }

            fwPanel.ItemsSource = items;

            LandscapeRB.IsChecked = true;
        }

        private void OnLandscape(object sender, RoutedEventArgs e)
        {
            rows = 3;
            columns = 4;
            fwPanel.Width = 300;
            fwPanel.Height = 200;
        }

        private void OnPortrait(object sender, RoutedEventArgs e)
        {
            rows = 4;
            columns = 3;
            fwPanel.Width = 200;
            fwPanel.Height = 300;
        }

        private void OnPhone(object sender, RoutedEventArgs e)
        {
            rows = 6;
            columns = 2;
            fwPanel.Width = 150;
            fwPanel.Height = 250;
        }

        private void OnFWPSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((rows.IsZero()) || (columns.IsZero()))
                return;

            var width = fwPanel.Width / columns;
            var height = fwPanel.Height / rows;

            foreach (var child in fwPanel.FluidItems.OfType<FluidItemControl>())
            {
                child.Width = width;
                child.Height = height;
            }

            fwPanel.ItemWidth = width;
            fwPanel.ItemHeight = height;

            fwPanel.InvalidateMeasure();
        }
    }
}
