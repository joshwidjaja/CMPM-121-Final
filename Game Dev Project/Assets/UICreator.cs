using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UICreator : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas myCanvas;
    GameObject canvasObj;
    GameObject up;

    UnityEngine.UI.Button upButton;
    RectTransform upTransform;
    void Start()
    {
        canvasObj = new GameObject();
        canvasObj.name = "MyCanvas";
        canvasObj.AddComponent<Canvas>();
        myCanvas = canvasObj.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();
        up = new GameObject();
        up.name = "UP";
        up.AddComponent<UnityEngine.UI.Button>();
        up.AddComponent<RectTransform>();
        upButton = up.GetComponent<UnityEngine.UI.Button>();
        up.AddComponent<UnityEngine.UI.Image>();
        upTransform = up.GetComponent<RectTransform>();
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
        textComponent.color = Color.black;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
