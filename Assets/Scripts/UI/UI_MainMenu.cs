using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private GameObject[] uiElements;
    private Image img;

    public void Play()
    {
        UI_FadeEffect.instance.LoadNextScene(sceneName);
    }

    public void SwitchUI(GameObject uiToEnable)
    {
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }

        uiToEnable.SetActive(true);
    }
}
