using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeEffectTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnParticleCollision(GameObject other)
    {

        if (other.tag == "Player")
        {
            
            GameObject.Find("HeroKnight").GetComponent<HeroKnight>().SendMessage("setgazeHurt", true);
        }
    }
}
