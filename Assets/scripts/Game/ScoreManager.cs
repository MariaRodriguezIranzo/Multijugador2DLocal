using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI globalScoreText;
    private int playerScore = 0;
    private static int globalScore = 0; // Puntuaci�n global, compartida entre todos los jugadores

    void Start()
    {
        UpdateScoreDisplay();
    }

    [PunRPC]
    public void AddScore(int points)
    {
        playerScore += points;  // Sumar puntos al jugador
        globalScore += points;  // Sumar puntos a la puntuaci�n global
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        playerScoreText.text = "Puntuaci�n: " + playerScore;
        globalScoreText.text = "Puntuaci�n Global: " + globalScore;
    }

    // RPC para actualizar la puntuaci�n global en todos los jugadores
    [PunRPC]
    public void SetGlobalScore(int score)
    {
        globalScore = score;
        UpdateScoreDisplay();
    }

    // Para asegurar que la puntuaci�n global est� sincronizada al principio
    public void SyncGlobalScore()
    {
        photonView.RPC("SetGlobalScore", RpcTarget.AllBuffered, globalScore);
    }
}
