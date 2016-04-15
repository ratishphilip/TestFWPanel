using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
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
            SizeChanged += MainPage_SizeChanged;
        }

        private async void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                fwPanel.Width = ContainerGrid.ActualWidth;
                fwPanel.Height = ContainerGrid.ActualHeight;
                RefreshPanel();
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new ObservableCollection<UIElement>();
            var count = 12;
            var prev = 0;
            var next = 0;
            for (var i = 0; i < count; i++)
            {
                while (next == prev)
                    next = _random.Next(_brushes.Length);

                var brush = _brushes[next];

                var ctrl = new FluidItemControl
                {
                    Width = 10,
                    Height = 10,
                    Fill = brush,
                    Data = (i + 1).ToString()
                };

                items.Add(ctrl);
                prev = next;
            }

            fwPanel.ItemsSource = items;

            rows = 3;
            columns = 4;
            LandscapeRB.IsChecked = true;
        }

        private async void OnLandscape(object sender, RoutedEventArgs e)
        {
            rows = 3;
            columns = 4;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, RefreshPanel);
        }

        private async void OnPortrait(object sender, RoutedEventArgs e)
        {
            rows = 4;
            columns = 3;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, RefreshPanel);
        }

        private async void OnPhone(object sender, RoutedEventArgs e)
        {
            rows = 6;
            columns = 2;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, RefreshPanel);
        }

        private void OnFWPSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshPanel();
        }

        private void RefreshPanel()
        {
            if ((rows.IsZero()) || (columns.IsZero()))
                return;

            var width = Math.Floor(fwPanel.Width / columns);
            var height = Math.Floor(fwPanel.Height / rows);

            foreach (var child in fwPanel.FluidItems.OfType<FluidItemControl>())
            {
                child.Width = width;
                child.Height = height;
            }

            fwPanel.ItemWidth = width;
            fwPanel.ItemHeight = height;
        }
    }
}
