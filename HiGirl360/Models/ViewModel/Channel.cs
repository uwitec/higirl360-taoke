using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiGirl360.Models.ViewModel
{
    public class Channel
    {
        public int ChannelID { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDesc { get; set; }
        public int ParentCateID { get; set; }
        public IEnumerable<Category> Category { get; set; }
    }
}