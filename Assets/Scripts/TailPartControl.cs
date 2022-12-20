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

    public float moveSpeed = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            Debug.Log("hit");
            gameObject.GetComponentInParent<TailControl>().takeDamage(10);
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
}
