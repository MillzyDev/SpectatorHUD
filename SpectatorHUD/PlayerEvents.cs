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

using System;
using Il2CppInterop.Runtime;
using Il2CppSLZ.Marrow;
using SpectatorHUD.Extensions;
using SpectatorHUD.Logging;
using SpectatorHUD.Util;
using UnityEngine;
using Logger = SpectatorHUD.Logging.Logger;

namespace SpectatorHUD;

internal sealed class PlayerEvents : MonoBehaviour
{
    private readonly Logger _logger;
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverAttachedLeft;
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverAttachedRight;
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverDetachedLeft;
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverDetachedRight;
    
    private readonly HudState _hudState;
    private RigManager? _rigManager;
    private Hand? _leftHand;
    private Hand? _rightHand;

    public PlayerEvents(IntPtr ptr) : base(ptr)
    {
        this._logger = LogService.Logger;
        this._hudState = HudState.Instance;
        
        this._onReceiverAttachedLeft =
            DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.OnReceiverAttachedLeft);
        this._onReceiverDetachedLeft =
            DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.OnReceiverDetachedLeft);
        this._onReceiverAttachedRight =
            DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.OnReceiverAttachedRight);
        this._onReceiverDetachedRight =
            DelegateSupport.ConvertDelegate<Il2CppSystem.Action<HandReciever>>(this.OnReceiverDetachedRight);
    }

    private void Start()
    {
        this._rigManager = this.GetComponent<RigManager>();

        PhysicsRig physicsRig = this._rigManager.physicsRig;
        this._leftHand = physicsRig.leftHand;
        this._rightHand = physicsRig.rightHand;

        this._leftHand.onRecieverAttached += this._onReceiverAttachedLeft;
        this._leftHand.onRecieverDetached += this._onReceiverDetachedLeft;
        this._rightHand.onRecieverAttached += this._onReceiverAttachedRight;
        this._rightHand.onRecieverDetached += this._onReceiverDetachedRight;
    }

    private void OnDestroy()
    {
        this._leftHand?.onRecieverAttached -= this._onReceiverAttachedLeft;
        this._leftHand?.onRecieverDetached -= this._onReceiverDetachedLeft;
        this._rightHand?.onRecieverAttached -= this._onReceiverAttachedRight;
        this._rightHand?.onRecieverDetached -= this._onReceiverDetachedRight;
    }

    private void OnReceiverAttachedLeft(HandReciever handReciever) // learn to spell SLZ
    {
        this._logger.Debug("OnReceiverAttachedLeft called");

        if (!handReciever.TryFindGunFromReceiver(out Gun? gun))
        {
            return;
        }

        this._hudState.LeftGun = gun;
        if (gun != null)
        {
            this._hudState.LeftDefaultCartridge = new CartridgeContainer(gun.defaultCartridge);
        }
        else
        {
            this._hudState.LeftDefaultCartridge = null;
            this._hudState.LeftAmmo = null;
            this._hudState.LeftReserve = null;
            this._hudState.LeftTotalAmmo = null;
            return;
        }

        this._hudState.LeftAmmo = gun.GetAmmoCount();
        this._hudState.LeftReserve = AmmoInventory.Instance.GetCartridgeCount(this._hudState.LeftDefaultCartridge.cartridge) -
                                     this._hudState.LeftAmmo;
        this._hudState.LeftTotalAmmo = this._hudState.LeftAmmo + this._hudState.LeftReserve;
    }

    private void OnReceiverDetachedLeft(HandReciever handReciever)
    {
        this._logger.Debug("OnReceiverDetachedLeft called");

        this._hudState.LeftGun = null;
        this._hudState.LeftDefaultCartridge = null;
        this._hudState.LeftAmmo = null;
        this._hudState.LeftReserve = null;
        this._hudState.LeftTotalAmmo = null;
    }

    private void OnReceiverAttachedRight(HandReciever handReciever)
    {
        this._logger.Debug("OnReceiverAttachedRight called");

        if (!handReciever.TryFindGunFromReceiver(out Gun? gun))
        {
            return;
        }

        this._hudState.RightGun = gun;
        if (gun != null)
        {
            this._hudState.RightDefaultCartridge = new CartridgeContainer(gun.defaultCartridge);
        }
        else
        {
            this._hudState.RightDefaultCartridge = null;
            this._hudState.RightAmmo = null;
            this._hudState.RightReserve = null;
            this._hudState.RightTotalAmmo = null;
            return;
        }

        this._hudState.RightAmmo = gun.GetAmmoCount();
        this._hudState.RightReserve = AmmoInventory.Instance.GetCartridgeCount(this._hudState.RightDefaultCartridge.cartridge) -
                                      this._hudState.RightAmmo;
        this._hudState.RightTotalAmmo = this._hudState.RightAmmo + this._hudState.RightReserve;
    }

    private void OnReceiverDetachedRight(HandReciever handReciever)
    {
        this._logger.Debug("OnReceiverDetachedRight called");

        this._hudState.RightGun = null;
        this._hudState.RightDefaultCartridge = null;
        this._hudState.RightAmmo = null;
        this._hudState.RightReserve = null;
        this._hudState.RightTotalAmmo = null;
    }
}