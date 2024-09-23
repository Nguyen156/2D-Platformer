using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_FadeEffect : MonoBehaviour
{
    public static UI_FadeEffect instance;

    public Animator anim;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        anim = GetComponent<Animator>();
    }

    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(NextSceneCoroutine(sceneName));
    }

    private IEnumerator NextSceneCoroutine(string sceneName)
    {
        anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        //anim.SetTrigger("fadeIn");
    }
}
