using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance { get; private set; }
    public int level_ID;
    public int maxTime = -1;
    int points;
    int actualTime;

    private void Awake()
    {
        if (_instance == null)
        {
            Debug.Log("LevelManager instanced");
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        EventsManager._instance.addPoints.AddListener(AddPoints);
        if (maxTime > 0)
        {
            actualTime = maxTime;
            Invoke("EndLevel", maxTime);
            InvokeRepeating("DecreaseTime", 0, 1);
        }
    }

    void AddPoints(int p)
    {
        points += p; 
        Debug.Log(points);
    }
    void DecreaseTime()
    {
        actualTime -= 1;
        Debug.Log("Remaining: " + actualTime + " s");
    }
    void EndLevel()
    {
        Debug.Log("LEVEL ENDED");
    }


}
