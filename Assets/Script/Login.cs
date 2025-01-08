using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json; // For JSON parsing
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Login : MonoBehaviour
{

    public GameObject videoScreen;
    string baseURL = "https://nj.dekhub.com/hamsterTown";
    string tempTokens;
    bool waitForData = false;
    public Button signOut;
    public Button startGame;
    string userdata;
    bool profileSet = false;

    public static string discordUsername;
    public static string id;
    public static string avatar;
    public static string username;
    bool isLogin = true;
    public string sceneChange = "MainScene";
    [Header("For Dev")]
    public bool becomePatrickMode = false;
    void Start()
    {
        QuickSignIn();
        signOut.onClick.AddListener(() => SignOut());
    }
    public void QuickSignIn()
    {
        if (PlayerPrefs.HasKey("DiscordUsername"))
        {
            discordUsername = PlayerPrefs.GetString("DiscordUsername");
            id = PlayerPrefs.GetString("ID");
            avatar = PlayerPrefs.GetString("Avatar");
            username = PlayerPrefs.GetString("Username");
            signOut.gameObject.SetActive(true);

            startGame.onClick.AddListener(() => StartGame());
            FadeOut();
        }
        else
        {
            signOut.gameObject.SetActive(false);

            startGame.onClick.AddListener(() => SignIn());
            StartCoroutine(StartVideo());
        }
    }

    public void FadeOut()
    {
        videoScreen.GetComponent<RawImage>().texture = null;
        videoScreen.GetComponent<Animator>().SetTrigger("FadeOut");
    }
    public IEnumerator StartVideo()
    {
        VideoPlayer videoPlayer = videoScreen.GetComponent<VideoPlayer>();
        videoScreen.GetComponent<RawImage>().texture = videoPlayer.targetTexture;
        videoPlayer.Play();
        videoScreen.GetComponent<Animator>().SetTrigger("FadeBlack");
        yield return new WaitUntil(()=>!videoPlayer.isPlaying);
        videoScreen.GetComponent<Animator>().SetTrigger("FadeOut");
    }

    public void StartGame()
    {
        if (becomePatrickMode) id = "547402456363958273";
        DatabaseManager.Instance.GetDataObejct<sPlayer>(API.signIn(id),OnGetPlayerData);
    }
    public void OnGetPlayerData(sPlayer data)
    {
        DatabaseManager.Instance.playerData = data;
        //StartCoroutine(GetDataCoroutine());
        //SceneManager.LoadScene(sceneChange);
        StartCoroutine(LoadSceneAsync(sceneChange));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading progress: " + asyncLoad.progress);
            yield return null;
        }
    }




    public void SignOut()
    {
        isLogin = false;
        waitForData = false;
        profileSet = false;

        PlayerPrefs.DeleteAll();

        discordUsername = null;
        id = null;
        avatar = null;
        username = null;

        startGame.onClick.RemoveAllListeners();
        startGame.onClick.AddListener(()=>SignIn());
        signOut.gameObject.SetActive(false);

        //Application.Quit();
    }
    public void SignIn()
    {
        StartCoroutine(StartSignIn());
    }

    void Update()
    {
        if (waitForData)
        {
            GetUser(tempTokens);
        }

        if (isLogin && PlayerPrefs.HasKey("DiscordUsername"))
        {
            signOut.gameObject.SetActive(true);
        }
        else
        {
            signOut.gameObject.SetActive(false);
        }
    }

    public void GetUser(string tempToken)
    {
        StartCoroutine(GetUserCoroutine(tempToken));
    }

    IEnumerator GetUserCoroutine(string tempToken)
    {
        string url = $"{baseURL}/api/get-user?tempToken={tempToken}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                userdata = webRequest.downloadHandler.text;

                try
                {
                    UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(userdata);
                    if (userProfile != null && !profileSet)
                    {
                        profileSet = true;
                        discordUsername = userProfile.data.username;
                        id = userProfile.discord_id;
                        username = userProfile.username;

                        avatar = $"https://cdn.discordapp.com/avatars/{userProfile.discord_id}/{userProfile.data.avatar}";

                        PlayerPrefs.SetString("DiscordUsername", discordUsername);
                        PlayerPrefs.SetString("ID", id);
                        PlayerPrefs.SetString("Avatar", avatar);
                        PlayerPrefs.SetString("Username", username);

                        Debug.Log("avatar url: " + avatar);

                        StartGame();

                        isLogin = true; // Set isLogin to true once the user profile is set
                    }
                }
                catch (JsonException ex)
                {
                    Debug.LogError("Error parsing user data: " + ex.Message);
                }

                waitForData = false; // Stop waiting for data after the process is completed
            }
        }
    }

    IEnumerator StartSignIn()
    {
        string url = $"{baseURL}/api/create-token";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                try
                {
                    // Deserialize into a strongly-typed class
                    LoginResponse jsonResponse = JsonConvert.DeserializeObject<LoginResponse>(webRequest.downloadHandler.text);

                    tempTokens = jsonResponse.tempToken;

                    Debug.Log("Received tempToken: " + tempTokens);
                    string loginURL = $"{baseURL}/discord/login?tempToken={tempTokens}";
                    Application.OpenURL(loginURL);
                    waitForData = true;
                }
                catch (JsonException ex)
                {
                    Debug.LogError("Error parsing login response: " + ex.Message);
                }
            }
        }
    }

    public void GetUserByDiscordId(string discordId)
    {
        //StartCoroutine(GetUserByDiscordIdCoroutine(discordId));
    }

    //private IEnumerator GetUserByDiscordIdCoroutine(string discordId)
    //{
    //    string url = DatabaseManager.USER_PATH + "/" + discordId; // Construct the full API endpoint URL

    //    using (UnityWebRequest request = UnityWebRequest.Get(url))
    //    {
    //        // Send the request and wait for the response
    //        yield return request.SendWebRequest();

    //        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            // Handle error
    //            Debug.LogError($"Error: {request.error}");
    //        }
    //        else
    //        {
    //            // Parse the response JSON
    //            string jsonResponse = request.downloadHandler.text;
    //            Debug.Log($"Response: {jsonResponse}");

    //            // Optionally, parse into a C# object
    //            PlayerData user = JsonUtility.FromJson<PlayerData>(jsonResponse);

    //        }
    //    }
    //}




    public class UserProfile
    {
        public string discord_id { get; set; }
        public string username { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public string username { get; set; }
        public string avatar { get; set; }
    }
    public class LoginResponse
    {
        public string tempToken { get; set; }
    }

}
