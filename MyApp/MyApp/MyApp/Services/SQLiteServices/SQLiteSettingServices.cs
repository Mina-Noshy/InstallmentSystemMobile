using MyApp.MVVM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services.SQLiteServices
{
    public interface ISQLiteSettingServices
    {
        int Update(Setting setting);
        int EditSettingsLastUpdet();
        Setting GetSetting();
        bool IsSettingsUpdated();
    }

    public class SQLiteSettingServices : ISQLiteSettingServices
    {
        public SQLiteConnection db = App.context.Connection;

        public Setting GetSetting()
        {
            return db.Table<Setting>().FirstOrDefault() ?? new Setting();
        }

        public bool IsSettingsUpdated()
        {
            return GetSetting().isSettingsUpdated();
        }

        public bool IsTokenExpired()
        {
            return GetSetting().isTokenExpired();
        }

        public int Update(Setting setting)
        {
            return db.Update(setting);
        }

        public int EditSettingsLastUpdet()
        {
            Setting setting = GetSetting();
            setting.settingsUpdatedAt = DateTime.UtcNow;
            return Update(setting);
        }
    }
}
