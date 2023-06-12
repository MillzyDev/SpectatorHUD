using MelonLoader;
using TMPro;
using UnityEngine;

namespace SpectatorHUD.Counters;

[RegisterTypeInIl2Cpp]
public abstract class Counter<T> : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    protected TextMeshProUGUI? text;
        
#pragma warning disable CS8618
    public T Value { get; set; }
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