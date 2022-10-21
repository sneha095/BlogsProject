using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BlogsProject.Models
{
    public partial class Blog
    {
        public Blog()
        {
            BlogPosts = new HashSet<BlogPost>();
        }

        public int BlogId { get; set; }
        public string BlogName { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}
