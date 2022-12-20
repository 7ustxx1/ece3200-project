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
        GetComponentInChildren<TailPartControl>().MoveDown();
        Destroy(gameObject);
    }

    public void takeDamage(float amount)
    {

        HP -= amount;
    }


}
