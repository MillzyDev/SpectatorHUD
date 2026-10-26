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

using SpectatorHUD.Util;
using UnityEngine;

namespace SpectatorHUD;

public class DebugGUI : MonoBehaviour
{
    private HudBootstrap _bootstrap;
    private GUILayoutManager _layout;
    private bool _showGui;

    public DebugGUI(IntPtr ptr) : base(ptr)
    {
    }

    private void Start()
    {
        this._bootstrap = this.GetComponent<HudBootstrap>();
        this._layout = new GUILayoutManager(
            startX:         25f, 
            startY:         25f, 
            elementWidth:   220f, 
            elementHeight:  22f,
            elementSpacing:-4f,
            indentSize:     10f, 
            spaceSize:      15f,
            cacheSize:      60
        );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            this._showGui = !this._showGui;
        }
    }

    private void OnGUI()
    {
        if (!this._showGui)
        {
            return;
        }
        
        this._layout.Start();
        
        this._layout.Column(0);
        GUI.Label(this._layout.GetNext(), "SpectatorHUD v" + BuildInfo.Version);
        
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "Tracked Values:");
        this._layout.Indent();
        GUI.Label(this._layout.GetNext(), "Left Gun Held: " + (HudState.LeftGun != null));
        GUI.Label(this._layout.GetNext(), "Right Gun Held: " + (HudState.RightGun != null));
        GUI.Label(this._layout.GetNext(), "Left Ammo: " + HudState.LeftAmmo);
        GUI.Label(this._layout.GetNext(), "Right Ammo: " + HudState.RightAmmo);
        GUI.Label(this._layout.GetNext(), "Left Reserve: " + HudState.LeftReserve);
        GUI.Label(this._layout.GetNext(), "Right Reserve: " + HudState.RightReserve);
        GUI.Label(this._layout.GetNext(), "Health: " + HudState.Health);
        GUI.Label(this._layout.GetNext(), "Max Health: " + HudState.MaxHealth);
        GUI.Label(this._layout.GetNext(), "Health Percentage: " + HudState.HealthPercentage);
        
        this._layout.Outdent();
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "HUD Metadata:");
        this._layout.Indent();
        GUI.Label(this._layout.GetNext(), "SpectatorHUD Version: " + MetaInfo.HUDCompatibility);
        GUI.Label(this._layout.GetNext(), "Name: " + MetaInfo.HUDName);
        GUI.Label(this._layout.GetNext(), "Author: " + MetaInfo.HUDAuthor);
        GUI.Label(this._layout.GetNext(), "Version: " + MetaInfo.HUDVersion);

        this._layout.Outdent();
        this._layout.Space(3);
        if (GUI.Button(this._layout.GetNext(), "Reload HUD"))
        {
            this._bootstrap.ReloadHudFromConfig();
        }

        TimeSpan since = DateTime.Now - this._bootstrap.lastLoad;
        GUI.Label(this._layout.GetNext(), $"Last reloaded {since.TotalSeconds:0.}s ago");
        
        this._layout.Column(1);
        _ = this._layout.GetNext();
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "Counters:");
        this._layout.Indent();
        GUI.Label(this._layout.GetNext(), "leftReserveCounter: " + MetaInfo.HasLeftReserveCounter);
        GUI.Label(this._layout.GetNext(), "leftAmmoCounter: " + MetaInfo.HasLeftAmmoCounter);
        GUI.Label(this._layout.GetNext(), "totalLeftAmmoCounter: " + MetaInfo.HasTotalLeftAmmoCounter);
        GUI.Label(this._layout.GetNext(), "rightReserveCounter: " + MetaInfo.HasRightReserveCounter);
        GUI.Label(this._layout.GetNext(), "rightAmmoCounter: " + MetaInfo.HasRightAmmoCounter);
        GUI.Label(this._layout.GetNext(), "totalRightAmmoCounter: " + MetaInfo.HasTotalRightAmmoCounter);
        GUI.Label(this._layout.GetNext(), "totalLightAmmoCounter: " + MetaInfo.HasTotalLightAmmoCounter);
        GUI.Label(this._layout.GetNext(), "totalMediumAmmoCounter: " + MetaInfo.HasTotalMediumAmmoCounter);
        GUI.Label(this._layout.GetNext(), "totalHeavyAmmoCounter: " + MetaInfo.HasTotalHeavyAmmoCounter);
        GUI.Label(this._layout.GetNext(), "healthCounter: " + MetaInfo.HasHealthCounter);
        GUI.Label(this._layout.GetNext(), "maxHealthCounter: " + MetaInfo.HasMaxHealthCounter);
        GUI.Label(this._layout.GetNext(), "healthPercentageCounter: " + MetaInfo.HasHealthPercentageCounter);
        this._layout.Outdent();
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "Toggleable Elements:");
        this._layout.Indent();
        GUI.Label(this._layout.GetNext(), "activeWithGunInLeftHand: " + MetaInfo.ActiveWithGunInLeftHand);
        GUI.Label(this._layout.GetNext(), "activeWithGunInRightHand: " + MetaInfo.ActiveWithGunInRightHand);

        // Animation Events
        this._layout.Column(2);
        _ = this._layout.GetNext();
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "Animation Events:");
        this._layout.Indent();
        GUI.Label(this._layout.GetNext(), "animationLeftGunHeld: " + MetaInfo.AnimationLeftGunHeld);
        GUI.Label(this._layout.GetNext(), "animationRightGunHeld: " + MetaInfo.AnimationRightGunHeld);
        GUI.Label(this._layout.GetNext(), "animationLeftGunHeldChanged: " + MetaInfo.AnimationLeftGunHeldChanged);
        GUI.Label(this._layout.GetNext(), "animationRightGunHeldChanged: " + MetaInfo.AnimationRightGunHeldChanged);
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "animationLeftAmmo: " + MetaInfo.AnimationLeftAmmo);
        GUI.Label(this._layout.GetNext(), "animationRightAmmo: " + MetaInfo.AnimationRightAmmo);
        GUI.Label(this._layout.GetNext(), "animationLeftAmmoChanged: " + MetaInfo.AnimationLeftAmmoChanged);
        GUI.Label(this._layout.GetNext(), "animationRightAmmoChanged: " + MetaInfo.AnimationRightAmmoChanged);
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "animationLeftReserve: " + MetaInfo.AnimationLeftReserve);
        GUI.Label(this._layout.GetNext(), "animationRightReserve: " + MetaInfo.AnimationRightReserve);
        GUI.Label(this._layout.GetNext(), "animationLeftReserveChanged: " + MetaInfo.AnimationLeftReserveChanged);
        GUI.Label(this._layout.GetNext(), "animationRightReserveChanged: " + MetaInfo.AnimationRightReserveChanged);
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "animationTotalLeftAmmo: " + MetaInfo.AnimationTotalLeftAmmo);
        GUI.Label(this._layout.GetNext(), "animationTotalRightAmmo: " + MetaInfo.AnimationTotalRightAmmo);
        GUI.Label(this._layout.GetNext(), "animationTotalLightAmmo: " + MetaInfo.AnimationTotalLightAmmo);
        GUI.Label(this._layout.GetNext(), "animationTotalMediumAmmo: " + MetaInfo.AnimationTotalMediumAmmo);
        GUI.Label(this._layout.GetNext(), "animationTotalHeavyAmmo: " + MetaInfo.AnimationTotalHeavyAmmo);
        GUI.Label(this._layout.GetNext(), "animationTotalLeftAmmoChanged: " + MetaInfo.AnimationTotalLeftAmmoChanged);
        GUI.Label(this._layout.GetNext(), "animationTotalRightAmmoChanged: " + MetaInfo.AnimationTotalRightAmmoChanged);
        GUI.Label(this._layout.GetNext(), "animationTotalLightAmmoChanged: " + MetaInfo.AnimationTotalLightAmmoChanged);
        GUI.Label(this._layout.GetNext(), "animationTotalMediumAmmoChanged: " + MetaInfo.AnimationTotalMediumAmmoChanged);
        GUI.Label(this._layout.GetNext(), "animationTotalHeavyAmmoChanged: " + MetaInfo.AnimationTotalHeavyAmmoChanged);
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "animationHealth: " + MetaInfo.AnimationHealth);
        GUI.Label(this._layout.GetNext(), "animationMaxHealth: " + MetaInfo.AnimationMaxHealth);
        GUI.Label(this._layout.GetNext(), "animationHealthPercentage: " + MetaInfo.AnimationHealthPercentage);
        GUI.Label(this._layout.GetNext(), "animationHealthChanged: " + MetaInfo.AnimationHealthChanged);
        GUI.Label(this._layout.GetNext(), "animationMaxHealthChanged: " + MetaInfo.AnimationMaxHealthChanged);
        
        this._layout.Column(3);
        _ = this._layout.GetNext();
        this._layout.Space();
        GUI.Label(this._layout.GetNext(), "Shader Events:");
        this._layout.Indent();
        GUI.Label(this._layout.GetNext(), "shaderLeftAmmo: " + MetaInfo.ShaderLeftAmmo);
        GUI.Label(this._layout.GetNext(), "shaderRightAmmo: " + MetaInfo.ShaderRightAmmo);
        GUI.Label(this._layout.GetNext(), "shaderLeftReserve: " + MetaInfo.ShaderLeftReserve);
        GUI.Label(this._layout.GetNext(), "shaderRightReserve: " + MetaInfo.ShaderRightReserve);
        GUI.Label(this._layout.GetNext(), "shaderTotalLeftAmmo: " + MetaInfo.ShaderTotalLeftAmmo);
        GUI.Label(this._layout.GetNext(), "shaderTotalRightAmmo: " + MetaInfo.ShaderTotalRightAmmo);
        GUI.Label(this._layout.GetNext(), "shaderTotalLightAmmo: " + MetaInfo.ShaderTotalLightAmmo);
        GUI.Label(this._layout.GetNext(), "shaderTotalMediumAmmo: " + MetaInfo.ShaderTotalMediumAmmo);
        GUI.Label(this._layout.GetNext(), "shaderTotalHeavyAmmo: " + MetaInfo.ShaderTotalHeavyAmmo);
        GUI.Label(this._layout.GetNext(), "shaderHealth: " + MetaInfo.ShaderHealth);
        GUI.Label(this._layout.GetNext(), "shaderMaxHealth: " + MetaInfo.ShaderMaxHealth);
        GUI.Label(this._layout.GetNext(), "shaderHealthPercentage: " + MetaInfo.ShaderHealthPercentage);
        
        this._layout.End();
    }
}