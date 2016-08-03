using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWeb.Models
{
    public class UserInfoModels
    {
        public string UserName { get; set; }
        public int Sex { get; set; }
        public int UserHeight { get; set; }
        public int UserWeight { get; set; }
        public string UserResult { get; set; }
        public UserInfoModels()
        {
            this.UserResult = string.Empty;
        }
    }
}