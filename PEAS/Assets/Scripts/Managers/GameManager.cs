using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager _instance { get; private set; }
    [SerializeField]
    LevelManager m_LevelManager;

    private void Awake()
    {
        if (_instance == null)
        {
            Debug.Log("GameManager instanced");
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            _instance.m_LevelManager = m_LevelManager;
            Destroy(gameObject);
        }
        
    }
}
