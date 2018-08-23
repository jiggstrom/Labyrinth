using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadManager : MonoBehaviour {

    public Animator animator;
    private string levelToLoad;

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
    void Update () {
        //if (Input.GetMouseButtonDown(0))
        //    FadeToLevel("Meny");
	}

    public void FadeToLevel(string name)
    {
        levelToLoad = name;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
