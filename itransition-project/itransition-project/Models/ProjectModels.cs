using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itransition_project.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string About { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Medal> Medals { get; set; }
        public virtual ICollection<Comix> Comixes { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }

    public class Comment
    {

    }

    public class Medal
    {

    }

    public class Rating
    {

    }

    public class Comix
    {

    }

    public class AgeRating
    {

    }

    public class Tag
    {

    }

    public class Template
    {

    }

    public class FrameType
    {

    }

    public class Frame
    {

    }

    public class Balloon
    {

    }

    public class BalloonType
    {

    }
}