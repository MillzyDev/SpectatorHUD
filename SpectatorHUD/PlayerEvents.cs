using Il2CppInterop.Runtime;
using Il2CppSLZ.Marrow;
using SpectatorHUD.Extensions;
using SpectatorHUD.Util;
using UnityEngine;

namespace SpectatorHUD;

public class PlayerEvents : MonoBehaviour
{
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverAttachedLeft;
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverDetachedLeft;
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverAttachedRight;
    private readonly Il2CppSystem.Action<HandReciever>? _onReceiverDetachedRight;

    private RigManager? _rigManager;
    private Hand? _leftHand;
    private Hand? _rightHand;
    
    public PlayerEvents(IntPtr ptr) : base(ptr)
    {
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
        Logger.Debug("HudState: OnReceiverAttachedLeft called");

        if (!handReciever.TryFindGunFromReceiver(out Gun? gun)) return;

        HudState.LeftGun = gun;
        if (gun?.defaultCartridge != null)
        {
            HudState.LeftDefaultCartridge = new CartridgeContainer(gun.defaultCartridge);
        }

        //UpdateLeftAmmoCount();
        //UpdateLeftReserveCount();
    }

    private void OnReceiverDetachedLeft(HandReciever handReciever)
    {
        Logger.Debug("PlayerEvents: OnReceiverDetachedLeft called");

        HudState.LeftGun = null;
        HudState.LeftDefaultCartridge = null;

        //UpdateLeftAmmoCount();
        //UpdateLeftReserveCount();
    }

    private void OnReceiverAttachedRight(HandReciever handReciever)
    {
        Logger.Debug("HudState: OnReceiverAttachedRight called");

        if (!handReciever.TryFindGunFromReceiver(out Gun? gun)) return;

        HudState.RightGun = gun;
        if (gun?.defaultCartridge != null)
        {
            HudState.RightDefaultCartridge = new CartridgeContainer(gun.defaultCartridge);
        }

        //UpdateRightAmmoCount();
        //UpdateRightReserveCount();
    }

    private void OnReceiverDetachedRight(HandReciever handReciever)
    {
        Logger.Debug("HudState: OnReceiverDetachedRight called");

        HudState.RightGun = null;
        //HudState.RightDefaultCartridge = null;

        //UpdateRightAmmoCount();
        //UpdateRightReserveCount();
    }
}