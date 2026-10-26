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

using System.Collections;
using MelonLoader;
using UnityEngine;
using Random = System.Random;

namespace SpectatorHUD.Animation;

public class RandomAnimationParameter(IntPtr ptr) : MonoBehaviour(ptr)
{
    public Animator animator;
    public string parameterName;
    private int _parameterId;
    
    public float rangeMin;
    public float rangeMax;

    public bool doesCycle;
    public int bufferSize;
    public float interval;
    
    private float[] _buffer;
    private object _coroutine;

    private void Start()
    {
        this._parameterId = Animator.StringToHash(this.parameterName);
        
        float range = this.rangeMax - this.rangeMin;
        
        if (!this.doesCycle)
        {
            var rng = new Random();
            float n = this.rangeMin + (rng.NextSingle() * range);
            this.animator.SetFloat(this._parameterId, n);
            return;
        }

        this._buffer = new float[this.bufferSize];
        Parallel.For(0, this.bufferSize, i => this._buffer[i] = this.rangeMin + (Random.Shared.NextSingle() * range));

        this._coroutine = MelonCoroutines.Start(this.BufferCycle());
    }

    private void OnDestroy()
    {
        MelonCoroutines.Stop(this._coroutine);
    }

    private IEnumerator BufferCycle()
    {
        int i = 0;

        while (true)
        {
            if (i >= this.bufferSize)
            {
                i = 0;
            }
            
            this.animator.SetFloat(this._parameterId, this._buffer[i++]);
            yield return new WaitForSeconds(this.interval);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}