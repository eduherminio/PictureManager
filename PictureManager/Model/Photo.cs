using Newtonsoft.Json;
using System;

namespace PictureManager.Model
{
    public class Photo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public Uri Url { get; set; }

        public Uri ThumbnailUrl { get; set; }

        /// <summary>
        /// FK to Album
        /// </summary>
        public string AlbumId { get; set; }

        [JsonIgnore]
        public Album Album { get; set; }
    }
}
