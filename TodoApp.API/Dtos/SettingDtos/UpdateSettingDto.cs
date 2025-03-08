using System;

namespace TodoApp.API.Dtos.SettingDtos
{
    public class UpdateSettingDto
    {
        public string Id { get; set; }
        public bool isDarkMode { get; set; }
        public string Language { get; set; }
        public bool isNotificationsEnabled { get; set; }
        public string UserId { get; set; }
    }
} 