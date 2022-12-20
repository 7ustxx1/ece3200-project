using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SmallSnakeDrop : MonoBehaviour
{

    public GameObject snakePre;
    private bool snakeEnable = false;

    void Start()
    {
        StartCoroutine("Drop");
        PlayerPrefs.SetFloat("snakeDropSpeed", 5);
    }

    IEnumerator Drop()
    {
        while (true)
        {
            if (snakeEnable)
            {
                GameObject.Instantiate(snakePre, transform.GetChild(Random.Range(1, 8)));

            }
            yield return new WaitForSeconds(PlayerPrefs.GetFloat("snakeDropSpeed"));
        }



    }
}
