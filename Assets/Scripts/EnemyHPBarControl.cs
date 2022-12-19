using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBarControl : MonoBehaviour
{
    public ProgressBar Bar;
    // Start is called before the first frame update
    void Start()
    {
        Bar.BarValue = 100;
        Bar.Title = "Egg Health";
        PlayerPrefs.SetString("barTitle", "Egg Health");
        PlayerPrefs.SetFloat("enemyHP", 100f);
    }

    // Update is called once per frame
    void Update()
    {
        Bar.Title = PlayerPrefs.GetString("barTitle");
        Bar.BarValue = PlayerPrefs.GetInt("enemyHP");
    }
}
