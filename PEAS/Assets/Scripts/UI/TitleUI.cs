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

    public void Options()
    {
        Debug.Log("change to options menu");
    }
    public void Credits()
    {
        Debug.Log("Change to credits menu");
    }
    public void Exit()
    {
        GameManager._instance.Exit();
    }

}
