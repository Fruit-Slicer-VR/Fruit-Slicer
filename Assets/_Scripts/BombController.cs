using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField]
    private ParticleSystem explosion;
    
    public void explode(){
        if(triggered == false)
        {
            explosion.Emit(1);
            triggered = true;
        }    
    }
}
