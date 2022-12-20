using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EggControl : MonoBehaviour
{
    private float HP = 100;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    void Die()
    {
        animator.SetBool("isDead", true);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
