using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private GameManager gameManager; // Referencia al GameManager

    public UnityEvent<string, int> submitScoreEvent;

    public void SubmitScore()
    {
        int score = gameManager.GetPoints(); // Obtener el puntaje del GameManager
        submitScoreEvent.Invoke(inputName.text, score);
    }
}