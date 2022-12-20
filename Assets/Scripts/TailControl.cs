using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TailControl : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float HP = 100;


    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Stage2Flag") == 1)
        {
            MoveUp();

            if (HP == 0)
            {
                Die();
            }
        }

    }

    void MoveDown(GameObject tail)
    {
        if (transform.position.y >= -3.2)
        {
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }

    void MoveUp()
    {
        if (transform.position.y < -0.14)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
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
