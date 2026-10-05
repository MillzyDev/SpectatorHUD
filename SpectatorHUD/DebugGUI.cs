using Il2CppSLZ.Marrow;
using UnityEngine;

namespace SpectatorHUD
{
    public class DebugGUI : MonoBehaviour
    {
        private bool _showGui = false;

        private bool _leftGunHeld;
        private bool _rightGunHeld;
        private int? _leftAmmo;
        private int? _rightAmmo;
        private int? _leftReserve;
        private int? _rightReserve;
        private float? _health;
        private float? _maxHealth;
        private float? _percentageHealth;
        
        public DebugGUI(IntPtr ptr) : base(ptr)
        {
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                this._showGui = !this._showGui;
            }
        }

        private void OnEnable()
        {
            HudState.OnLeftGunChanged += this.UpdateLeftGun;
            HudState.OnRightGunChanged += this.UpdateRightGun;
            HudState.OnLeftAmmoChanged += this.UpdateLeftAmmo;
            HudState.OnRightAmmoChanged += this.UpdateRightAmmo;
            HudState.OnLeftReserveChanged += this.UpdateLeftReserve;
            HudState.OnRightReserveChanged += this.UpdateRightReserve;
            HudState.OnHealthChanged += this.UpdateHealth;
            HudState.OnMaxHealthChanged += this.UpdateMaxHealth;
        }

        private void OnDisable()
        {
            HudState.OnLeftGunChanged -= this.UpdateLeftGun;
            HudState.OnRightGunChanged -= this.UpdateRightGun;
            HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmo;
            HudState.OnRightAmmoChanged -= this.UpdateRightAmmo;
            HudState.OnLeftReserveChanged -= this.UpdateLeftReserve;
            HudState.OnRightReserveChanged -= this.UpdateRightReserve;
            HudState.OnHealthChanged -= this.UpdateHealth;
            HudState.OnMaxHealthChanged -= this.UpdateMaxHealth;
        }

        private void UpdateLeftGun(Gun? gun)
        {
            this._leftGunHeld = gun != null;
        }

        private void UpdateRightGun(Gun? gun)
        {
            this._rightGunHeld = gun != null;
        }

        private void UpdateLeftAmmo(int? ammo)
        {
            this._leftAmmo = ammo;
        }

        private void UpdateRightAmmo(int? ammo)
        {
            this._rightAmmo = ammo;
        }

        private void UpdateLeftReserve(int? reserve)
        {
            this._leftReserve = reserve;
        }

        private void UpdateRightReserve(int? reserve)
        {
            this._rightReserve = reserve;
        }

        private void UpdateHealth(float? health, float? percentage)
        {
            this._health = health;
            this._percentageHealth = percentage;
        }

        private void UpdateMaxHealth(float? maxHealth)
        {
            this._maxHealth = maxHealth;
        }

        private void OnGUI()
        {
            if (!this._showGui)
            {
                return;
            }
            
            GUI.Label(new Rect(25, 25, 300, 30), "SpectatorHUD v" + BuildInfo.Version);


            GUI.Label(new Rect(25, 50, 300, 30), "Tracked Values:");

            GUI.Label(new Rect(35, 65, 300, 30), "Left Gun Held: " + this._leftGunHeld);
            GUI.Label(new Rect(35, 80, 300, 30), "Right Gun Held: " + this._rightGunHeld);
            GUI.Label(new Rect(35, 95, 300, 30), "Left Ammo: " + this._leftAmmo);
            GUI.Label(new Rect(35, 110, 300, 30), "Right Ammo: " + this._rightAmmo);
            GUI.Label(new Rect(35, 125, 300, 30), "Left Reserve: " + this._leftReserve);
            GUI.Label(new Rect(35, 140, 300, 30), "Right Reserve: " + this._rightReserve);
            GUI.Label(new Rect(35, 155, 300, 30), "Health: " + this._health);
            GUI.Label(new Rect(35, 170, 300, 30), "Max Health: " + this._maxHealth);
            GUI.Label(new Rect(35, 185, 300, 30), "Health Percentage: " + this._percentageHealth);


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
            GUI.Label(new Rect(695, 225, 300, 30), "animationRightReserve: " + MetaInfo.AnimationRightReserveChanged);
            GUI.Label(new Rect(695, 240, 300, 30), "animationLeftReserveChanged: " + MetaInfo.AnimationLeftReserveChanged);
            GUI.Label(new Rect(695, 255, 300, 30), "animationRightReserveChanged: " + MetaInfo.AnimationRightReserveChanged);

            GUI.Label(new Rect(695, 275, 300, 30), "animationHealth: " + MetaInfo.AnimationHealth);
            GUI.Label(new Rect(695, 290, 300, 30), "animationMaxHealth: " + MetaInfo.AnimationMaxHealth);
            GUI.Label(new Rect(695, 305, 300, 30), "animationHealthPercentage: " + MetaInfo.AnimationHealthPercentage);
            GUI.Label(new Rect(695, 320, 300, 30), "animationHealthChanged: " + MetaInfo.AnimationHealthChanged);
            GUI.Label(new Rect(695, 335, 300, 30), "animationMaxHealthChanged: " + MetaInfo.AnimationMaxHealthChanged);
        }
    }
}