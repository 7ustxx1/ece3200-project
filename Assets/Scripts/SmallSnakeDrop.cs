using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SmallSnakeDrop : MonoBehaviour
{

    public GameObject snakePre;
    private int snakeEnable = 0;

    void Start()
    {
        StartCoroutine("Drop");
        PlayerPrefs.SetFloat("snakeDropSpeed", 5);
        PlayerPrefs.SetInt("SnakeEnable", 0);
    }

    private void Update()
    {
        snakeEnable = PlayerPrefs.GetInt("SnakeEnable");
    }

    IEnumerator Drop()
    {
        while (true)
        {
            if (snakeEnable != 0)
            {
                GameObject.Instantiate(snakePre, transform.GetChild(Random.Range(1, 8)));

            }
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("snakeDropSpeed"));
        }



    }
}