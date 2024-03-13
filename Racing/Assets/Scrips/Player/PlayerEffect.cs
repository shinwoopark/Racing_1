using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public GameObject[] ParticleSystems;
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
                    ParticleSystems[0].SetActive(true);
                    ParticleSystems[1].SetActive(true);
                }
                else
                {
                    ParticleSystems[0].SetActive(false);
                    ParticleSystems[1].SetActive(false);
                }               
                break;

        }
        
    }
}
