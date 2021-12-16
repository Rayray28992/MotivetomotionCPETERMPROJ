using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class welcomeVoice : MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Button C;

    void Start()
    {
        actions.Add("continue", Continue);

        kR = new KeywordRecognizer(actions.Keys.ToArray());
        kR.OnPhraseRecognized += Recog;
        kR.Start();
    }

    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Continue()
    {
        C.onClick.Invoke();
        kR.Stop();
    }
}