using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class speedVoice : MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Slider speed;

    void Start()
    {
        actions.Add("up", Up);
        actions.Add("down", Down);

        kR = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        kR.OnPhraseRecognized += Recog;
        kR.Start();
    }

    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Up()
    {
        speed.value = speed.value+0.1f;
    }
    private void Down()
    {
        if (speed.value>=0.1)
            speed.value = speed.value - 0.1f;
    }
}

