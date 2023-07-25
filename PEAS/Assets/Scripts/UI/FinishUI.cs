using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishUI : MonoBehaviour
{
    public void Title()
    {
        GameManager._instance.ChangeScene("TitleScene");
    }
}
