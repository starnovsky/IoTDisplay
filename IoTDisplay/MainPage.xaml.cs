using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTDisplay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // create Flickr feed reader for unicorn images
            var reader = new FlickrReader("unicorn");

            TimeSpan period = TimeSpan.FromSeconds(5);
            // display neew images every five seconds
            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(
                async (source) =>
                {
                    // get next image
                    var image = await reader.GetImage();

                    if (image != null)
                    {
                        // we have to update UI in UI thread only
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () =>
                        {
                            // create and load bitmap
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.UriSource = new Uri(image, UriKind.Absolute);
                            // display image
                            splashImage.Source = bitmap;
                        }
                        );
                    }
                }, period);
        }
    }
}
