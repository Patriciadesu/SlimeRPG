/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

namespace Helios.GUI {
    public class WheelMenuController : MonoBehaviour {
        private const int FULL_CIRCLE = 360;

        [Header("References")]
        [SerializeField] private Image[] _imgRewards;
        [SerializeField] private GameObject _goRewardPopup;
        [SerializeField] private Image _imgRewardIcon;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _imgFocusLine;
        [SerializeField] private Button _btnTapToClose;
        [SerializeField] private Button TurnButton;
        [SerializeField] private GameObject Circle;           // Rotatable Object with rewards
        [SerializeField] private TextMeshProUGUI ticketAmount;

        [Header("Config params")]
        [SerializeField] private int _nbSpinTime = 5;
        [SerializeField] private int _nbAnimationTime = 3;
        [SerializeField] private List<AnimationCurve> animationCurves;

        private bool spinning;
        private float anglePerItem;
        private int itemNumber;
        private string username = APIController.username;

        public GameObject Button;
        public GameObject ButtonPressed;

        public TextMeshProUGUI showItemName;
        public GameObject exitButton;

        [System.Serializable]
        public class RandomItemResponse {
            public string itemName;
            public int limitedGacha;
        }

        private void Awake() {
            Debug.Log("Awake called");
            TurnButton.onClick.AddListener(TurnWheel);
        }

        private IEnumerator ShowReward(int index) {
            Debug.Log("ShowReward called with index: " + index);
            yield return new WaitForSeconds(0.4f);
            TurnButton.interactable = true;
            exitButton.SetActive(true);
            _animator.SetTrigger("Released");
            _imgFocusLine.gameObject.SetActive(true);
            _goRewardPopup.SetActive(true);
            _imgRewardIcon.sprite = _imgRewards[index].sprite;
        }

        void Start() {
            Debug.Log("Start called");
            spinning = false;
            anglePerItem = FULL_CIRCLE / _imgRewards.Length;
        }

        void OnEnable() {
            Debug.Log("OnEnable called");
            StartCoroutine(CheckTicketAmount());
        }

        private IEnumerator CheckTicketAmount() {
            Debug.Log("CheckTicketAmount called");
            string url = $"https://nj.dekhub.com/hamsterTown/getGachaTicketAmount?username={username}";
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                int ticketCount = int.Parse(request.downloadHandler.text);
                Debug.Log("Ticket count: " + ticketCount);
                ticketAmount.text = ticketCount.ToString();
                
                // Toggle button visibility based on ticket count
                bool hasTickets = ticketCount > 0;
                Button.SetActive(hasTickets);
                ButtonPressed.SetActive(!hasTickets);
                
                Debug.Log("TurnButton set to " + (hasTickets ? "interactable" : "non-interactable"));

            } else {
                Debug.LogError("Failed to check ticket amount: " + request.error);
                TurnButton.interactable = false;

                // Set button visibility when there's an error
                Button.SetActive(false);
                ButtonPressed.SetActive(true);
            }
        }

        private void TurnWheel() {
            Debug.Log("TurnWheel called");
            if (!spinning && TurnButton.interactable) { // Check if the wheel is not spinning and button is interactable
                // UI handle
                Debug.Log("Wheel is not spinning and button is interactable, proceeding");
                _animator.SetTrigger("Pressed");
                _btnTapToClose.interactable = false;
                _imgFocusLine.gameObject.SetActive(false);

                // Disable exitButton when the wheel starts spinning
                exitButton.SetActive(false);

                StartCoroutine(UseTicketAndSpinWheel());
            } else {
                Debug.Log("Wheel is already spinning or button is not interactable");
            }
        }

        private IEnumerator UseTicketAndSpinWheel() {
            Debug.Log("UseTicketAndSpinWheel called");

            // Disable the button before performing the operations
            TurnButton.interactable = false;

            // Use a ticket
            string deleteUrl = $"https://nj.dekhub.com/hamsterTown/deleteItem?username={username}&itemName=GachaTicket";
            UnityWebRequest deleteRequest = UnityWebRequest.Get(deleteUrl);
            yield return deleteRequest.SendWebRequest();

            if (deleteRequest.result != UnityWebRequest.Result.Success) {
                Debug.LogError("Failed to delete ticket: " + deleteRequest.error);
                // Re-enable the button if there was an error
                TurnButton.interactable = true;
                yield break;
            }
            Debug.Log("Ticket deleted successfully");

            // Get random item
            string randomItemUrl = $"https://nj.dekhub.com/hamsterTown/RandomItem?username={username}";
            UnityWebRequest randomItemRequest = UnityWebRequest.Get(randomItemUrl);
            yield return randomItemRequest.SendWebRequest();

            if (randomItemRequest.result == UnityWebRequest.Result.Success) {
                Debug.Log("Random item retrieved successfully");
                RandomItemResponse randomItem = JsonUtility.FromJson<RandomItemResponse>(randomItemRequest.downloadHandler.text);
                Debug.Log("Random item: " + randomItem.itemName);
                itemNumber = GetItemIndex(randomItem.itemName);
                if (itemNumber >= 0) {
                    float maxAngle = _nbSpinTime * FULL_CIRCLE + (itemNumber * anglePerItem);
                    StartCoroutine(SpinTheWheel(_nbAnimationTime, maxAngle));
                    StartCoroutine(AddItem(randomItem.itemName));
                    showItemName.text = randomItem.itemName;
                } else {
                    Debug.LogError("Invalid item index for item: " + randomItem.itemName);
                    TurnButton.interactable = true;
                }
            } else {
                Debug.LogError("Failed to get random item: " + randomItemRequest.error);
                TurnButton.interactable = true;
            }
        }

        private int GetItemIndex(string itemName) {
            Debug.Log("GetItemIndex called with itemName: " + itemName);
            switch (itemName) {
                case "Big Enter": return 0;
                case "Poo": return 1;
                case "Hamster Coin 30": return 2;
                case "Cute LED lamp": return 3;
                case "Dry Leaves": return 4;
                case "Hamster Coin 10": return 5;
                case "Rimuru Pillow": return 6;
                case "Salt": return 7;
                default: 
                    Debug.LogError("Invalid item name: " + itemName);
                    return -1;
            }
        }

        private IEnumerator AddItem(string itemName) {
            Debug.Log("AddItem called with itemName: " + itemName);
            string addItemUrl = $"https://nj.dekhub.com/hamsterTown/addItem?username={username}&itemName={itemName}";
            UnityWebRequest addItemRequest = UnityWebRequest.Get(addItemUrl);
            yield return addItemRequest.SendWebRequest();

            if (addItemRequest.result == UnityWebRequest.Result.Success) {
                Debug.Log("Item added successfully: " + itemName);
            } else {
                Debug.LogError($"Failed to add item {itemName}: " + addItemRequest.error);
            }

            // Check ticket amount again
            StartCoroutine(CheckTicketAmount());
        }

        IEnumerator SpinTheWheel(float time, float maxAngle) {
            Debug.Log("SpinTheWheel called with time: " + time + " and maxAngle: " + maxAngle);
            spinning = true;
            TurnButton.interactable = false;

            float timer = 0.0f;
            float startAngle = Circle.transform.eulerAngles.z;
            maxAngle -= startAngle;

            int animationCurveNumber = UnityEngine.Random.Range(0, animationCurves.Count); // Explicitly use UnityEngine.Random
            Debug.Log("Animation Curve No. : " + animationCurveNumber);

            while (timer < time) {
                // To calculate rotation
                float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
                Circle.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
                timer += Time.deltaTime;
                yield return null;
            }

            Circle.transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
            spinning = false;
            _btnTapToClose.interactable = true;
            StartCoroutine(ShowReward(itemNumber));
        }
    }
}
*/