using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private ParticleSystem ps;

    private void wake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if(ps && !ps.IsAlive())
        {
            DestroySelf();
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
