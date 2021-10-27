using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_SceneLoader : MonoBehaviour
{
    public Animator transitionAnimation;
    public static Animator _transitionAnimation;

    public Canvas canvas;

    private void Awake()
    {
        _transitionAnimation = transitionAnimation;
    }

    public static IEnumerator LoadToScene(string sceneName)
    {
        _transitionAnimation.SetBool("Transition", false);
        Time.timeScale = 1;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName);
    }

    public static void StartScene()
    {
        _transitionAnimation.SetBool("Transition", true);
    }
}
