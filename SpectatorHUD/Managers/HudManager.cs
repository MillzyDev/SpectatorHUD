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
    private BonelabGameControl? _bonelabGameControl;

    private RigManager? _playerRigManager;
    private Health? _health;
    
    public HudManager(IntPtr ptr) : base(ptr) {}

    [Inject]
    public void Inject(BonelabGameControl bonelabGameControl)
    {
        _bonelabGameControl = bonelabGameControl;
    }

    private void Start()
    {
        _playerRigManager = _bonelabGameControl!.PlayerRigManager;
        _health = _playerRigManager.health;
    }

    private void Update()
    {
        MelonLogger.Msg($"{_health!.curr_Health}/{_health.max_Health}");
    }
}