using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    Button playButton;
    [SerializeField]
    Button creditsButton;
    [SerializeField]
    Button exitButton;
    [SerializeField]
    Button optionsButton;

    [SerializeField]
    GameObject creditsGameUI, mainGameUI;
    public void Play()
    {
        GameManager._instance.ChangeScene("SampleScene");
    }
    public void Options()
    {
        Debug.Log("change to options menu");
    }
    public void Credits()
    {
        creditsGameUI.SetActive(true);
        mainGameUI.SetActive(false);
    }
    public void Exit()
    {
        GameManager._instance.Exit();
    }
    public void GoToMain()
    {
        mainGameUI.SetActive(true);
        creditsGameUI.SetActive(false);
    }

}
