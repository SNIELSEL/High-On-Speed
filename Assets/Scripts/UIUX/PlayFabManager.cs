using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    [Header("LeaderBoardNameStuff")]

    [Header("ScoreBoard")]
    public GameObject rowPrefab;
    public Transform[] leaderboardlistTransforms;
    public string[] leaderboardNamesList;
    public int currentSelectedLeaderboard;
    public int currentLeaderboardToSendDataTo;

    [Header("eventTriggersOrChecks")]
    public string playername;

    [Header("scripts")]
    public SaveAndLoad saveAndLoad;

    // Start is called before the first frame update
    void Start()
    {
        Login();

        saveAndLoad.LoadData();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSucces, OnError);
    }

    void OnLoginSucces(LoginResult result)
    {
        playername = null;

        if(result.InfoResultPayload.PlayerProfile != null)
        {
            playername = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (playername == null || playername == SystemInfo.deviceUniqueIdentifier)
        {
            saveAndLoad.SaveData();
        }
        Debug.Log("Logged in Succesfully as " + SystemInfo.deviceUniqueIdentifier);
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Couldnt Log In");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderBoard(int time)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = leaderboardNamesList[currentLeaderboardToSendDataTo],
                    Value = time
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }

    void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent to " + leaderboardNamesList[currentLeaderboardToSendDataTo]);
    }

    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardNamesList[currentLeaderboardToSendDataTo],
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }

    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        foreach(Transform item in leaderboardlistTransforms[currentSelectedLeaderboard])
        {
            Destroy(item.gameObject);
        }

        foreach(var item in result.Leaderboard)
        {
            GameObject newRow = Instantiate(rowPrefab, leaderboardlistTransforms[currentSelectedLeaderboard]);
            TextMeshProUGUI[] texts = newRow.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = Oculus.Platform.Users.GetLoggedInUser().ToString()
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        saveAndLoad.SaveData();
    }

    public void SetSelectedLeaderboardInt(int numberToSetIntsTo)
    {
        currentSelectedLeaderboard = numberToSetIntsTo;
    }
    
}
