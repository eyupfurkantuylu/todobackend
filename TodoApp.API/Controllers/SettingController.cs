using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.API.Dtos.SettingDtos;
using TodoApp.API.DTOs.SettingDtos;
using TodoApp.API.Services.SettingsServices;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSettings()
        {
            var settings = await _settingService.GetAllSettingsAsync();
            return Ok(settings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSettingById(string id)
        {
            var setting = await _settingService.GetSettingByIdAsync(id);
            return Ok(setting);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSetting(CreateSettingDto settingDto)
        {
            var setting = await _settingService.CreateSettingAsync(settingDto);
            return Ok(setting);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateSetting(UpdateSettingDto settingDto)
        {
            var setting = await _settingService.UpdateSettingAsync(settingDto);
            return Ok(setting);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetting(string id)
        {
            var result = await _settingService.DeleteSettingAsync(id);
            return NoContent();
        }
    }
}