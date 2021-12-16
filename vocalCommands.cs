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
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Button C, F, B, M, O, Q, N1, N2, N3, Y1, Y2, Y3, x, y;
    
    void Start()
    {
        C = GameObject.Find ("Cont").GetComponent<Button>();
        F = GameObject.Find ("S.Button (1)").GetComponent<Button>();
        B = GameObject.Find ("S.Button (2)").GetComponent<Button>();
        M = GameObject.Find ("S.Button (3)").GetComponent<Button>();
        O = GameObject.Find ("Options Button").GetComponent<Button>();
        Q = GameObject.Find ("Quit Button").GetComponent<Button>();
        
        N1 = GameObject.Find ("S.(1) sure?/No1").GetComponent<Button>();
        N2 = GameObject.Find ("S.(2) sure?/No2").GetComponent<Button>();
        N3 = GameObject.Find ("S.(3) sure?/No3").GetComponent<Button>();
        
        Y1 = GameObject.Find ("S.(1) sure?/Yes1").GetComponent<Button>();
        Y2 = GameObject.Find ("S.(2) sure?/Yes2").GetComponent<Button>();
        Y3 = GameObject.Find ("S.(3) sure?/Yes3").GetComponent<Button>();
        
        actions.Add("continue", Continue);
        actions.Add("forest", Forest);
        actions.Add("beach", Beach);
        actions.Add("mountains", Mountains);
        actions.Add("options", Options);
        actions.Add("quit", Quit);
        actions.Add("yes", ()=>Yes(x));
        actions.Add("no", ()=>No(y));
        
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += Recog;
        keywordRecognizer.Start();
    }
    
    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Continue()
    {
        C.onClick.Invoke();
    }
    private void Forest()
    {
        x=Y1;
        y=N1;
        F.onClick.Invoke();
    }
    private void Beach()
    {
        x=Y2;
        y=N2;
        B.onClick.Invoke();
    }
    private void Mountains()
    {
        x=Y3;
        y=N3;
        M.onClick.Invoke();
    }
    private void Options()
    {
        O.onClick.Invoke();
    }
    
    private void Yes(Button  x)
    {
        x.onClick.Invoke();
    }
    private void No(Button y)
    {
        y.onClick.Invoke();
    }
    
    public void Quit()
    {
        keywordRecognizer.Stop();
        Debug.Log("QUIT");
        Q.onClick.Invoke();
    }

}
