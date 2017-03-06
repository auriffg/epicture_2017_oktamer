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
using epicture_2017.imgurAPI;
using epicture_2017.ViewModel;
// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace epicture_2017
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ImgurClient client = new ImgurClient(ConstantContainer.IMGUR_CLIENT_ID,
    ConstantContainer.IMGUR_CLIENT_SECRET);

            client.GetMainGalleryImages(ImgurGallerySection.Hot, ImgurGallerySort.Viral, 0, (s) =>
            {
                ImgurImageData data = s;
               // Debug.WriteLine(s.Images.First().AccountUrl);
            });
        }
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if ((MainPageViewModel.Instance.DeserializedHomeImages == null) !=
                (MainPageViewModel.Instance.DeserializedHomeImages.Count == 0))
            {
                App.ServiceClient.GetMainGalleryImages(ImgurGallerySection.Hot, ImgurGallerySort.Viral, 0, (s) =>
                {
                    ImgurImageData data = s;
                    MainPageViewModel.Instance.DeserializedHomeImages = new List<ImgurImage>(s.Images);
                    System.Diagnostics.Debug.WriteLine("Main gallery images loaded in MainViewModel.");
                });
            }
        }
    }
}
