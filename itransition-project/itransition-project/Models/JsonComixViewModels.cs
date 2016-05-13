﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itransition_project.Models
{
    [Serializable]
    public class JsonComixViewModel
    {
        public List<JsonPagesViewModel> Pages { get; set; }
    }

    [Serializable]
    public class JsonPagesViewModel
    {
        public string Template { get; set; }
        public List<JsonFrameImageViewModel> FrameImages { get; set; }
    }

    [Serializable]
    public class JsonFrameImageViewModel
    {
        public string BackgroundImage { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public List<JsonBalloonsViewModel> Balloons { get; set; }
    }

    [Serializable]
    public class JsonBalloonsViewModel
    {
        public string Text { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }
}
