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
    void Start()
    {
        InvokeRepeating("SpawnPea", firstSummonTime, summonFrequency);
    }

    void SpawnPea()
    {
        GameObject pea = null;
        //PeaType t = (PeaType)Random.Range(0, (int)PeaType.LASTPEA);
        PeaType t = PeaType.BASIC;
        pea = PeaPool.Instance.GetPooledObject(t);

        if (pea)
        {
            pea.SetActive(true);
            pea.transform.position = spawnPoint.position;
        }
    }
}
