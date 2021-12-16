using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBrightness : MonoBehaviour
{
    public Slider slider;
    public Light sceneLight;

    // Update is called once per frame
    void Update()
    {
        sceneLight.intensity = slider.value;
    }
}
