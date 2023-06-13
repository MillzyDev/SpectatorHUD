using System;
using MelonLoader;
using TMPro;
using UnityEngine;

namespace SpectatorHUD.Counters;

[RegisterTypeInIl2Cpp]
public abstract class FloatCounter : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    protected TextMeshProUGUI? text;
    
#pragma warning disable CS8618
    // ReSharper disable once PublicConstructorInAbstractClass
    public FloatCounter(IntPtr ptr) : base(ptr)
    {
    }

    public float Value { get; set; }
#pragma warning restore CS8618

    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        UpdateCounter();
    }

    protected abstract void UpdateCounter();
}