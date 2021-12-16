using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class loadBeach: MonoBehaviour
{
    private KeywordRecognizer kR;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public GameObject placeholder1;
    public Image loadingProgressBar;

    private AsyncOperation load;

    public void playBeach()
    {
        SceneManager.LoadScene("Beach");
        //load = SceneManager.LoadSceneAsync("Beach");
        //load.allowSceneActivation = false;
        //StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        while (!load.isDone)
        {
            totalProgress = Mathf.Clamp01(load.progress / 0.9f);
            loadingProgressBar.fillAmount = totalProgress;
            if (load.progress == 0.9f)
            {
                actions.Add("start", Continue);
                kR = new KeywordRecognizer(actions.Keys.ToArray());
                kR.OnPhraseRecognized += Recog;
                kR.Start();
                placeholder1.SetActive(true);
            }
            yield return null;
        }
    }

    private void Recog(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    private void Continue()
    {
        load.allowSceneActivation = true;
        kR.Stop();
        //if (Input.GetKeyDown(KeyCode.Space))
        //    load.allowSceneActivation = true;
    }
}
