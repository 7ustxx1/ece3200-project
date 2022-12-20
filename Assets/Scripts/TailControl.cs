using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TailControl : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float HP = 100;
    public GameObject waypoints;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Stage2Flag") == 1)
        {
            MoveUp();
            PlayerPrefs.SetString("barTitle", "Tail Health");

            if (HP == 0)
            {
                Die();
            }
        }

    }

    void MoveDown()
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

    void moveToPoint(int pointIndex)
    {
        Vector3 movetowards = Vector3.MoveTowards(transform.position, waypoints.transform.GetChild(pointIndex).position, Time.deltaTime * 20);
        transform.position = movetowards;
    }

    void Die()
    {
        PlayerPrefs.SetInt("Stage2Flag", 1);
        Destroy(gameObject);
    }

    void takeDamage(float amount)
    {
        HP -= amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            takeDamage(10);
        }
    }
}
