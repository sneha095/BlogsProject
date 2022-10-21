using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BlogsProject.Models
{
    public partial class BlogPost
    {
        public BlogPost()
        {
            Comments = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? BlogId { get; set; }

        [JsonIgnore]
        public virtual Blog Blog { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
