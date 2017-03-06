using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace epicture_2017.imgurAPI
{
     public class ImgurImageData
    {
        [JsonProperty(PropertyName = "data")]
        public IEnumerable<ImgurImage> Images { get; set; }
    }
}
