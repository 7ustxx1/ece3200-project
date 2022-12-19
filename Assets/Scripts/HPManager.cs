using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    public ProgressBar playerBar;
    private float HP;

    // Start is called before the first frame update
    void Start()
    {
        HP = PlayerPrefs.GetFloat("playerHP");
        playerBar.BarValue = HP;
    }

    // Update is called once per frame
    void Update()
    {
        HP = PlayerPrefs.GetFloat("playerHP");
        playerBar.BarValue = HP;
    }
}
