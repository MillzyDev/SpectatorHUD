﻿using Ninject.Modules;
using SpectatorHUD.Managers;

namespace SpectatorHUD.Modules;

// ReSharper disable once ClassNeverInstantiated.Global
public class SHPlayerModule : NinjectModule
{
    public override void Load()
    {
        Bind<HUDValueManager>().ToSelf().InSingletonScope();
        Bind<HUDManager>().ToSelf().InSingletonScope();
    }
}