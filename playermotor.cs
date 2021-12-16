using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playermotor : MonoBehaviour
{
    public Slider mySlider;
    private CharacterController controller;
    private float speed = 5.0f;
    void Start()
    {
        mySlider.onValueChanged.AddListener(delegate{ValueChangeCheck();});
        speed = mySlider.value*15;
        controller = GetComponent<CharacterController> ();
    }
    void Update()
    {
        controller.Move ((Vector3.forward * speed) *
        Time.deltaTime);
    }
    public void ValueChangeCheck(){
    Debug.Log(mySlider.value);
    speed = mySlider.value*15;}
}
