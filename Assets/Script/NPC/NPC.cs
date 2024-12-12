using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem.UI;
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
        #endregion
        #region CreateEventSystem
        GameObject EventSystemZ = new GameObject("EventSystem");
        EventSystemZ.AddComponent<EventSystem>();
        EventSystemZ.AddComponent<InputSystemUIInputModule>();
        #endregion
        #region CreateDialogText
        GameObject DialogText = new GameObject("DialogText");
        DialogText.transform.SetParent(DialogCanvas.transform);

        // Add TextMeshProUGUI component
        TMPro.TextMeshProUGUI text = DialogText.AddComponent<TMPro.TextMeshProUGUI>();
        text.text = "Hello, Dynamic Canvas!";
        text.font = Resources.Load<TMPro.TMP_FontAsset>("Fonts & Materials/Arial SDF"); // Ensure you have a TMP font asset
        text.fontSize = 30;
        text.alignment = TMPro.TextAlignmentOptions.Center;

        // Adjust the RectTransform of the Text
        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(600, 200);
        textRect.anchoredPosition = Vector2.zero; // Center the text
        #endregion
        #region DialogSystem
        int dialogNumber = 1;
        if (dialogNumber < npcDialog.Length + 1)
        {
            text.text = npcDialog[dialogNumber-1];
        }
        #endregion
    }
}
