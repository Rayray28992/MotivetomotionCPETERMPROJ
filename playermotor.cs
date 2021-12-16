using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playermotor : MonoBehaviour
{
        public Slider mySlider;
private CharacterController controller;
private float speed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        mySlider.onValueChanged.AddListener(delegate{ValueChangeCheck();});
        speed = mySlider.value*7;
       controller = GetComponent<CharacterController> ();
       
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move ((Vector3.forward * speed) * Time.deltaTime);
    }
    
    
    public void ValueChangeCheck(){
    Debug.Log(mySlider.value);
    speed = mySlider.value*7;
}
}
