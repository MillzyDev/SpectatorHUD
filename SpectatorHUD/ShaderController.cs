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
    }
}