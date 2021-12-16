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
    public Button Y1, Y2, Y3, N1, N2, N3;
    public GameObject sure1, sure2, sure3;

    void Start()
    {
        actions.Add("yes", Yes);
        actions.Add("no", No);

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
        if (sure1.activeSelf)
        {
            Y1.onClick.Invoke();
        }
        if (sure2.activeSelf)
        {
            Y2.onClick.Invoke();
        }
        if (sure3.activeSelf)
        {
            Y3.onClick.Invoke();
        }
        kR.Stop();
    }
    private void No()
    {
        if (sure1.activeSelf)
        {
            N1.onClick.Invoke();
        }
        if (sure2.activeSelf)
        {
            N2.onClick.Invoke();
        }
        if (sure3.activeSelf)
        {
            N3.onClick.Invoke();
        }
    }
}