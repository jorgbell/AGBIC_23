using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [HideInInspector]
    public int[] pointsData = new int[0];
    public static DataManager _instance { get; private set; }
    private void Awake()
    {
        if (_instance == null)
        {
            Debug.Log("DataManager instanced");
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int GetPoints(PeaType p)
    {
        return pointsData[(int)p];
    }


}
