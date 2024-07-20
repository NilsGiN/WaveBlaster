using Dan.Main;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Laderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    private string publicLeaderboardKey = "6e3e6fb3e853b8344c38fecbe1407fc7bff620262caa523160859b17ba2aaa22";
    private void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {   
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
                Debug.Log(msg[i].Score.ToString());
            }
        }));
    }
    public void SetLeaderBoardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) => {

            GetLeaderBoard();
        }));
    }
}
