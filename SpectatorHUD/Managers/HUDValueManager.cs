using Ninject;
using SLZ.Rig;

namespace SpectatorHUD.Managers;

public class HUDValueManager
{
    private readonly RigManager _rigManager;
    
    [Inject]
    public HUDValueManager(RigManager rigManager)
    {
        _rigManager = rigManager;
    }

    public float Health => _rigManager.health.curr_Health;
}