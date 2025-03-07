using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.API.Data
{
    public class Settings
    {
        public string Id { get; set; }
        public bool isDarkMode { get; set; }
        public string Language { get; set; }
        public bool isNotificationsEnabled { get; set; }
        public string UserId { get; set; }
    }
}