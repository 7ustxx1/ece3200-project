using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TailControl : MonoBehaviour
{
    public float HP = 100;

    public bool tailOneUp = false;
    public bool tailOneDown = false;

    private bool healthSet = false;

    private int randomTail;

    private void Start()
    {
        randomTail = Random.Range(0, 3);
        PlayerPrefs.SetInt("CurrentTail", randomTail);
        StartCoroutine("DoAtteck");
    }




    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Stage1Flag") == 1)
        {
            PlayerPrefs.SetInt("SnakeEnable", 1);
            PlayerPrefs.SetFloat("snakeDropSpeed", 8);

            if (!healthSet)
            {
                PlayerPrefs.SetString("barTitle", "Tail Health");
                PlayerPrefs.SetFloat("enemyHP", 100f);
                healthSet = true;
            }

            if (!tailOneUp)
            {
                transform.GetChild(randomTail).GetComponent<TailPartControl>().MoveUp();
            }



            PlayerPrefs.SetFloat("enemyHP", HP);

            if (HP <= 0)
            {
                Die();
                if (tailOneDown)
                {
                    Destroy(gameObject);
                }
            }
        }

    }



    void Die()
    {
        PlayerPrefs.SetInt("Stage2Flag", 1);
        PlayerPrefs.SetInt("SnakeEnable", 0);
        GetComponentInChildren<TailPartControl>().MoveDown();
    }

    public void takeDamage(float amount)
    {
        HP -= amount;
    }

    IEnumerator DoAtteck()
    {
        while (true)
        {
            if (transform.GetChild(randomTail).GetComponent<TailPartControl>().isEnable)
            {
                transform.GetChild(randomTail).GetComponent<TailPartControl>().RandomAtteck();
            }

            yield return new WaitForSeconds(2f);
        }

    }
}