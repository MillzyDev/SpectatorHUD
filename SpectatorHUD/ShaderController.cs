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

using SpectatorHUD.Shading;
using UnityEngine;

namespace SpectatorHUD;

public class ShaderController(IntPtr ptr) : MonoBehaviour(ptr)
{
    public List<ShaderIntReference> shaderLeftAmmo;
    public List<ShaderIntReference> shaderRightAmmo;
    public List<ShaderIntReference> shaderLeftReserve;
    public List<ShaderIntReference> shaderRightReserve;
    public List<ShaderIntReference> shaderTotalLeftAmmo;
    public List<ShaderIntReference> shaderTotalRightAmmo;
    public List<ShaderIntReference> shaderTotalLightAmmo;
    public List<ShaderIntReference> shaderTotalMediumAmmo;
    public List<ShaderIntReference> shaderTotalHeavyAmmo;
    public List<ShaderFloatReference> shaderHealth;
    public List<ShaderFloatReference> shaderMaxHealth;
    public List<ShaderFloatReference> shaderHealthPercentage;

    private void Start()
    {
        MetaInfo.ShaderLeftAmmo = this.shaderLeftAmmo.Count;
        MetaInfo.ShaderRightAmmo = this.shaderRightAmmo.Count;
        MetaInfo.ShaderLeftReserve = this.shaderLeftReserve.Count;
        MetaInfo.ShaderRightReserve = this.shaderRightReserve.Count;
        MetaInfo.ShaderTotalLeftAmmo = this.shaderTotalLeftAmmo.Count;
        MetaInfo.ShaderTotalRightAmmo = this.shaderTotalRightAmmo.Count;
        MetaInfo.ShaderTotalLightAmmo = this.shaderTotalLightAmmo.Count;
        MetaInfo.ShaderTotalMediumAmmo = this.shaderTotalMediumAmmo.Count;
        MetaInfo.ShaderTotalHeavyAmmo = this.shaderTotalHeavyAmmo.Count;
        MetaInfo.ShaderHealth = this.shaderHealth.Count;
        MetaInfo.ShaderMaxHealth = this.shaderMaxHealth.Count;
        MetaInfo.ShaderHealthPercentage = this.shaderHealthPercentage.Count;

        this.ProcessShaderProperties(this.shaderLeftAmmo);
        this.ProcessShaderProperties(this.shaderRightAmmo);
        this.ProcessShaderProperties(this.shaderLeftReserve);
        this.ProcessShaderProperties(this.shaderRightReserve);
        this.ProcessShaderProperties(this.shaderTotalLeftAmmo);
        this.ProcessShaderProperties(this.shaderTotalRightAmmo);
        this.ProcessShaderProperties(this.shaderTotalLightAmmo);
        this.ProcessShaderProperties(this.shaderTotalMediumAmmo);
        this.ProcessShaderProperties(this.shaderTotalHeavyAmmo);
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
        HudState.OnHealthPercentageChanged -= this.UpdateHealthPercentageShader;
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
        for (var i = 0; i < properties.Count; i++)
        {
            ShaderIntReference property = properties[i];
            property.propertyId = Shader.PropertyToID(property.propertyName);
            properties[i] = property;
        }
    }

    private void ProcessShaderProperties(List<ShaderFloatReference> properties)
    {
        for (var i = 0; i < properties.Count; i++)
        {
            ShaderFloatReference property = properties[i];
            property.propertyId = Shader.PropertyToID(property.propertyName);
            property.modifierExpression = property.modifier.ToExpression();
            properties[i] = property;
        }
    }
}