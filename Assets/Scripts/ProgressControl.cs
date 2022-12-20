using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressControl : MonoBehaviour
{
    public ProgressBarCircle Bar;
    // Start is called before the first frame update
    void Start()
    {
        Bar.BarValue = 0;
        PlayerPrefs.SetInt("Stage1Flag", 0);
        PlayerPrefs.SetInt("Stage2Flag", 0);
        PlayerPrefs.SetInt("Stage3Flag", 0);
        PlayerPrefs.SetInt("Stage4Flag", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateBar()
    {
        Bar.BarValue = (PlayerPrefs.GetInt("Stage1Flag") + PlayerPrefs.GetInt("Stage2Flag") +
                        PlayerPrefs.GetInt("Stage3Flag") + PlayerPrefs.GetInt("Stage4Flag")) * 25;
    }
}
