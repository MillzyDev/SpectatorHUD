using Ninject;
using SLZ.Rig;

namespace SpectatorHUD.Managers
{
    public class HudValueManager
    {
        private readonly RigManager _rigManager;

        [Inject]
        public HudValueManager(RigManager rigManager)
        {
            _rigManager = rigManager;
        }

        public float Health
        {
            get => _rigManager.health.curr_Health;
        }
    }
}
