using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using TodoApp.API.Data;
using TodoApp.API.Dtos.SettingDtos;
using TodoApp.API.DTOs.SettingDtos;

namespace TodoApp.API.Services.SettingsServices
{
    public class SettingService : ISettingService
    {
        private readonly DapperDbContext _context;

        public SettingService(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<List<SettingResponseDto>> GetAllSettingsAsync()
        {
            const string sql = @"
                SELECT Id, isDarkMode, Language, isNotificationsEnabled, UserId 
                FROM Settings";

            using (var connection = _context.CreateConnection())
            {
                var settings = await connection.QueryAsync<SettingResponseDto>(sql);
                return settings.AsList();
            }
        }

        public async Task<SettingResponseDto> GetSettingByIdAsync(string id)
        {
            const string sql = @"
                SELECT Id, isDarkMode, Language, isNotificationsEnabled, UserId 
                FROM Settings 
                WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<SettingResponseDto>(sql, parameters);
            }
        }

        public async Task<SettingResponseDto> CreateSettingAsync(CreateSettingDto settingDto)
        {
            var id = Guid.NewGuid().ToString();

            const string sql = @"
                INSERT INTO Settings (Id, isDarkMode, Language, isNotificationsEnabled, UserId)
                VALUES (@Id, @isDarkMode, @Language, @isNotificationsEnabled, @UserId);
                
                SELECT Id, isDarkMode, Language, isNotificationsEnabled, UserId
                FROM Settings
                WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("isDarkMode", settingDto.isDarkMode);
            parameters.Add("Language", settingDto.Language);
            parameters.Add("isNotificationsEnabled", settingDto.isNotificationsEnabled);
            parameters.Add("UserId", settingDto.UserId);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstAsync<SettingResponseDto>(sql, parameters);
            }
        }

        public async Task<SettingResponseDto> UpdateSettingAsync(UpdateSettingDto settingDto)
        {
            const string sql = @"
                UPDATE Settings 
                SET isDarkMode = @isDarkMode,
                    Language = @Language,
                    isNotificationsEnabled = @isNotificationsEnabled,
                    UserId = @UserId
                WHERE Id = @Id;
                
                SELECT Id, isDarkMode, Language, isNotificationsEnabled, UserId
                FROM Settings
                WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", settingDto.Id);
            parameters.Add("isDarkMode", settingDto.isDarkMode);
            parameters.Add("Language", settingDto.Language);
            parameters.Add("isNotificationsEnabled", settingDto.isNotificationsEnabled);
            parameters.Add("UserId", settingDto.UserId);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<SettingResponseDto>(sql, parameters);
            }
        }

        public async Task<bool> DeleteSettingAsync(string id)
        {
            const string sql = "DELETE FROM Settings WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, parameters);
                return affectedRows > 0;
            }
        }
    }
}