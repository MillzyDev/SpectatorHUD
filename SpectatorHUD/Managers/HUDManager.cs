using System;
using Ninject;
using UnityEngine;

namespace SpectatorHUD.Managers;

public class HUDManager : MonoBehaviour
{
    private HUDValueManager _hudValueManager = null!;
    
    [Inject]
    public void Inject(HUDValueManager hudValueManager)
    {
        _hudValueManager = hudValueManager;
    }

    private void Start()
    {
        
    }
}