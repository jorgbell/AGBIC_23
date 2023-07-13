using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnPoint;
    [SerializeField]
    float summonFrequency = 3.0f, firstSummonTime = 0;
    // Start is called before the first frame update
    [SerializeField]
    SpriteRenderer nextPeaPortraitHolder;
    GameObject nextPea;
    void Start()
    {
        SetNextPea();
        InvokeRepeating("SpawnPea", firstSummonTime, summonFrequency);
    }

    void SpawnPea()
    {
        if (nextPea)
        {
            nextPea.SetActive(true);
            nextPea.transform.position = spawnPoint.position;
        }
        SetNextPea();
    }
    void SetNextPea()
    {
        //PeaType t = (PeaType)Random.Range(0, (int)PeaType.LASTPEA);
        PeaType t = PeaType.BASIC;
        nextPea = PeaPool.Instance.GetPooledObject(t);
        if(nextPea)
        {
            nextPeaPortraitHolder.sprite = nextPea.gameObject.GetComponent<SpriteRenderer>().sprite;
            nextPeaPortraitHolder.color = nextPea.gameObject.GetComponent<SpriteRenderer>().color;
        }

    }

}
