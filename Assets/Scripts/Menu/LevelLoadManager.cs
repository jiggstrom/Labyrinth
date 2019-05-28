using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour {

    public Animator animator;
    private string levelToLoad;
    public TMP_Text text;
    private float wait = 0f;

    #region "Singelton"
    public static LevelLoadManager instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple instances of LevelManager found!");
            return;
        }
        instance = this;
    }
    #endregion

    // Update is called once per frame
    void Update ()
    {
        //if (Input.GetMouseButtonDown(0))
        //    DoLoad();
    }

    public void FadeToLevel(string name)
    {
        levelToLoad = name;
        text.text = name;
        animator.SetTrigger("FadeOut");
    }

    public IEnumerator OnFadeComplete()
    {
        if (wait > 0)
        {
            yield return new WaitForSecondsRealtime(wait);
        }

        wait = 0f;
        DoLoad();        
    }
    public void DoLoad()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Greet(string message, string levelName)
    {
        levelToLoad = levelName;
        text.text = message;
        wait = 10f;
        animator.SetTrigger("FadeOut");
    }

}
