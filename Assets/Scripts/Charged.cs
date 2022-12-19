﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charged : MonoBehaviour
{
    public float moveSpeed;

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
        Destroy(gameObject);
    }

}
