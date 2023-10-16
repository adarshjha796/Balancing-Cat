using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button tapToStartBtn;
    private void Start()
    {
        tapToStartBtn.onClick.AddListener(OnTapToStartPressed);
    }

    /// <summary>
    /// This method will load the game scene.
    /// </summary>
    private void OnTapToStartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
