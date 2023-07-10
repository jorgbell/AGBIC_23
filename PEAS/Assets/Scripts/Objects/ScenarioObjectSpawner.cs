using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioObjectSpawner : MonoBehaviour
{
    public ScenarioObjectType objectToSpawnHere;
    ScenarioObject objectSpawned = null;
    bool isInPosition = false;
    public Ladder ladderPrefab;
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
                sprRender.color = ladderPrefab.GetComponent<SpriteRenderer>().color;
                objectSpawned = Instantiate(ladderPrefab, new Vector3(800, 800, 800), Quaternion.identity);
                break;
            case ScenarioObjectType.ELEVATOR:
                Debug.LogWarning("ELEVATOR NOT IMPLEMENTED");
                break;
            case ScenarioObjectType.TRAMPOLINE:
                Debug.LogWarning("TRAMPOLINE NOT IMPLEMENTED");
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
                    isInPosition = true;
                    objectSpawned.SpawnScenarioObject(transform);
                }
                else
                {
                    isInPosition = false;
                    objectSpawned.DeSpawnScenarioObject();

                }
            }
        }
        else
        {
            if (sprRender.enabled == true) sprRender.enabled = false;
        }
    }
}
