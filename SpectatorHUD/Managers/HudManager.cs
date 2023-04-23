using System;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.Rig;
using UnityEngine;

namespace SpectatorHUD.Managers;

[RegisterTypeInIl2Cpp]
public class HudManager : MonoBehaviour
{
    private bool _finishedInject;
    private BonelabGameControl? _bonelabGameControl;

    private RigManager? _playerRigManager;
    private Health? _health;

    [Inject]
    public void Inject(BonelabGameControl bonelabGameControl)
    {
        _bonelabGameControl = bonelabGameControl;
        _finishedInject = true;
        Init();
    }

    private void Init()
    {
        _playerRigManager = _bonelabGameControl!.PlayerRigManager;
        _health = _playerRigManager.health;
        MelonLogger.Msg(_health);
    }

    private void Update()
    {
        if (!_finishedInject) return;
    }
}