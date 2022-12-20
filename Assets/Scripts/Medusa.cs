﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : MonoBehaviour
{
    public float health;
    public GameObject dieEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Stage2Flage") == 1)
        {
            MoveIn();
        }

    }

    void MoveIn()
    {
        if (transform.position.x > 4.5)
        {
            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            TakeDamage(1f);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(dieEffectPrefab, transform.position, transform.rotation);

        Destroy(gameObject);

        PlayerPrefs.SetInt("Stage4Flag", 1);

    }
}
