using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public GameObject heart;
    public GameObject halfHeart;
    public GameObject emptyHeart;
    private float HP;

    // Start is called before the first frame update
    void Start()
    {
        HP = PlayerPrefs.GetFloat("playerHP");
        int heartNumber = (int)HP / 50;
    }

    // Update is called once per frame
    void Update()
    {
        HP = PlayerPrefs.GetFloat("playerHP");

    }
}
