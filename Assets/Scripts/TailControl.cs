using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TailControl : MonoBehaviour
{


    public float HP = 100;


    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Stage2Flag") == 1)
        {

            if (HP == 0)
            {
                Die();
            }
        }

    }





    void Die()
    {
        PlayerPrefs.SetInt("Stage2Flag", 1);
        Destroy(gameObject);
    }

    public void takeDamage(float amount)
    {
        Debug.Log("HIT");
        HP -= amount;
    }


}
