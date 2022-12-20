using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailPartControl : MonoBehaviour
{
    public bool canDoRight;
    public bool canDoLeft;
    public Transform[] leftAtteckPosList;
    public Transform[] rightAtteckPosList;
    public float atteckRange;
    public bool isEnable;
    Animator animator;
    public float moveSpeed = 1f;
    public LayerMask whatIsPlayer;

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

    void Update()
    {

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
        isEnable = true;
        if (transform.position.y < -0.14)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
        else
        {
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
        Collider2D[] enemiseToDamage1;
        Collider2D[] enemiseToDamage2;
        enemiseToDamage1 = Physics2D.OverlapCircleAll(leftAtteckPosList[0].position, atteckRange, whatIsPlayer);
        enemiseToDamage2 = Physics2D.OverlapCircleAll(leftAtteckPosList[1].position, atteckRange, whatIsPlayer);
        for (int i = 0; i < enemiseToDamage1.Length; i++)
        {
            enemiseToDamage1[i].GetComponent<HeroKnight>().PlayerDamage(10f);
        }
        for (int i = 0; i < enemiseToDamage2.Length; i++)
        {
            enemiseToDamage2[i].GetComponent<HeroKnight>().PlayerDamage(10f);
        }
    }


    public void atteckRight()
    {
        animator.SetTrigger("doRight");
        Collider2D[] enemiseToDamage1;
        Collider2D[] enemiseToDamage2;
        enemiseToDamage1 = Physics2D.OverlapCircleAll(rightAtteckPosList[0].position, atteckRange, whatIsPlayer);
        enemiseToDamage2 = Physics2D.OverlapCircleAll(rightAtteckPosList[1].position, atteckRange, whatIsPlayer);
        for (int i = 0; i < enemiseToDamage1.Length; i++)
        {
            enemiseToDamage1[i].GetComponent<HeroKnight>().PlayerDamage(10f);
        }
        for (int i = 0; i < enemiseToDamage2.Length; i++)
        {
            enemiseToDamage2[i].GetComponent<HeroKnight>().PlayerDamage(10f);
        }
    }

}
