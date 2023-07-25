using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioObjectSpawner : MonoBehaviour
{
    public ScenarioObjectType objectToSpawnHere;
    ScenarioObject objectSpawned = null;
    public bool isInPosition = false;
    public Ladder ladderPrefab;
    public Trampoline trampolinePrefab;
    Collider2D col;
    SpriteRenderer sprRender;
    private void Start()
    {
        col = GetComponent<Collider2D>();
        sprRender = GetComponent<SpriteRenderer>();
        SpawnObject();
    }
    void SpawnObject()
    {
        switch (objectToSpawnHere)
        {
            case ScenarioObjectType.LADDER:
                sprRender.sprite = ladderPrefab.GetComponent<SpriteRenderer>().sprite;
                objectSpawned = Instantiate(ladderPrefab, new Vector3(800, 800, 800), Quaternion.identity);
                break;
            case ScenarioObjectType.ELEVATOR:
                Debug.LogWarning("ELEVATOR NOT IMPLEMENTED");
                break;
            case ScenarioObjectType.TRAMPOLINE:
                sprRender.sprite = trampolinePrefab.GetComponent<SpriteRenderer>().sprite;
                objectSpawned = Instantiate(trampolinePrefab, new Vector3(800, 800, 800), Quaternion.identity);
                break;
            case ScenarioObjectType.DEATHZONE:
                Debug.LogWarning("DEATHZONE NOT IMPLEMENTED");
                break;
            case ScenarioObjectType.ICE:
                Debug.LogWarning("ICE NOT IMPLEMENTED");
                break;
            case ScenarioObjectType.CINTA:
                Debug.LogWarning("CINTA NOT IMPLEMENTED");
                break;
            case ScenarioObjectType.TRAMPILLA:
                Debug.LogWarning("TRAMPILLA NOT IMPLEMENTED");
                break;
            case ScenarioObjectType.NONE:
                break;
        }
    }

    private void Update()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convertir la posición del ratón a 2D (descartando la coordenada Z)
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        // Verificar si el ratón está sobre el objeto del mundo
        if (col.OverlapPoint(mousePosition2D))
        {
            if (sprRender.enabled == false) sprRender.enabled = true;
            //click izquierdo para colocar el objeto
            if (Input.GetMouseButtonDown(0))
            {
                //si ha hecho click mientras esta por encima, comprueba si el objeto estaba colocado o no, para hacer lo opuesto
                if (!isInPosition)
                {
                    if(LevelManager._instance.GetActiveSpawners(objectToSpawnHere) >= LevelManager._instance.GetMaxObjectsFromType(objectToSpawnHere))
                    {
                        LevelManager._instance.DespawnLast(objectToSpawnHere);
                    }
                    SpawnThis();
                }
                else
                {
                    DespawnThis();
                }
                EventsManager._instance.scenarioObjectChanged.Invoke();
            }
        }
        else
        {
            if (sprRender.enabled == true) sprRender.enabled = false;
        }
    }


    public void SpawnThis()
    {
        isInPosition = true;
        objectSpawned.SpawnScenarioObject(transform);
        LevelManager._instance.SetLastSpawned(this);
    }
    public void DespawnThis()
    {
        isInPosition = false;
        objectSpawned.DeSpawnScenarioObject();
    }

}
