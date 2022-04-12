using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemParticleNotActive : PoolItem
{
    public ParticleSystem particle;
    public override void Create()
    {
        particle.Stop(true);
    }

    public override void Spawn()
    {
        particle.Play(true);
    }

    public override void Recycle()
    {
        particle.Stop(true);
    }
}
