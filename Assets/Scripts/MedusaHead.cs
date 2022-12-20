using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaHead : MonoBehaviour
{

    public float health;
    public GameObject dieEffectPrefab;
    public GameObject AcidEffect;
    public GameObject GazeEffect;

    private Animator MedusaHeadAnimator;
    private AnimatorStateInfo stateInfo;
    private bool ableToAttack;

    void Start()
    {
        MedusaHeadAnimator = GetComponent<Animator>();
        ableToAttack = true;
        Gaze();
        PlayerPrefs.SetFloat("snakeDropSpeed", 1);
    }


    void Update()
    {
        stateInfo = MedusaHeadAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("MedusaHeadOpenEyes") && stateInfo.normalizedTime >= 1.0f)
        {
            MedusaHeadAnimator.SetInteger("Gaze", 2);
            if (ableToAttack)
            {
                GameObject effectIns = (GameObject)Instantiate(GazeEffect, transform.position, transform.rotation);
                Destroy(effectIns, 5f);
            }
            ableToAttack = false;
        }
        if (stateInfo.IsName("MedusaHeadCloseEyes") && stateInfo.normalizedTime >= 1.0f)
        {
            MedusaHeadAnimator.SetInteger("Gaze", 0);
            ableToAttack = true;
        }
        if (stateInfo.IsName("MedusaHeadOpenMouth") && stateInfo.normalizedTime >= 1.0f)
        {
            MedusaHeadAnimator.SetInteger("AcidRain", 2);
            if (ableToAttack)
            {
                GameObject effectIns = (GameObject)Instantiate(AcidEffect, transform.position, transform.rotation);
                Destroy(effectIns, 5f);
            }
            ableToAttack = false;
        }
        if (stateInfo.IsName("MedusaHeadCloseMouth") && stateInfo.normalizedTime >= 1.0f)
        {
            MedusaHeadAnimator.SetInteger("AcidRain", 0);
            ableToAttack = true;
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

    void Gaze()
    {
        MedusaHeadAnimator.SetInteger("Gaze", 1);

    }

    void AcidRain()
    {
        MedusaHeadAnimator.SetInteger("AcidRain", 1);
    }
}
