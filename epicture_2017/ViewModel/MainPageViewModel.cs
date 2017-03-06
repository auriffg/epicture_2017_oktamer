using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using epicture_2017.imgurAPI;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Windows.UI.Core;

namespace epicture_2017.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        static MainPageViewModel instance = null;
        static readonly object padlock = new object();

        public MainPageViewModel()
        {
            DeserializedHomeImages = new List<ImgurImage>();
        }

        public static MainPageViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MainPageViewModel();
                    }
                    return instance;
                }
            }
        }

        private List<ImgurImage> _deserializedHomeImages;
        public List<ImgurImage> DeserializedHomeImages
        {
            get
            {
                return _deserializedHomeImages;
            }
            set
            {
                if (_deserializedHomeImages != value)
                {
                    _deserializedHomeImages = value;
                    NotifyPropertyChanged("DeserializedHomeImages");
                }
            }
        }

        private ImgurImage _currentImage;
        public ImgurImage CurrentImage
        {
            get
            {
                return _currentImage;
            }
            set
            {
                if (_currentImage != value)
                {
                    _currentImage = value;
                    NotifyPropertyChanged("CurrentImage");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
      async  private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                //System.Windows.Deployment.Current.Dispatcher.BeginInvoke(
                //  () =>
                CoreDispatcher dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(info));
                    });
            }
        }
    }
}
