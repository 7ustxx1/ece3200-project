using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float moveSpeed;
    public float damage;

    private float lifeTime = 1f;


    private void FixedUpdate()
    {
        transform.Translate(-Vector3.right * moveSpeed * Time.fixedDeltaTime);

        lifeTime -= Time.fixedDeltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            // should be changed to the enemy's scripts name
            GameObject go_other = other.GetComponent<GameObject>();
            doDamage(go_other);
        }

        Destroy(gameObject);
    }

    void doDamage(GameObject other)
    {
        //other.takeDamage(damage);
    }

}
