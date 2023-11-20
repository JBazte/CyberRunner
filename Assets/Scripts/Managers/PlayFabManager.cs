using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UIElements;

public class PlayFabManager : TemporalSingleton<GameManager> {
    [SerializeField]
    private GameObject usernameWindow;
    [SerializeField]
    private TMP_InputField inputUsername;
    [SerializeField]
    private GameObject leaderboardPanel;
    [SerializeField]
    private GameObject listingPrefab;
    [SerializeField]
    private Transform listingContainer;
    [SerializeField]
    private UIDocument uiObject;
    private string userPlayFabId;

    void Start() {
        LoginPlayFab();
    }

    private void LoginPlayFab() {
#if UNITY_ANDROID
        var requestAndroid = new LoginWithAndroidDeviceIDRequest {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams() {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginSuccess, OnError);
#endif
#if UNITY_IOS
        var requestIOS = new LoginWithIOSDeviceIDRequest {
            DeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams() {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginSuccess, OnError);
#endif
    }

    #region Leaderboard
    public void GetLeaderboardEntries() {
        var request = new GetLeaderboardRequest {
            StatisticName = "Highscore-dev",
            StartPosition = 0,
            MaxResultsCount = 25
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardEntriesSuccess, OnError);
    }

    public void GetLeaderboardEntriesAroundPlayer() {
        var request = new GetLeaderboardAroundPlayerRequest {
            StatisticName = "Highscore-dev",
            MaxResultsCount = 25
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardEntriesAroundPlayerSuccess, OnError);
    }

    public void SetLeaderboardEntry(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                   StatisticName = "Highscore-dev", Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnError);
    }

    #endregion

    #region PlayerData
    private void GetPlayerData() {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnPlayerDataGetSuccess, OnError);
    }

    public void SetPlayerData() {
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {"CyberbikePowerUp", PlayerPrefs.GetInt("MotorbikeCharges").ToString() },
                {"HyperspeedPowerUp", PlayerPrefs.GetInt("HyperspeedCharges").ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnPlayerDataSetSuccess, OnError);
    }
    #endregion

    #region Interactable UI

    public void CloseLeaderboardPanel() {
        leaderboardPanel.SetActive(false);
        for (int i = listingContainer.childCount - 1; i >= 0; i--) {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
    }

    public void SetUserTitleDisplayName() {
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = inputUsername.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUserTitleDisplayNameSetSuccess, OnError);
    }

    #endregion

    #region HTTP Handling
    void OnLoginSuccess(LoginResult result) {
        Debug.Log("Succesful login/account create!");
        userPlayFabId = result.PlayFabId;
        string username = null;
        if (result.InfoResultPayload.PlayerProfile != null) {
            username = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (username == null) {
            usernameWindow.SetActive(true);
        } else {
            uiObject.enabled = true;
        }

        GetPlayerData();
    }

    void OnLeaderboardEntriesSuccess(GetLeaderboardResult result) {
        leaderboardPanel.SetActive(true);
        foreach (var item in result.Leaderboard) {
            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
            LeaderboardList leaderboardList = tempListing.GetComponent<LeaderboardList>();
            leaderboardList.rankText.text = (item.Position + 1) + ".  ";
            leaderboardList.usernameText.text = item.DisplayName;
            leaderboardList.scoreText.text = item.StatValue.ToString();
            Debug.Log("Rank: " + item.Position + " | ID: " + item.PlayFabId + " | Score: " + item.StatValue);
        }
    }

    void OnLeaderboardEntriesAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result) {
        leaderboardPanel.SetActive(true);
        foreach (var item in result.Leaderboard) {
            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
            LeaderboardList leaderboardList = tempListing.GetComponent<LeaderboardList>();
            leaderboardList.rankText.text = (item.Position + 1) + ".  ";
            leaderboardList.usernameText.text = item.DisplayName;
            leaderboardList.scoreText.text = item.StatValue.ToString();

            if (userPlayFabId == item.PlayFabId) {
                leaderboardList.rankText.color = Color.red;
                leaderboardList.usernameText.color = Color.red;
                leaderboardList.scoreText.color = Color.red;
            }

            Debug.Log("Rank: " + item.Position + " | ID: " + item.PlayFabId + " | Score: " + item.StatValue);
        }
    }

    void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result) {
        Debug.Log("Succesful Leaderboard Update!");
        //GetLeaderboardEntriesAroundPlayer();
    }

    void OnPlayerDataGetSuccess(GetUserDataResult result) {
        if (result.Data != null && result.Data.ContainsKey("CyberbikePowerUp") && result.Data.ContainsKey("HyperspeedPowerUp")) {
            Debug.Log("Succesful PlayerData Get Request!");
            PlayerPrefs.SetInt("MotorbikeCharges", int.Parse(result.Data["CyberbikePowerUp"].Value));
            PlayerPrefs.SetInt("HyperspeedCharges", int.Parse(result.Data["HyperspeedPowerUp"].Value));
        } else {
            Debug.Log("Error in PlayerData Request!");
        }
    }

    void OnPlayerDataSetSuccess(UpdateUserDataResult result) {
        Debug.Log("Succesful PlayerData Update!");
    }

    void OnUserTitleDisplayNameSetSuccess(UpdateUserTitleDisplayNameResult result) {
        Debug.Log("Succesful Username Update!");
        usernameWindow.SetActive(false);
        uiObject.enabled = true;
    }

    void OnError(PlayFabError error) {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion
}
