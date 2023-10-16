using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreTxt;

    [SerializeField]
    private TMP_Text itemTxt;

    [SerializeField]
    private TextMesh heightTxt;

    private DistanceMeter distanceMeter;

    private GameObject heightMeter;

    private ObjectScript os;

    private void Awake()
    {
        distanceMeter = FindObjectOfType<DistanceMeter>();
        heightMeter = GameObject.FindGameObjectWithTag("Height");
        os = GetComponent<ObjectScript>();
    }
    private void Start()
    {
        heightMeter.SetActive(false);
    }

    public void OnReplayPressed()
    {
        GamePlayController.instance.RestartGame();
        Cat.latestDistanceMeterValue = -1.2f;
        Cat.latestDistance = 0;
    }

    public void OnGotoMainMenuPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OnCapturePressed()
    {
        RepositionHeightMeter(distanceMeter.transform.position.y, distanceMeter.GetCurrentDistanceTextValue());
    }

    public void UpdateScore(float score)
    {
        score = Mathf.Round(score * 10f) / 10f;
        scoreTxt.text = score.ToString("f1");
    }

    public void UpdateItem(int item)
    {
        itemTxt.text = item.ToString();
    }

    public void RepositionHeightMeter(float yPos, float height)
    {
        if (!heightMeter.activeSelf)
        {
            heightMeter.SetActive(true);
        }

        Vector3 pos = heightMeter.transform.position;
        pos.x = 0;
        pos.y = yPos;
        heightMeter.transform.position = pos;
        heightTxt.text = "Top: " + height.ToString() + " cm";
    }
}
