using Photon.Pun;
using UnityEngine;

public class CoinSpawner : MonoBehaviourPun
{
    public GameObject coinPrefab;         // Prefab de la moneda
    public Transform[] coinSpawnPoints;   // Array de puntos de spawn para las monedas

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
}
