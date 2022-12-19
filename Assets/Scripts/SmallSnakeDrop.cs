using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SmallSnakeDrop : MonoBehaviour
{

    public GameObject snakePre;

    void Start()
    {
        StartCoroutine("Drop");
        PlayerPrefs.SetFloat("snakeDropSpeed", 10);
    }

    IEnumerator Drop()
    {
        while (true)
        {
            GameObject.Instantiate(snakePre, transform.GetChild(Random.Range(1, 7)));
        }
        yield return new WaitForSeconds(PlayerPrefs.GetFloat("snakeDropSpeed"));


    }
}
