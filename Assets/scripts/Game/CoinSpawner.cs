using Photon.Pun;
using UnityEngine;

public class CoinSpawner : MonoBehaviourPun
{
    public GameObject coinPrefab;         // Prefab de la moneda
    public Transform[] coinSpawnPoints;   // Array de puntos de spawn para las monedas
    public float respawnTime = 3f;        // Tiempo que tardan las monedas en reaparecer

    void Start()
    {
        // Aseguramos que solo el master client (el que crea la sala) instancie las monedas
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnCoins();
        }
    }

    void SpawnCoins()
    {
        // Aseguramos que hay puntos de spawn asignados
        if (coinSpawnPoints.Length > 0)
        {
            // Por cada punto de spawn, instanciamos una moneda
            foreach (Transform spawnPoint in coinSpawnPoints)
            {
                // Instanciamos la moneda en la posición del punto de spawn
                PhotonNetwork.Instantiate(coinPrefab.name, spawnPoint.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogError("No hay puntos de spawn de monedas asignados.");
        }
    }

    // Método para hacer respawn de las monedas después de que se destruyen
    [PunRPC]
    public void RespawnCoin(Vector3 spawnPosition)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Esperar unos segundos antes de hacer respawn
            PhotonNetwork.Instantiate(coinPrefab.name, spawnPosition, Quaternion.identity);
        }
    }
}
