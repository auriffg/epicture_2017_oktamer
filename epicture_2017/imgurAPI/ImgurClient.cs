using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace epicture_2017.imgurAPI
{
    public class ImgurClient
    {
        private string _ClientId = "ecd7a7c92afea61";
        private string _ClientSecret = "e3430f3093b3bec79bc27fa1219609f6909a4d52";

        public ImgurClient(string clientID, string clientSecret)
        {
            _ClientId = clientID;
            _ClientSecret = clientSecret;
        }

        public void GetAccessToken(Action<string> onCompletion)
        {

            var client = new HttpClient();
            var task = client.GetStringAsync(new Uri(string.Format("https://api.imgur.com/oauth2/authorize?client_id={0}&redirect_uri={1}&response_type=code&state=CODE_RECEIVED",
                _ClientId, "epicture_2017_test"))).ContinueWith((wesh) =>
                {
                    onCompletion(wesh.Result);
                });
        }

        public void GetMainGalleryImages(ImgurGallerySection section, ImgurGallerySort sort, int page, Action<ImgurImageData> onCompletion)
        {
            string _sort = sort.ToString().ToLower();
            string _section = section.ToString().ToLower();

            //WebClient client = new WebClient();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID " + _ClientId);
            var task = client.GetStringAsync(new Uri(string.Format(ImgurEndpoints.MainGallery, _section, _sort, page))).ContinueWith((taskwithresp) =>
           {
               try
               {
                   var response = taskwithresp.Result;
                   var imagedata = JsonConvert.DeserializeObject<ImgurImageData>(response);
                   onCompletion(imagedata);
               }
               catch
               {
                   Debug.WriteLine("imgur error");
               }
           });
        }
    }
    public static class ImgurEndpoints
    {
        public const string MainGallery = "https://api.imgur.com/3/gallery/{0}/{1}/{2}.json";
    }

    public enum ImgurGallerySort
    {
        Viral,
        Time
    }

    public enum ImgurGallerySection
    {
        Hot,
        Top,
        User
    }
}

    