using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailPartControl : MonoBehaviour
{
    public bool canDoRight;
    public bool canDoLeft;
    public bool isEnable;
    Animator animator;
    public float moveSpeed = 1f;
    public LayerMask whatIsPlayer;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt" && isEnable)
        {
            gameObject.GetComponentInParent<TailControl>().takeDamage(10);
        }
        else if (collision.tag == "Waveform" && isEnable)
        {
            gameObject.GetComponentInParent<TailControl>().takeDamage(5);
        }
        else if (collision.tag == "Crossed" && isEnable)
        {
            gameObject.GetComponentInParent<TailControl>().takeDamage(20);
        }
    }


    public void MoveDown()
    {
        isEnable = false;
        if (transform.position.y >= -3.2)
        {
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.GetComponentInParent<TailControl>().tailOneDown = true;
        }
    }

    public void MoveUp()
    {

        if (transform.position.y < 0.3)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            isEnable = true;
            transform.GetComponentInParent<TailControl>().tailOneUp = true;
        }
    }

    public void TakeDamage(float amount)
    {
        gameObject.GetComponentInParent<TailControl>().takeDamage(amount);
    }

    public void RandomAtteck()
    {
        int randomIndex;
        if (!canDoLeft)
        {
            randomIndex = Random.Range(0, 2);
        }
        else if (!canDoRight)
        {
            randomIndex = Random.Range(1, 3);
        }
        else
        {
            randomIndex = Random.Range(0, 3);
        }
        Debug.Log(randomIndex);
        if (randomIndex == 0)
        {
            atteckLeft();
        }

        if (randomIndex == 2)
        {
            atteckRight();
        }

    }

    public void atteckLeft()
    {
        animator.SetTrigger("doLeft");
    }


    public void atteckRight()
    {
        animator.SetTrigger("doRight");

    }

    public void activateLeftTrigger()
    {
        transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
    }
    public void activateRightTrigger()
    {
        transform.GetChild(1).GetComponent<Collider2D>().enabled = true;
    }
    public void deactivateLeftTrigger()
    {
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
    }
    public void deactivateRightTrigger()
    {
        transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
    }
}
