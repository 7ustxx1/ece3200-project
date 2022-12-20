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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt" && isEnable)
        {
            gameObject.GetComponentInParent<TailControl>().takeDamage(10);
        }
    }

    public void MoveDown()
    {
        isEnable = false;
        if (transform.position.y >= -3.2)
        {
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }

    public void MoveUp()
    {
        isEnable = true;
        if (transform.position.y < -0.14)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }

    public void TakeDamage(float amount)
    {
        gameObject.GetComponentInParent<TailControl>().takeDamage(amount);
    }


}
