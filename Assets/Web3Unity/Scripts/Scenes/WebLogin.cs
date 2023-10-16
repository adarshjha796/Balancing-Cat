using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Numerics;
using TMPro;

public enum LOGIN_MODE
{
    LM_GUEST,
    LM_WALLET
}

#if UNITY_WEBGL || UNITY_EDITOR
public class WebLogin : MonoBehaviour
{
    public static WebLogin instance;
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    //private int expirationTime;
    public string account;
    public GameObject errorMessage;
    //public AudioClip tapSfx;
    public LOGIN_MODE login_mode;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void OnLogin()
    {
        Web3Connect();
        OnConnected();
    }

    async private void OnConnected()
    {
        login_mode = LOGIN_MODE.LM_WALLET;

        account = ConnectAccount();
        while (account == "")
        {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };

        // save account for next scene
        //PlayerPrefs.SetString("Account", account);
        // reset login message
        SetConnectAccount("");

        // Update the wallet address to the database.
        ApiCalls.instance.SendWalltetAddressFunction("walletAddress", account);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //// load next scene
        //string chain = "Polygon";
        //string network = "Mainnet"; // mainnet ropsten kovan rinkeby goerli
        //string contract = "0x2d0D9b4075E231bFf33141d69DF49FFcF3be7642"; /*"0xf1c21f90e759183cb51152006eee31dd9455cc04"; */
        //string tokenId = "7771625817211";

        //BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
        ////print(balanceOf);
        //if (balanceOf > 0 && account != null)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    //ApiCalls.instance.GetAllUserDataFunction();
        //}
        //else
        //{
        //    //errorMessage.SetActive(true);
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    //ApiCalls.instance.GetAllUserDataFunction();
        //}
    }

    public void OnSkip()
    {
        // move to next scene

        login_mode = LOGIN_MODE.LM_GUEST;

        account = "";

#if UNITY_EDITOR
        login_mode = LOGIN_MODE.LM_WALLET;
        account = "editor_testAccount";
#endif

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Convert the Wallet Id format as first 5 string and add ... then 4 last string if player uses the metamask account.
    /// Else condition will print ID as Guest...play for Individual Player.
    /// </summary>
    public string GetNameString(string str)
    {
        string acc = "";
        if (str.Length > 10)
        {
            acc += str.Substring(0, 4) + "..." + str.Substring(str.Length - 4, 4);
        }
        else
        {
            return "Guest...Play";
        }

        return acc;
    }
}
#endif
