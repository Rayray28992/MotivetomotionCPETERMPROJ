using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class volbrightVoice : MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Slider v, b;
    public GameObject pauseM;

    void Start()
    {
        actions.Add("dimmer", Dim);
        actions.Add("bright", B);
        actions.Add("louder", L);
        actions.Add("quieter", Q);

        kR = new KeywordRecognizer(actions.Keys.ToArray());
        kR.OnPhraseRecognized += Recog;
        kR.Start();
    }

    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Dim()
    {
        if (pauseM.activeSelf)
            if (b.value >= 0.1)
                b.value = b.value - 0.1f;
    }
    private void B()
    {
        if (pauseM.activeSelf)
            b.value = b.value + 0.1f;
    }
    private void L()
    {
        if (pauseM.activeSelf)
            v.value = v.value + 0.1f;
    }
    private void Q()
    {
        if (pauseM.activeSelf)
            if (v.value >= 0.1)
                v.value = v.value - 0.1f;
    }
}
