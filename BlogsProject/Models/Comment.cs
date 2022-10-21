using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace BlogsProject.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }

        public virtual BlogPost Post { get; set; }
        public virtual User User { get; set; }
    }
}
