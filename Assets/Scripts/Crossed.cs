using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossed : MonoBehaviour
{
    public GameObject CrossedHitPrefab;
    public GameObject t_hit;
    public float moveSpeed;

    private float lifeTime = 3f;


    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime);

        lifeTime -= Time.fixedDeltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        t_hit = Instantiate<GameObject>(CrossedHitPrefab, transform.position, transform.rotation) as GameObject;

        Destroy(gameObject);
    }

}
