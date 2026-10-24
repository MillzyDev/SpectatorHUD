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

using SpectatorHUD.Shading;
using UnityEngine;

namespace SpectatorHUD
{
    public class ShaderController : MonoBehaviour
    {
        public List<ShaderIntReference> shaderLeftAmmo;
        public List<ShaderIntReference> shaderRightAmmo;
        public List<ShaderIntReference> shaderLeftReserve;
        public List<ShaderIntReference> shaderRightReserve;
        public List<ShaderFloatReference> shaderHealth;
        public List<ShaderFloatReference> shaderMaxHealth;
        public List<ShaderFloatReference> shaderHealthPercentage;

        public ShaderController(IntPtr ptr) : base(ptr)
        {
        }
        
        private void Start()
        {
            MetaInfo.ShaderLeftAmmo = this.shaderLeftAmmo.Count;
            MetaInfo.ShaderRightAmmo = this.shaderRightAmmo.Count;
            MetaInfo.ShaderLeftReserve = this.shaderLeftReserve.Count;
            MetaInfo.ShaderRightReserve = this.shaderRightReserve.Count;
            MetaInfo.ShaderHealth = this.shaderHealth.Count;
            MetaInfo.ShaderMaxHealth = this.shaderMaxHealth.Count;
            MetaInfo.ShaderHealthPercentage = this.shaderHealthPercentage.Count;
            
            this.ProcessShaderProperties(this.shaderLeftAmmo);
            this.ProcessShaderProperties(this.shaderRightAmmo);
            this.ProcessShaderProperties(this.shaderLeftReserve);
            this.ProcessShaderProperties(this.shaderRightReserve);
            this.ProcessShaderProperties(this.shaderHealth);
            this.ProcessShaderProperties(this.shaderMaxHealth);
            this.ProcessShaderProperties(this.shaderHealthPercentage);
        }

        private void OnEnable()
        {
            HudState.OnLeftAmmoChanged += this.UpdateLeftAmmoShader;
            HudState.OnRightAmmoChanged += this.UpdateRightAmmoShader;
            HudState.OnLeftReserveChanged += this.UpdateLeftReserveShader;
            HudState.OnRightReserveChanged += this.UpdateRightReserveShader;
            HudState.OnHealthChanged += this.UpdateHealthShader;
            HudState.OnHealthPercentageChanged += this.UpdateHealthPercentageShader;
            HudState.OnMaxHealthChanged += this.UpdateMaxHealthShader;
        }

        private void OnDisable()
        {
            HudState.OnLeftAmmoChanged -= this.UpdateLeftAmmoShader;
            HudState.OnRightAmmoChanged -= this.UpdateRightAmmoShader;
            HudState.OnLeftReserveChanged -= this.UpdateLeftReserveShader;
            HudState.OnRightReserveChanged -= this.UpdateRightReserveShader;
            HudState.OnHealthChanged -= this.UpdateHealthShader;
            HudState.OnHealthPercentageChanged += this.UpdateHealthPercentageShader;
            HudState.OnMaxHealthChanged -= this.UpdateMaxHealthShader;
        }

        private void UpdateLeftAmmoShader(int? ammo)
        {
            this.SetShaderInts(this.shaderLeftAmmo, ammo ?? 0);
        }

        private void UpdateRightAmmoShader(int? ammo)
        {
            this.SetShaderInts(this.shaderRightAmmo, ammo ?? 0);
        }

        private void UpdateLeftReserveShader(int? reserve)
        {
            this.SetShaderInts(this.shaderLeftReserve, reserve ?? 0);
        }

        private void UpdateRightReserveShader(int? reserve)
        {
            this.SetShaderInts(this.shaderRightReserve, reserve ?? 0);
        }

        private void UpdateHealthShader(float? health)
        {
            this.SetShaderFloatsWithModifier(this.shaderHealth, health ?? 0);
        }

        private void UpdateHealthPercentageShader(float? percentage)
        {
            this.SetShaderFloatsWithModifier(this.shaderHealthPercentage, percentage ?? 0f);
        }

        private void UpdateMaxHealthShader(float? maxHealth)
        {
            this.SetShaderFloatsWithModifier(this.shaderMaxHealth, maxHealth ?? 0);
        }

        private void SetShaderInts(List<ShaderIntReference> properties, int value)
        {
            foreach (ShaderIntReference property in properties)
            {
                property.material.SetInteger(property.propertyId, value);
            }
        }

        private void SetShaderFloatsWithModifier(List<ShaderFloatReference> properties, float value)
        {
            foreach (ShaderFloatReference property in properties)
            {
                property.material.SetFloat(property.propertyId, property.modifierExpression.Evaluate(value));
            }
        }
        
        private void ProcessShaderProperties(List<ShaderIntReference> properties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                ShaderIntReference property = properties[i];
                property.propertyId = Shader.PropertyToID(property.propertyName);
                properties[i] = property;
            }
        }
        
        private void ProcessShaderProperties(List<ShaderFloatReference> properties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                ShaderFloatReference property = properties[i];
                property.propertyId = Shader.PropertyToID(property.propertyName);
                property.modifierExpression = property.modifier.ToExpression();
                properties[i] = property;
            }
        }
    }
}