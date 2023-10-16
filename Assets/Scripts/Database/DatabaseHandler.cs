//using System;
//using System.Collections;
//using System.Collections.Generic;
//using DatabaseAPI.Account;
//using TMPro;
//using UnityEngine;

//public class DatabaseHandler : MonoBehaviour
//{
//    private string walletIDTxt;
//    private string emailTxt;
//    private string passwordTxt;

//    private string walletID; 

//    /// <summary>
//    /// This Method will store the wallet id from metamask
//    /// and generate a static email and password.
//    /// </summary>
//    private void Start()
//    {      
//        // Get the wallet id from prefs
//        walletID = PlayerPrefs.GetString("Account");
//        walletIDTxt = walletID.Substring(walletID.Length - 4, 4);

//        emailTxt = walletIDTxt + "@gmail.com";
//        passwordTxt = "12345678";

//        InitializeDatabase(walletIDTxt, emailTxt, passwordTxt);
//    }

//    /// <summary>
//    /// This Method will let the player either register or login to the database.
//    /// </summary>
//    private void InitializeDatabase(string username, string email, string password)
//    {
//        // Fill out credentials
//        AccountController.controller.GET_USER_USERNAME(username);
//        AccountController.controller.GET_USER_EMAIL(email);
//        AccountController.controller.GET_USER_PASSWORD(password);

//        AccountController.controller.LOGIN_ACTION();

//        // Check if user is trying to login by accessing email from prefs
//        //if (PlayerPrefs.HasKey("PLAYFAB_USER_EMAIL"))
//        //{
//        //    AccountController.controller.GET_USER_EMAIL(PlayerPrefs.GetString("PLAYFAB_USER_EMAIL"));
//        //    AccountController.controller.GET_USER_PASSWORD(PlayerPrefs.GetString("PLAYFAB_USER_PASSWORD"));

//        //    AccountController.controller.LOGIN_ACTION();
//        //}
//        //else
//        //{
//        //    AccountController.controller.ON_CLIC_CREATE_ACCOUNT();
//        //}
//    }
//}
