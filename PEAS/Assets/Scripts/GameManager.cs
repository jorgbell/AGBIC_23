using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public static GameManager _instance { get; private set; }


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
            //Asigna el roundmanager de la escena cuando se carga
            //_instance.m_roundManager = m_roundManager;
            Destroy(gameObject);
        }
    }
}
