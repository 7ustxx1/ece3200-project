using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSnakeControl : MonoBehaviour
{

    public float moveSpeed = 1.0f;

    private bool moveLeftFlag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (moveLeftFlag)
        {
            moveLeft();
        }
        else
        {
            moveRight();
        }




    }

    void moveLeft()
    {
        GetComponent<SpriteRenderer>().flipX = true;
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }

    void moveRight()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            moveLeftFlag = !moveLeftFlag;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


}
