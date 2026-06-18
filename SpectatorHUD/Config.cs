/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

using MelonLoader;
using MelonLoader.Utils;

namespace SpectatorHUD
{
    public sealed class Config
    {
        private static readonly Lazy<Config> Lazy = new(() => new Config());

        private readonly MelonPreferences_Category _config;
        private readonly MelonPreferences_Entry<string?> _hud; 
        
        private Config()
        {
            this._config = MelonPreferences.CreateCategory("Config");
            
            // Add entries
            this._hud = this._config.CreateEntry<string?>("hud", "");
            
            this._config.SetFilePath(Path.Join(MelonEnvironment.UserDataDirectory, "SpectatorHUD", "config.cfg"));
        }

        public static Config Instance => Lazy.Value;

        public string? Hud => this._hud.Value;

        public void Save()
        {
            this._config.SaveToFile();
        }
    }
}