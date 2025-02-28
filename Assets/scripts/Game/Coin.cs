using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviourPun
{
    public int points = 1; // Puntos que otorga esta moneda al jugador que la recoge

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Si el jugador recoge la moneda, sumamos los puntos al marcador
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(points); // Añadir puntos al marcador
            }

            // Destruir la moneda para todos los jugadores de manera sincronizada
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
