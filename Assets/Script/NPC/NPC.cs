using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class NPC : MonoBehaviour
{
    protected string npcName;
    protected string[] npcDialog;

    private void Start()
    {
        PlayDialog();
    }
    public void PlayDialog()
    {
        #region CreateCanvas
        GameObject DialogCanvas = new GameObject("DialogCanvas");
        Canvas canvas = DialogCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasScaler = DialogCanvas.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1080, 1920);
        DialogCanvas.AddComponent<GraphicRaycaster>();
        GameObject DialogText = new GameObject("Text");
        DialogText.transform.SetParent(DialogCanvas.transform);
        UnityEngine.UI.Text text = DialogText.AddComponent<UnityEngine.UI.Text>();
        //TMP_Text text = DialogText.AddComponent<UnityEngine.UI.Text>();
        text.text = "Hello, Dynamic Canvas!";
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 30;
        text.alignment = TextAnchor.MiddleCenter;

        // Adjust the RectTransform of the Text
        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(600, 200);
        textRect.anchoredPosition = Vector2.zero; // Center the text
        #endregion
    }
}
