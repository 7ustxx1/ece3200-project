using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailPartControl : MonoBehaviour
{
    public bool canDoRight;
    public bool canDoLeft;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            Debug.Log("hit");
            gameObject.GetComponentInParent<TailControl>().takeDamage(10);
        }
    }


}
