using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UICreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject canvasObj;
    Canvas myCanvas;   
    void Start()
    {
        canvasObj = new GameObject();
        canvasObj.name = "MyCanvas";
        canvasObj.AddComponent<Canvas>();
        myCanvas = canvasObj.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        /*GameObject up = new GameObject();
        up.name = "UP";
        up.AddComponent<UnityEngine.UI.Button>();
        up.AddComponent<RectTransform>();
        UnityEngine.UI.Button upButton = up.GetComponent<UnityEngine.UI.Button>();
        up.AddComponent<UnityEngine.UI.Image>();
        RectTransform upTransform = up.GetComponent<RectTransform>();
        UnityEngine.UI.Button.ButtonClickedEvent myEvent = new UnityEngine.UI.Button.ButtonClickedEvent();
        myEvent.AddListener(GameObject.Find("Player").GetComponent<PlayerController>().MoveUp);
        upButton.onClick = myEvent;
        //Debug.Log(GameObject.Find("Canvas").transform.Find("Up").transform.Find("Text (TMP)").GetComponents());
        up.transform.parent = myCanvas.transform;
        upTransform.anchoredPosition = new Vector3(-283, -105);
        upTransform.sizeDelta = new Vector2(30, 30);
        GameObject theText = new GameObject();
        theText.transform.parent = up.transform;
        theText.AddComponent<TextMeshProUGUI>();
        TextMeshProUGUI textComponent = theText.GetComponent<TextMeshProUGUI>();
        RectTransform textTransform = theText.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(0, 0);
        textTransform.sizeDelta = new Vector2(30, 30);
        textComponent.fontSize = 24;
        textComponent.SetText("↑");
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.color = Color.black;*/
        makeButton("UP", -283, -105, 30, 30, "↑", GameObject.Find("Player").GetComponent<PlayerController>().MoveUp);
        makeButton("LEFT", -315, -139, 30, 30, "←", GameObject.Find("Player").GetComponent<PlayerController>().MoveLeft);
        makeButton("DOWN", -283, -170, 30, 30, "↓", GameObject.Find("Player").GetComponent<PlayerController>().MoveDown);
        makeButton("RIGHT", -251, -139, 30, 30, "→", GameObject.Find("Player").GetComponent<PlayerController>().MoveRight);
        makeButton("Plant", -283, -05, 30, 30, "P", GameObject.Find("Player").GetComponent<PlayerController>().Plant);
        makeButton("Harvest", -283, 95, 30, 30, "H", GameObject.Find("Player").GetComponent<PlayerController>().Harvest);
        makeButton("Undo", 483, -05, 30, 30, "U", GameObject.Find("Player").GetComponent<PlayerController>().Undo);
        makeButton("Redo", 483, 95, 30, 30, "R", GameObject.Find("Player").GetComponent<PlayerController>().Redo);
    }
    GameObject makeButton(string name, int pos1, int pos2, int size1, int size2, string text, UnityEngine.Events.UnityAction theFunction){
        GameObject up = new GameObject();
        up.name = name;
        up.AddComponent<UnityEngine.UI.Button>();
        up.AddComponent<RectTransform>();
        UnityEngine.UI.Button upButton = up.GetComponent<UnityEngine.UI.Button>();
        up.AddComponent<UnityEngine.UI.Image>();
        RectTransform upTransform = up.GetComponent<RectTransform>();
        UnityEngine.UI.Button.ButtonClickedEvent myEvent = new UnityEngine.UI.Button.ButtonClickedEvent();
        //myEvent.AddListener(GameObject.Find("Player").GetComponent<PlayerController>().MoveUp);
        myEvent.AddListener(theFunction);
        upButton.onClick = myEvent;
        //Debug.Log(GameObject.Find("Canvas").transform.Find("Up").transform.Find("Text (TMP)").GetComponents());
        up.transform.parent = myCanvas.transform;
        upTransform.anchoredPosition = new Vector3(pos1, pos2);
        upTransform.sizeDelta = new Vector2(size1, size2);
        GameObject theText = new GameObject();
        theText.transform.parent = up.transform;
        theText.AddComponent<TextMeshProUGUI>();
        TextMeshProUGUI textComponent = theText.GetComponent<TextMeshProUGUI>();
        RectTransform textTransform = theText.GetComponent<RectTransform>();
        textTransform.anchoredPosition = new Vector2(0, 0);
        textTransform.sizeDelta = new Vector2(30, 30);
        textComponent.fontSize = 24;
        textComponent.SetText(text);
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.color = Color.black;
        return up;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
