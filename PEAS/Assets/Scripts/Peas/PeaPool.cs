using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaPool : MonoBehaviour
{
    public static PeaPool Instance;
    //[HideInInspector]
    public List<List<GameObject>> pooledObjects;
    public BasicPea basicPeaPrefab;
    public int amountToPool;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //crea las listas de listas de guisantes segun el numero de tipos de guisantes definidos
        pooledObjects = new List<List<GameObject>>();
        PeaType[] enumValues = (PeaType[])Enum.GetValues(typeof(PeaType));
        int numPeas = enumValues.Length-1;
        for(int i = 0; i< numPeas; i++)
        {
            pooledObjects.Add(new List<GameObject>());
        }
        //inicializa cada una de las listas creando amountToPool guisantes en cada lista
        for(int i = 0; i< numPeas; i++)
        {
            List<GameObject> actuallist = pooledObjects[i];
            GameObject peaPrefab = null;
            GameObject tmp;
            //SELECTS PREFAB
            switch ((PeaType)i)
            {
                case PeaType.BASIC:
                    peaPrefab = basicPeaPrefab.gameObject;
                    break;
                case PeaType.KID:
                    Debug.LogError("KID NOT CREATED YET");
                    break;
                case PeaType.OLD:
                    Debug.LogError("OLD NOT CREATED YET");
                    break;
                case PeaType.SALARYMAN:
                    Debug.LogError("SALARYMAN NOT CREATED YET");
                    break;
                case PeaType.CARRIER:
                    Debug.LogError("CARRIER NOT CREATED YET");
                    break;
                case PeaType.DESTROYER:
                    Debug.LogError("DESTROYER NOT CREATED YET");
                    break;
                case PeaType.DOG:
                    Debug.LogError("DOG NOT CREATED YET");
                    break;
                case PeaType.COUPLE:
                    Debug.LogError("COUPLE NOT CREATED YET");
                    break;
            }

            for (int j = 0; j < amountToPool; j++)
            {
                if (peaPrefab != null)
                {
                    tmp = Instantiate(peaPrefab, transform);
                    tmp.SetActive(false);
                    actuallist.Add(tmp);
                }
            }
        }


    }

    public GameObject GetPooledObject(PeaType p)
    {
        foreach(GameObject go in pooledObjects[(int)p])
        {
            if (!go.activeInHierarchy)
                return go;
        }
        return null;
    }
}