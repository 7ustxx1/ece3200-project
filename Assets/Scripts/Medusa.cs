using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : MonoBehaviour
{
    public float health;
    public GameObject dieEffectPrefab;
    public GameObject GazeBornPos; 
    public GameObject GazeEffect;

    private Animator MedusaBodyAnimator;
    private AnimatorStateInfo stateInfo;
    private bool ableToAttack;
    private bool healthSet = false;
    private float attackInterval = 4f;
    private float attackCooldown = 5f;
    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        MedusaBodyAnimator = GetComponent<Animator>();
        ableToAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Stage2Flag") == 1)
        {
            PlayerPrefs.SetInt("SnakeEnable", 1);
            PlayerPrefs.SetFloat("snakeDropSpeed", 8);
            if (!healthSet)
            {
                PlayerPrefs.SetString("barTitle", "Body Health");
                PlayerPrefs.SetFloat("enemyHP", health);
                health = 100;
                healthSet = true;
            }
            MoveIn();
            PlayerPrefs.SetFloat("enemyHP", health);
            RandomAttack();
        }

        stateInfo = MedusaBodyAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("MedusaBodyGaze") && stateInfo.normalizedTime >= 1.0f)
        {
            MedusaBodyAnimator.SetInteger("Gaze", 0);
        }
        if (stateInfo.IsName("MedusaBodyHairAttack") && stateInfo.normalizedTime >= 1.0f)
        {
            MedusaBodyAnimator.SetInteger("HairAttack", 0);
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
            TakeDamage(5f);
        }
        else if (collision.tag == "Waveform")
        {
            TakeDamage(2f);
        }
        else if (collision.tag == "Crossed")
        {
            TakeDamage(8f);
        }
    }

    public void TakeDamage(float damage)
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

        PlayerPrefs.SetInt("Stage3Flag", 1);

    }

    void Gaze()
    {
        MedusaBodyAnimator.SetInteger("Gaze", 1);
    }

    void GazeGenerate()
    {
        GameObject effectIns = (GameObject)Instantiate(GazeEffect, GazeBornPos.transform.position, transform.rotation);
        Destroy(effectIns, 5f);
    }

    void HairAttack()
    {
        MedusaBodyAnimator.SetInteger("HairAttack", 1);
    }

    void RandomAttack()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        if (attackCooldown <= 0)
        {
            randomIndex = Random.Range(0, 2);
            if (randomIndex == 0)
            {
                Gaze();
            }
            if (randomIndex == 1)
            {
                HairAttack();
            }
            attackCooldown = attackInterval;
        }
    }

    public void activateHairAttackTrigger()
    {
        transform.GetChild(2).GetComponent<Collider2D>().enabled = true;
    }
    public void deactivateHairAttackTrigger()
    {
        transform.GetChild(2).GetComponent<Collider2D>().enabled = false;
    }
}
