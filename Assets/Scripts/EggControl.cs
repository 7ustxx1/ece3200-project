using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EggControl : MonoBehaviour
{
    private float HP = 100f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerPrefs.SetString("barTitle", "Egg Health");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("enemyHP", HP);
        if (HP <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bolt")
        {
            TakeDamage(10f);
        }
        else if (collision.tag == "Waveform")
        {
            TakeDamage(5f);
        }
        else if (collision.tag == "Crossed")
        {
            TakeDamage(20f);
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
    }

    public void Remove()
    {
        PlayerPrefs.SetInt("Stage1Flag", 1);
        Destroy(gameObject);

    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
    }
}
