using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TestFWPanel
{
    public class PageBase : Page
    {
        public enum PageDisplayType
        {
            None = 0,
            Display3x4 = 1,
            Display4x3 = 2,
            Display6x2 = 3
        }

        #region PageDisplay

        /// <summary>
        /// PageDisplay Dependency Property
        /// </summary>
        public static readonly DependencyProperty PageDisplayProperty =
            DependencyProperty.Register("PageDisplay", typeof(PageDisplayType), typeof(MainPage),
                new PropertyMetadata(PageBase.PageDisplayType.None, OnPageDisplayChanged));

        /// <summary>
        /// Gets or sets the PageDisplay property. This dependency property 
        /// indicates the number of rows and columns to set in the FluidWrapPanel.
        /// </summary>
        public PageBase.PageDisplayType PageDisplay
        {
            get { return (PageBase.PageDisplayType)GetValue(PageDisplayProperty); }
            set { SetValue(PageDisplayProperty, value); }
        }

        /// <summary>
        /// Handles changes to the PageDisplay property.
        /// </summary>
        /// <param name="d">MainPage</param>
		/// <param name="e">DependencyProperty changed event arguments</param>
        private static void OnPageDisplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var page = (PageBase)d;
            var oldPageDisplay = (PageBase.PageDisplayType)e.OldValue;
            var newPageDisplay = page.PageDisplay;
            page.OnPageDisplayChanged(oldPageDisplay, newPageDisplay);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the PageDisplay property.
        /// </summary>
		/// <param name="oldPageDisplay">Old Value</param>
		/// <param name="newPageDisplay">New Value</param>
        async void OnPageDisplayChanged(PageBase.PageDisplayType oldPageDisplay, PageBase.PageDisplayType newPageDisplay)
        {
            switch (newPageDisplay)
            {
                case PageBase.PageDisplayType.Display3x4:
                    rows = 3;
                    columns = 4;
                    break;
                case PageBase.PageDisplayType.Display4x3:
                    rows = 4;
                    columns = 3;
                    break;
                case PageBase.PageDisplayType.Display6x2:
                    rows = 6;
                    columns = 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newPageDisplay), newPageDisplay, null);
            }

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, RefreshPanel);
        }

        #endregion

        protected double rows = 0;
        protected double columns = 0;

        protected virtual void RefreshPanel()
        {
            
        }
    }
}
