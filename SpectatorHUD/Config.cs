/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This software is licensed under the terms of the Functional Source License,
 *      Version 1.1, ALv2 Future License.  You are permitted to use, copy, modify,
 *      create derivative works, publicly display, and redistribute it under the the
 *      terms of the license.
 *
 *      THE SOFTWARE IS PROVIDED "AS IS" AND WITHOUT WARRANTIES OF ANY KIND, EXPRESS OR
 *      IMPLIED, INCLUDING WITHOUT LIMITATION WARRANTIES OF FITNESS FOR A PARTICULAR
 *      PURPOSE, MERCHANTABILITY, TITLE OR NON-INFRINGEMENT.
 *
 *      You should have received a copy of the Functional Source License, Version 1.1,
 *      ALv2 Future License along with this program.  If not, see https://fsl.software/.
 */

using MelonLoader;
using MelonLoader.Utils;

namespace SpectatorHUD;

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

    public string? Hud
    {
        get => this._hud.Value;
        set => this._hud.Value = value;
    }

    public void Save()
    {
        this._config.SaveToFile();
    }
}