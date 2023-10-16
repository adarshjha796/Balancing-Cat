using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class LeaderboardHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject rowID;

    [SerializeField]
    private GameObject rowSCORE;

    [SerializeField]
    private GameObject rowRANK;

    [SerializeField]
    public GameObject playerSelfID;

    public Transform rowIDParent;
    public Transform rowSCOREParent;
    public Transform rowRANKParent;
    public Transform playerSelfIDParent;

    public int rank;

    [SerializeField]
    private string[] arrangedScore;
    private string[] arrangedScoreInString;
    private float[] arrangedScoreInFloat;
    [SerializeField]
    private string[] arrangedWalletAddress;

    [SerializeField]
    private List<string> arrangedWalletAddressList;

    [System.Serializable]
    public class Data
    {
        public string walletAddress;
        public string score;
    }

    [System.Serializable]
    public class DataList
    {
        public Data[] data;
    }

    public DataList myDataList = new DataList();

    /// <summary>
    /// This function will gathered all the leaderboard data in a variable fron=m the JSon format and conavert it.
    /// call the arrangedLeaderboard function for making the score in decending order, Instaciating All the Leaderboard Coloumn, Remove the duplicates.
    /// </summary>
    public void canShowLeaderboard()
    {
        myDataList = JsonUtility.FromJson<DataList>(ApiCalls.instance.userDataJson);

        arrangedScore = new string[myDataList.data.Length];
        arrangedScoreInFloat = new float[myDataList.data.Length];
        arrangedWalletAddress = new string[myDataList.data.Length];
        arrangedScoreInString = new string[myDataList.data.Length];
        arrangeLeaderBoard();
    }

    /// <summary>
    /// This function will Sort the Score and also remove the duplicates wallet Id from the leaderboard.
    /// </summary>
    public void arrangeLeaderBoard()
    {
        // Now convert above string to float for sorting to work to happen.
        for (int i = 0; i < myDataList.data.Length; i++)
        {
            arrangedScoreInFloat[i] = float.Parse(myDataList.data[i].score);
        }

        // Now we have score array filled in desending order.
        Array.Sort(arrangedScoreInFloat);
        Array.Reverse(arrangedScoreInFloat);

        // Coverting all floats to string to trim in next step.
        for (int i = 0; i < arrangedScoreInFloat.Length; i++)
        {
            arrangedScoreInString[i] = arrangedScoreInFloat[i].ToString();
        }

        // Trim the string for only 1 decimal point
        for (int i = 0; i < arrangedScoreInString.Length; i++)
        {
            string[] bits = arrangedScoreInString[i].Split('.');
            arrangedScore[i] = bits[0] + "." + bits[1].Substring(0, 1);
        }

        for (int i = 0; i < myDataList.data.Length; i++)
        {
            for (int j = 0; j < myDataList.data.Length; j++)
            {
                string[] bits = myDataList.data[j].score.Split('.');
                myDataList.data[j].score = bits[0] + "." + bits[1].Substring(0, 1);
                if (arrangedScore[i] == myDataList.data[j].score)
                {
                    if (!arrangedWalletAddressList.Contains(myDataList.data[j].walletAddress))
                    {
                        arrangedWalletAddress[i] = myDataList.data[j].walletAddress;
                        arrangedWalletAddressList.Add(arrangedWalletAddress[i]);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        //This loop will instanciate all the Wallet ID, Score and Rank.
        for (int i = 0; i < myDataList.data.Length; i++)
        {
            //Instanciating the rowID prefab
            GameObject objID = Instantiate(rowID, rowIDParent);
            TMP_Text textsID = objID.GetComponentInChildren<TMP_Text>();
            textsID.text = WebLogin.instance.GetNameString(arrangedWalletAddress[i]); //arrangedWalletAddress[i].Substring(0, 5) + "..." + arrangedWalletAddress[i].Substring(38, arrangedWalletAddress[i].Length - 38);

            //Instanciating the rowScore Prefab
            GameObject obj = Instantiate(rowSCORE, rowSCOREParent);
            TMP_Text texts = obj.GetComponentInChildren<TMP_Text>();
            texts.text = arrangedScore[i].ToString();

            //Instanciating the rowRank Prefab
            rank = i + 1;
            GameObject objRANK = Instantiate(rowRANK, rowRANKParent);
            TMP_Text textsRANK = objRANK.GetComponentInChildren<TMP_Text>();

            textsRANK.text = rank.ToString();

            switch (rank)
            {
                default:
                    textsRANK.text = "#" + rank;
                    break;

                case 1:
                    textsRANK.text = "#1";
                    break;

                case 2:
                    textsRANK.text = "#2";
                    break;

                case 3:
                    textsRANK.text = "#3";
                    break;
            }
        }
    }

    /// <summary>
    /// This script is for closing the leaderboard and switch the scene of Menu.
    /// It will be attach to the close button.
    /// </summary>
    public void CloseLeaderBoard()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
