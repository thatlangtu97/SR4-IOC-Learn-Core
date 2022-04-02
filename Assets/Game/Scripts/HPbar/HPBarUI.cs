using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Image slider;
    public Image sliderAfter;
    public Text textValue;

    public float timelerp;
    public AnimationCurve curveLerp;

    public void Setvalue(float value, float maxvalue)
    {
        slider.fillAmount = value / maxvalue;
        textValue.text = $"{(int) value} / {(int) maxvalue}";
    }
    
    public void Setvalue(int value, int maxvalue)
    {
        slider.fillAmount = (float)value / (float)maxvalue;
        textValue.text = $"{value} / {maxvalue}";
    }

    private void OnEnable()
    {
        slider.fillAmount = 1f;
        sliderAfter.fillAmount = 1f;
    }

    public void Update()
    {

        if (sliderAfter.fillAmount > slider.fillAmount)
        {
            timelerp = Mathf.Clamp(timelerp +Time.deltaTime, 0f, 1f);
            sliderAfter.fillAmount = Mathf.Lerp(sliderAfter.fillAmount, slider.fillAmount, curveLerp.Evaluate(timelerp));
        }
        else
        {
            timelerp = 0;
            sliderAfter.fillAmount = slider.fillAmount;
        }

//        if (slider.fillAmount > sliderAfter.fillAmount)
//        {
//            
//        }
    }
}
