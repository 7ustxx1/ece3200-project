using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailControl : MonoBehaviour
{

    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        MoveUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MoveDown()
    {
        if (transform.position.y >= -3.2)
        {
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }

    void MoveUp()
    {
        if (transform.position.y < -0.14)
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }

    void moveToPoint(int pointIndex)
    {

    }

    void Die()
    {
        PlayerPrefs.SetInt("Stage2Flag", 1);
        Destroy(gameObject);
    }
}
