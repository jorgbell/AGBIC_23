using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventsManager : MonoBehaviour
{
    public static EventsManager _instance { get; private set; }
    public UnityEvent<int> addPoints { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            Debug.Log("EventsManager instanced");
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            //_instance.m_roundManager = m_roundManager;
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        addPoints = new UnityEvent<int>();
    }

}
