using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Serializable]
    public struct ScenarioObjectUI
    {
        public ScenarioObjectType type;
        public Sprite s;
        public Color c;
    }

    int points = 0;
    public TextMeshProUGUI pointsText, timeText;
    public List<ScenarioObjectUI> objectsUI;
    List<TextMeshProUGUI> SOUITexts = new List<TextMeshProUGUI>();
    public GameObject rightUI;
    public GameObject SOUIprefab;
    void Start()
    {
        EventsManager._instance.changeTime.AddListener(ChangeTime);
        EventsManager._instance.addPoints.AddListener(AddPoints);
        EventsManager._instance.scenarioObjectChanged.AddListener(ChangeSOUITexts);
        pointsText.text = "Points: "+ points.ToString();
        InitSOUI();

    }

    void InitSOUI()
    {
        //inicializamos el contador de objetos disponibles para la UI segun el tipo asociado
        for (int i = 0; i < objectsUI.Count; i++)
        {
            GameObject souiGO = Instantiate(SOUIprefab, rightUI.transform.position, Quaternion.identity);
            souiGO.transform.SetParent(rightUI.transform);
            souiGO.transform.localScale = Vector3.one;
            //accedemos a los textos que se cambiaran en ejecucion
            TextMeshProUGUI text = souiGO.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) SOUITexts.Add(text);
            Image spr = souiGO.GetComponentInChildren<Image>();
            if (spr != null)
            {
                spr.sprite = objectsUI[i].s;
                spr.color = objectsUI[i].c;
            }
        }

        ChangeSOUITexts();
    }
    void ChangeSOUITexts()
    {
        for(int i = 0; i < SOUITexts.Count; i++)
        {
            SOUITexts[i].text = "x"+ LevelManager._instance.GetInactiveSpawners(objectsUI[i].type).ToString();
        }
    }
    void ChangeTime(int time)
    {
        timeText.text = time != int.MaxValue ? "Time: "+ time.ToString() : "ENDLESS!!";
    }
    void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        pointsText.text = "Points: "+ points.ToString();
    }
}
