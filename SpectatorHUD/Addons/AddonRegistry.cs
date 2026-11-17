using System;
using System.Collections.Generic;
using Semver;
using SpectatorHUD.Logging;

namespace SpectatorHUD.Addons;

public sealed class AddonRegistry
{
    private static readonly Lazy<AddonRegistry> LazyInitialiser = new(() => new AddonRegistry());
    public static AddonRegistry Instance => LazyInitialiser.Value;
    
    private readonly Logger _logger;
    private readonly Dictionary<string, SemVersion> _registeredAddons;
    
    private AddonRegistry()
    {
        this._logger = LogService.Logger;
        this._registeredAddons = new Dictionary<string, SemVersion>();
    }

    public void Register(string id, SemVersion version)
    {
        this._registeredAddons.Add(id, version);
    }

    public void Register(string id, string version)
    {
        this.Register(id, SemVersion.Parse(version, strict: true));
    }

    public void Register(string id, int major, int minor, int patch)
    {
        this.Register(id, new SemVersion(major, minor, patch));
    }

    public bool HasAddon(string id)
    {
        return this._registeredAddons.ContainsKey(id);
    }

    public bool HasAddonCompatibleWith(string id, SemVersion version)
    {
        if (!this._registeredAddons.TryGetValue(id, out SemVersion? registeredVersion))
        {
            return false;
        }
        
        // Addon is compatible if:
        //  - MAJOR number is the same
        //  - Registered MINOR is <= queried version
        // PATCH number doesn't matter
        if (registeredVersion.Major != version.Major)
        {
            return false;
        }

        return registeredVersion.Minor <= version.Minor;
    }

    public bool HasAddonCompatibleWith(string id, string version)
    {
        return this.HasAddonCompatibleWith(id, SemVersion.Parse(version));
    }

    public bool HasAddonCompatibleWith(string id, int major, int minor, int patch)
    {
        if (!this._registeredAddons.TryGetValue(id, out SemVersion? registeredVersion))
        {
            return false;
        }

        if (registeredVersion.Major != major)
        {
            return false;
        }
        
        return registeredVersion.Minor <= minor;
    }
}