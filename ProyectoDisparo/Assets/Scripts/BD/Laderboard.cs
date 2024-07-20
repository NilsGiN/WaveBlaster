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
    private string publicLeaderboardKey = "3739b03a61cc1af7524694f1a5af456273e1817b946d0046c9651932015abc7b";
    private void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {   
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
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
