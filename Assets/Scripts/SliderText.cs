using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{

    Text percentageText;

    public Slider speed;
    public int cps;



    void Start()
    {
        cps = 7;
        percentageText = GetComponent<Text>();
        speed = GetComponent<Slider>();
        cps = (int)speed.value;
    }

    // Update is called once per frame
    public void textUpdate(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 10) + "%";
        cps = (int)speed.value;

    }

}
