using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class backopVoice : MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Button Ba, Q;
    public GameObject pauseM;

    void Start()
    {
        actions.Add("back", Back);
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
    private void Back()
    {
        if (pauseM.activeSelf)
            Ba.onClick.Invoke();
    }
    public void Quit() //runs even when pause menu inactive in case user shouts out quit
    {
        kR.Stop();
        Debug.Log("QUIT");
        Q.onClick.Invoke();
    }
}
