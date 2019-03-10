using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureManager.Model
{
    public class Album
    {
        /// <summary>
        /// PK
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// FK to User
        /// </summary>
        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [NotMapped]
        public ICollection<Photo> Photos { get; set; }

        public Album()
        {
            Photos = new List<Photo>();
        }
    }
}
