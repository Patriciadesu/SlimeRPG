using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem.UI;
using Unity.VisualScripting;
public class NPC : MonoBehaviour
{
    [SerializeField] protected string npcName;
    [SerializeField] protected string[] npcDialog;

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
        #region CreateScreenDim
        GameObject ScreenDim = new GameObject("ScreenDim");
        ScreenDim.transform.SetParent(DialogCanvas.transform);
        RawImage Panel = ScreenDim.AddComponent<RawImage>();
        Panel.rectTransform.sizeDelta = new Vector2(1080,607.5f);
        Panel.rectTransform.anchoredPosition = new Vector2(-0.000244139999f, 1.52590001e-05f);
        Panel.color = new Color(0f,0f,0f, 0.6f);

        #endregion
        #region CreateEventSystem
        GameObject EventSystemZ = new GameObject("EventSystem");
        EventSystemZ.AddComponent<EventSystem>();
        EventSystemZ.AddComponent<InputSystemUIInputModule>();
        #endregion
        #region CreateDialogText
        GameObject DialogText = new GameObject("DialogText");
        DialogText.transform.SetParent(DialogCanvas.transform);//เข้า parent
        TMPro.TextMeshProUGUI text = DialogText.AddComponent<TMPro.TextMeshProUGUI>();
        text.font = Resources.Load<TMPro.TMP_FontAsset>("Fonts & Materials/Arial SDF"); 
        text.fontSize = 25;
        text.alignment = TMPro.TextAlignmentOptions.TopLeft;

        // Adjust the RectTransform of the Text
        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(900, 200);
        textRect.anchoredPosition = new Vector2(7, -166);
        #endregion
        #region DialogSystem
        int dialogNumber = 1;
        if (dialogNumber < npcDialog.Length + 1)
        {
            text.text = npcDialog[dialogNumber-1];
        }
        else 
        { 
            Destroy(DialogCanvas);
            Destroy(EventSystemZ);
        }
        #endregion
        void OnButtonClick()
        {
            dialogNumber += 1;
        }
        #region CreateButtonForDialog
        GameObject buttonObject = new GameObject("MyButton");
        buttonObject.transform.SetParent(DialogCanvas.transform);
        Button button = buttonObject.AddComponent<Button>();
        button.AddComponent<CanvasRenderer>();
        RectTransform rectTransform = button.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1080, 600);
        rectTransform.localPosition = new Vector3(0, 3, 0);
        button.onClick.AddListener(OnButtonClick);
        //พังว้อยยยย ทำไมเพิ่ม Listener บ่ได้้้้
        #endregion
    }

}
