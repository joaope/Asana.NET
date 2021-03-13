using Newtonsoft.Json;

namespace Asana.Models
{
    public sealed class UserPhoto
    {
        [JsonProperty("image_128x128")]
        public string Image128x128 { get; }
        [JsonProperty("image_21x21")]
        public string Image21x21 { get; }
        [JsonProperty("image_27x27")]
        public string Image27x27 { get; }
        [JsonProperty("image_36z36")]
        public string Image36x36 { get; }
        [JsonProperty("image_60x60")]
        public string Image60x60 { get; }

        [JsonConstructor]
        public UserPhoto(string image128X128, string image21X21, string image27X27, string image36X36, string image60X60)
        {
            Image128x128 = image128X128;
            Image21x21 = image21X21;
            Image27x27 = image27X27;
            Image36x36 = image36X36;
            Image60x60 = image60X60;
        }
    }
}