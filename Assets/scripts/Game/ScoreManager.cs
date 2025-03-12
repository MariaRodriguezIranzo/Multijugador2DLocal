using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI globalScoreText;
    private int playerScore = 0;
    private static int globalScore = 0; // Puntuación global, compartida entre todos los jugadores

    void Start()
    {
        UpdateScoreDisplay();
    }

    [PunRPC]
    public void AddScore(int points)
    {
        playerScore += points;  // Sumar puntos al jugador
        globalScore += points;  // Sumar puntos a la puntuación global
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        playerScoreText.text = "Puntuación: " + playerScore;
        globalScoreText.text = "Puntuación Global: " + globalScore;
    }

    // RPC para actualizar la puntuación global en todos los jugadores
    [PunRPC]
    public void SetGlobalScore(int score)
    {
        globalScore = score;
        UpdateScoreDisplay();
    }

    // Para asegurar que la puntuación global está sincronizada al principio
    public void SyncGlobalScore()
    {
        photonView.RPC("SetGlobalScore", RpcTarget.AllBuffered, globalScore);
    }
}
