using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public LeaderboardHandler leaderboardHandler;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        leaderboardHandler = FindObjectOfType<LeaderboardHandler>();

        ApiCalls.instance.GetAllUserDataFunction();

        yield return new WaitForSeconds(1f);

        leaderboardHandler.canShowLeaderboard();
    }
}
