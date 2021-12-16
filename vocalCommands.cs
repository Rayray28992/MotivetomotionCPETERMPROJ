using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class vocalCommands : MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Button F, B, M, O, Q;
    public GameObject main;
    
    void Start()
    {
        F = GameObject.Find ("S.Button (1)").GetComponent<Button>();
        B = GameObject.Find ("S.Button (2)").GetComponent<Button>();
        M = GameObject.Find ("S.Button (3)").GetComponent<Button>();
        O = GameObject.Find ("Options Button").GetComponent<Button>();
        Q = GameObject.Find ("Quit Button").GetComponent<Button>();
        
        actions.Add("forest", Forest);
        actions.Add("beach", Beach);
        actions.Add("mountains", Mountains);
        actions.Add("options", Options);
        actions.Add("quit", Quit);
        
        kR = new KeywordRecognizer(actions.Keys.ToArray());
        kR.OnPhraseRecognized += Recog;
        kR.Start();
    }
    
    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Forest()
    {
        if (main.activeSelf)
            F.onClick.Invoke();
    }
    private void Beach()
    {
        if (main.activeSelf)
            B.onClick.Invoke();
    }
    private void Mountains()
    {
        if (main.activeSelf)
            M.onClick.Invoke();
    }
    private void Options()
    {
        if (main.activeSelf)
            O.onClick.Invoke();
    }
    
    public void Quit() //runs despite not showing
    {
        kR.Stop();
        Debug.Log("QUIT");
        Q.onClick.Invoke();
    }

}
