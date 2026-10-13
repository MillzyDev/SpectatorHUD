using SpectatorHUD.Expressions;
using SpectatorHUD.Shading;
using UnityEngine;

namespace SpectatorHUD
{
    public class ShaderController : MonoBehaviour
    {
        public List<ShaderBoolReference> shaderLeftGunHeld;
        public List<ShaderBoolReference> shaderRightGunHeld;
        
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
            this.ProcessShaderProperties(this.shaderLeftGunHeld);
            this.ProcessShaderProperties(this.shaderRightGunHeld);
            this.ProcessShaderProperties(this.shaderLeftAmmo);
            this.ProcessShaderProperties(this.shaderRightAmmo);
            this.ProcessShaderProperties(this.shaderLeftReserve);
            this.ProcessShaderProperties(this.shaderRightReserve);
            this.ProcessShaderProperties(this.shaderHealth);
            this.ProcessShaderProperties(this.shaderMaxHealth);
            this.ProcessShaderProperties(this.shaderHealthPercentage);
        }

        private void ProcessShaderProperties(List<ShaderBoolReference> properties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                ShaderBoolReference property = properties[i];
                property.propertyId = Shader.PropertyToID(property.propertyName);
                properties[i] = property;
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