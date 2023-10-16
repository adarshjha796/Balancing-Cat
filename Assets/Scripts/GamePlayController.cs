using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public Cat cat;

    [Header("int")]
    public int nextCameraMoveThreshold = 1;
    private int numberOfBoxesOnFloor = 0;
    public int numberOfBoxesOnCatsHead = 0;
    public int totalNumberOfItemsInStackBeforeGameOver = 0;
    [SerializeField]
    public int maxStackHeightBeforeCatShift = 5;

    public bool isGameOver;

    [SerializeField]
    private GameObject gameoverScreen;

    [SerializeField]
    private GameObject itemContainer;

    [HideInInspector]
    public ObjectScript currentObject;

    public LeaderboardHandler leaderboardHandler;


    public int BoxesOnFloor
    {
        get
        {
            return numberOfBoxesOnFloor;
        }
        set
        {
            numberOfBoxesOnFloor += value;
        }
    }

    public int BoxesOnHead
    {
        get
        {
            return numberOfBoxesOnCatsHead;
        }
        set
        {
            numberOfBoxesOnCatsHead += value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private IEnumerator Start()
    {
        cat = FindObjectOfType<Cat>();
        leaderboardHandler = FindObjectOfType<LeaderboardHandler>();

        yield return new WaitForSeconds(0.1f);

        isGameOver = false;
    }

    /// <summary>
    /// This Method will load the same scene if the collectibles touches the ground.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnGameOver()
    {
        ApiCalls.instance.SaveUserDataFunction("walletAddress", WebLogin.instance.account, "score", cat.Score.ToString());
        StartCoroutine(IOnGameOver());
    }
    private IEnumerator IOnGameOver()
    {
        itemContainer.SetActive(true);
        totalNumberOfItemsInStackBeforeGameOver = numberOfBoxesOnCatsHead;

        yield return new WaitForSeconds(2f);

        ApiCalls.instance.GetAllUserDataFunction();

        //Instanciating the rowID prefab
        GameObject objID = Instantiate(leaderboardHandler.playerSelfID, leaderboardHandler.playerSelfIDParent);
        TMP_Text textsID = objID.GetComponentInChildren<TMP_Text>();
        textsID.text = WebLogin.instance.GetNameString(WebLogin.instance.account);

        yield return new WaitForSeconds(1f);

        leaderboardHandler.canShowLeaderboard();

        yield return new WaitForSeconds(1f);

        gameoverScreen.SetActive(true);
    }

    /// <summary>
    /// This Method will save player's score in the database.
    /// </summary>
    //private void SetPlayerStats()
    //{
    //    // Set stats for height 
    //    float height = ObjectScript.distance;
    //    string heightStr = height.ToString("f1");

    //    char[] heightArray = heightStr.ToCharArray();

    //    string pre = "";

    //    for (int i = 0; i < heightArray.Length; i++)
    //    {
    //        if (heightArray[i] != '.')
    //        {
    //            pre += heightArray[i];
    //        }
    //    }

    //    //AccountController.controller.SetStat("Height", int.Parse(pre));

    //    int points = totalNumberOfItemsInStackBeforeGameOver * int.Parse(pre);

    //    //AccountController.controller.SetStat("Score",points);

    //    //AccountController.controller.SavePlayerData();
    //}
}
