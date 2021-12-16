using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class sureVoice : MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Button Y, N;
    public GameObject sceneM;

    void Start()
    {
        if (sceneM.activeSelf)
        { 
            actions.Add("yes", Yes);
            actions.Add("no", No);
        }

        kR = new KeywordRecognizer(actions.Keys.ToArray());
        kR.OnPhraseRecognized += Recog;
        kR.Start();
    }

    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Yes()
    {
        Y.onClick.Invoke();
        kR.Stop();
    }
    private void No()
    {
        N.onClick.Invoke();
    }
}