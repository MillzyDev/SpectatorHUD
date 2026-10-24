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

using Il2CppSLZ.Marrow;
using UnityEngine;

namespace SpectatorHUD
{
    public class DebugGUI : MonoBehaviour
    {
        private HudBootstrap _bootstrap;
        private bool _showGui = false;
        
        public DebugGUI(IntPtr ptr) : base(ptr)
        {
        }

        private void Start()
        {
            this._bootstrap = this.GetComponent<HudBootstrap>();
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
            
            GUI.Label(new Rect(25, 25, 300, 30), "SpectatorHUD v" + BuildInfo.Version);


            GUI.Label(new Rect(25, 50, 300, 30), "Tracked Values:");

            GUI.Label(new Rect(35, 65, 300, 30), "Left Gun Held: " + (HudState.LeftGun != null));
            GUI.Label(new Rect(35, 80, 300, 30), "Right Gun Held: " + (HudState.RightGun != null));
            GUI.Label(new Rect(35, 95, 300, 30), "Left Ammo: " + HudState.LeftAmmo);
            GUI.Label(new Rect(35, 110, 300, 30), "Right Ammo: " + HudState.RightAmmo);
            GUI.Label(new Rect(35, 125, 300, 30), "Left Reserve: " + HudState.LeftReserve);
            GUI.Label(new Rect(35, 140, 300, 30), "Right Reserve: " + HudState.RightReserve);
            GUI.Label(new Rect(35, 155, 300, 30), "Health: " + HudState.Health);
            GUI.Label(new Rect(35, 170, 300, 30), "Max Health: " + HudState.MaxHealth);
            GUI.Label(new Rect(35, 185, 300, 30), "Health Percentage: " + HudState.HealthPercentage);


            GUI.Label(new Rect(225, 50, 300, 30), "HUD Metadata:");

            GUI.Label(new Rect(235, 65, 300, 30), "SpectatorHUD Version: " + MetaInfo.HUDCompatibility);
            GUI.Label(new Rect(235, 80, 300, 30), "Name: " + MetaInfo.HUDName);
            GUI.Label(new Rect(235, 95, 300, 30), "Author: " + MetaInfo.HUDAuthor);
            GUI.Label(new Rect(235, 110, 300, 30), "Version: " + MetaInfo.HUDVersion);


            GUI.Label(new Rect(425, 50, 300, 30), "HudManager Inspection:");

            GUI.Label(new Rect(435, 65, 300, 30), "Counters:");
            GUI.Label(new Rect(445, 80, 300, 30), "leftReserveCounter: " + MetaInfo.HasLeftReserveCounter);
            GUI.Label(new Rect(445, 95, 300, 30), "leftAmmoCounter: " + MetaInfo.HasLeftAmmoCounter);
            GUI.Label(new Rect(445, 110, 300, 30), "rightReserveCounter: " + MetaInfo.HasRightReserveCounter);
            GUI.Label(new Rect(445, 125, 300, 30), "rightAmmoCounter: " + MetaInfo.HasRightAmmoCounter);
            GUI.Label(new Rect(445, 140, 300, 30), "healthCounter: " + MetaInfo.HasHealthCounter);
            GUI.Label(new Rect(445, 155, 300, 30), "maxHealthCounter: " + MetaInfo.HasMaxHealthCounter);
            GUI.Label(new Rect(445, 170, 300, 30), "healthPercentageCounter: " + MetaInfo.HasHealthPercentageCounter);
            

            GUI.Label(new Rect(435, 190, 300, 30), "Toggleable Elements:");
            GUI.Label(new Rect(445, 205, 300, 30), "activeWithGunInLeftHand: " + MetaInfo.ActiveWithGunInLeftHand);
            GUI.Label(new Rect(445, 220, 300, 30), "activeWithGunInRightHand: " + MetaInfo.ActiveWithGunInRightHand);

            GUI.Label(new Rect(685, 65, 300, 30), "Animation Events:");
            GUI.Label(new Rect(695, 80, 300, 30), "animationLeftGunHeld: " + MetaInfo.AnimationLeftGunHeld);
            GUI.Label(new Rect(695, 95, 300, 30), "animationRightGunHeld: " + MetaInfo.AnimationRightGunHeld);
            GUI.Label(new Rect(695, 110, 300, 30), "animationLeftGunHeldChanged: " + MetaInfo.AnimationLeftGunHeldChanged);
            GUI.Label(new Rect(695, 125, 300, 30), "animationRightGunHeldChanged: " + MetaInfo.AnimationRightGunHeldChanged);

            GUI.Label(new Rect(695, 145, 300, 30), "animationLeftAmmo: " + MetaInfo.AnimationLeftAmmo);
            GUI.Label(new Rect(695, 160, 300, 30), "animationRightAmmo: " + MetaInfo.AnimationRightAmmo);
            GUI.Label(new Rect(695, 175, 300, 30), "animationLeftAmmoChanged: " + MetaInfo.AnimationLeftAmmoChanged);
            GUI.Label(new Rect(695, 190, 300, 30), "animationRightAmmoChanged: " + MetaInfo.AnimationRightAmmoChanged);

            GUI.Label(new Rect(695, 210, 300, 30), "animationLeftReserve: " + MetaInfo.AnimationLeftReserve);
            GUI.Label(new Rect(695, 225, 300, 30), "animationRightReserve: " + MetaInfo.AnimationRightReserve);
            GUI.Label(new Rect(695, 240, 300, 30), "animationLeftReserveChanged: " + MetaInfo.AnimationLeftReserveChanged);
            GUI.Label(new Rect(695, 255, 300, 30), "animationRightReserveChanged: " + MetaInfo.AnimationRightReserveChanged);

            GUI.Label(new Rect(695, 275, 300, 30), "animationHealth: " + MetaInfo.AnimationHealth);
            GUI.Label(new Rect(695, 290, 300, 30), "animationMaxHealth: " + MetaInfo.AnimationMaxHealth);
            GUI.Label(new Rect(695, 305, 300, 30), "animationHealthPercentage: " + MetaInfo.AnimationHealthPercentage);
            GUI.Label(new Rect(695, 320, 300, 30), "animationHealthChanged: " + MetaInfo.AnimationHealthChanged);
            GUI.Label(new Rect(695, 335, 300, 30), "animationMaxHealthChanged: " + MetaInfo.AnimationMaxHealthChanged);
            
            GUI.Label(new Rect(935, 65, 300, 30), "Shader Events:");
            GUI.Label(new Rect(945, 80, 300, 30), "shaderLeftAmmo: " + MetaInfo.ShaderLeftAmmo);
            GUI.Label(new Rect(945, 95, 300, 30), "shaderRightAmmo: " + MetaInfo.ShaderRightAmmo);
            GUI.Label(new Rect(945, 110, 300, 30), "shaderLeftReserve: " + MetaInfo.ShaderLeftReserve);
            GUI.Label(new Rect(945, 125, 300, 30), "shaderRightReserve: " + MetaInfo.ShaderRightReserve);
            GUI.Label(new Rect(945, 140, 300, 30), "shaderHealth: " + MetaInfo.ShaderHealth);
            GUI.Label(new Rect(945, 155, 300, 30), "shaderMaxHealth: " + MetaInfo.ShaderMaxHealth);
            GUI.Label(new Rect(945, 170, 300, 30), "shaderHealthPercentage: " + MetaInfo.ShaderHealthPercentage);
            
            if (GUI.Button(new Rect(25, 235, 200, 20), "Reload HUD"))
            {
                this._bootstrap.ReloadHudFromConfig();
            }

            TimeSpan since = DateTime.Now - this._bootstrap.lastLoad;
            GUI.Label(new Rect(25, 265, 300, 30), $"Last reloaded {since.TotalSeconds:0.}s ago");
        }
    }
}