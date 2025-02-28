using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI globalScoreText;
    private int playerScore = 0;

    void Start()
    {
        UpdateScoreDisplay();
    }

    [PunRPC]
    public void AddScore(int points)
    {
        playerScore += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        playerScoreText.text = "Puntuación: " + playerScore;
        globalScoreText.text = "Puntuación Global: " + playerScore; // Puedes sumar puntos de todos aquí
    }
}
