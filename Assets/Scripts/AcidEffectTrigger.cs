using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffectTrigger : MonoBehaviour
{
    //private ParticleSystem ps;

    void Start()
    {
        //ps = transform.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {

        if (other.tag == "Player")
        {
            //Debug.Log("碰撞到了Player");
            GameObject.Find("HeroKnight").GetComponent<HeroKnight>().SendMessage("setacidHurt", true);
        }
    }

}
