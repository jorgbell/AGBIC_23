using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance { get; private set; }
    public int level_ID;
    public int maxTime = -1;
    int points;
    int actualTime;
    [HideInInspector]
    public int nScenarioObjects = 0;
    [HideInInspector]
    public int[] MaxActiveObjectsInLevel = new int[0];
    public GameObject spawnersHolder;
    [HideInInspector]
    public List<List<ScenarioObjectSpawner>> scenarioObjects;
    int[] lastSpawned;

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
        InitScenarioObjects();
    }

    void InitScenarioObjects()
    {
        scenarioObjects = new List<List<ScenarioObjectSpawner>>();
        for(int i = 0; i< nScenarioObjects; i++)
        {
            scenarioObjects.Add(new List<ScenarioObjectSpawner>());
        }
        ScenarioObjectSpawner[] spawners = spawnersHolder.GetComponentsInChildren<ScenarioObjectSpawner>();
        foreach (ScenarioObjectSpawner s in spawners)
        {
            scenarioObjects[(int)s.objectToSpawnHere].Add(s);
        }
        lastSpawned = new int[scenarioObjects.Count];
        for(int i = 0; i< scenarioObjects.Count; i++) { lastSpawned[i] = -1; }
    }

    private void Start()
    {
        EventsManager._instance.addPoints.AddListener(AddPoints);
        if (maxTime > 0)
        {
            actualTime = maxTime;
            EventsManager._instance.changeTime.Invoke(actualTime);
            Invoke("EndLevel", maxTime);
            InvokeRepeating("DecreaseTime", 0, 1);
        }
        else
        {
            EventsManager._instance.changeTime.Invoke(int.MaxValue);
        }
    }

    void AddPoints(int p)
    {
        points += p; 
    }
    void DecreaseTime()
    {
        actualTime -= 1;
        EventsManager._instance.changeTime.Invoke(actualTime);
    }
    void EndLevel()
    {
        Debug.Log("LEVEL ENDED");
    }

    public int GetActiveSpawners(ScenarioObjectType type)
    {
        int nActives = 0;
        List<ScenarioObjectSpawner> l = scenarioObjects[(int)type];
        foreach(ScenarioObjectSpawner s in l)
        {
            if (s.isInPosition) nActives++;
        }
        return nActives;
    }
    public int GetInactiveSpawners(ScenarioObjectType type)
    {
        return MaxActiveObjectsInLevel[(int)type] - GetActiveSpawners(type);
    }
    public int GetMaxObjectsFromType(ScenarioObjectType type)
    {
        return MaxActiveObjectsInLevel[(int)type];
    }
    public void SetLastSpawned(ScenarioObjectSpawner spawner)
    {
        ScenarioObjectType t = spawner.objectToSpawnHere;
        int index = scenarioObjects[(int)t].IndexOf(spawner);
        if (index >= 0) lastSpawned[(int)t] = index;
    }
    public void DespawnLast(ScenarioObjectType type)
    {
        if (lastSpawned[(int)type] >= 0)
            scenarioObjects[(int)type][lastSpawned[(int)type]].DespawnThis();
    }
}
