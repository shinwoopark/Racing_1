using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public ParticleSystem[] ParticleSystems;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Play(int particlesNumbers, bool b)
    {
        switch (particlesNumbers)
        {
            //Drift
            case 1:
                if (b)
                {
                    ParticleSystems[0].Play();
                    ParticleSystems[1].Play();
                }
                else
                {
                    ParticleSystems[0].Stop();
                    ParticleSystems[1].Stop();
                }               
                break;

        }
        
    }
}
