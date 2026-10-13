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
            // TODO: Shader MetaInfo
            // TODO: Debug GUI for shader elements
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
        }

        private void UpdateLeftAmmoShader(int? ammo)
        {
            this.SetShaderInts(this.shaderLeftAmmo, ammo ?? 0);
        }

        private void UpdateRightAmmoShader(int? ammo)
        {
            this.SetShaderInts(this.shaderRightAmmo, ammo ?? 0);
        }

        private void SetShaderInts(List<ShaderIntReference> properties, int value)
        {
            foreach (ShaderIntReference property in properties)
            {
                property.material.SetInteger(property.propertyId, value);
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