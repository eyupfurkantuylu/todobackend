using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.API.Dtos.SettingDtos;
using TodoApp.API.DTOs.SettingDtos;

namespace TodoApp.API.Services.SettingsServices
{
    public interface ISettingService
    {
        Task<List<SettingResponseDto>> GetAllSettingsAsync();
        Task<SettingResponseDto> GetSettingByIdAsync(string id);
        Task<SettingResponseDto> CreateSettingAsync(CreateSettingDto settingDto);
        Task<SettingResponseDto> UpdateSettingAsync(UpdateSettingDto settingDto);
        Task<bool> DeleteSettingAsync(string id);
    }
}