using System;
using System.Collections.Generic;

#nullable disable

namespace BlogsProject.Models
{
    public partial class PostTagRelation
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public virtual Tag Post { get; set; }
        public virtual BlogPost Tag { get; set; }
    }
}
