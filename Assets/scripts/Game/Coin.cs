using Photon.Pun;
using UnityEngine;

public class Coin : MonoBehaviourPun
{
    public int points = 1;  // Puntos que otorga la moneda al jugador que la recoge

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Si el jugador recoge la moneda, sumamos los puntos al marcador
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                // Llamamos al método AddScore a todos los jugadores mediante RPC
                scoreManager.photonView.RPC("AddScore", RpcTarget.All, points);
            }

            // Destruir la moneda para todos los jugadores de manera sincronizada
            PhotonNetwork.Destroy(gameObject);

            // Llamamos al CoinSpawner para que respawnee la moneda
            CoinSpawner coinSpawner = FindObjectOfType<CoinSpawner>();
            if (coinSpawner != null)
            {
                // Se le pasa la posición de spawn para el respawn
                coinSpawner.RespawnCoin(transform.position);
            }
        }
    }
}
