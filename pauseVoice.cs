using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class pauseVoice : MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Button P;
    public GameObject pauseM;

    void Start()
    {
        actions.Add("pause", Pause);

        kR = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        kR.OnPhraseRecognized += Recog;
        kR.Start();
    }

    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Pause()
    {
        if (!pauseM.activeSelf)
            P.onClick.Invoke();
    }
}
