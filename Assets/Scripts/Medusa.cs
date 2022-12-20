using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medusa : MonoBehaviour
{
    public float health;
    public GameObject dieEffectPrefab;

    private Animator MedusaBodyAnimator;
    private AnimatorStateInfo stateInfo;
    private bool ableToAttack;
    private bool healthSet = false;

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
            if (!healthSet)
            {
                PlayerPrefs.SetString("barTitle", "Tail Health");
                PlayerPrefs.SetFloat("enemyHP", health);
                healthSet = true;
            }
            MoveIn();
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
        MedusaBodyAnimator.SetInteger("Gaze", 1);
    }

    void HairAttack()
    {
        MedusaBodyAnimator.SetInteger("HairAttack", 1);
    }
}
