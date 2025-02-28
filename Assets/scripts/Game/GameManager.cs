using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab; // Prefab del jugador
    public Transform spawnPoint;    // Punto donde los jugadores aparecerán

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
        else
        {
            Debug.LogError("No estás conectado a una sala.");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a la sala. Instanciando jugador...");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.Log("🔎 Buscando prefab en Resources...");
            playerPrefab = Resources.Load<GameObject>("Player");

            if (playerPrefab == null)
            {
                Debug.LogError("🚨 Error: El prefab 'Player' no se encuentra en Resources.");
                return;
            }
        }

        if (spawnPoint == null)
        {
            Debug.LogError("❌ Error: No se asignó un spawnPoint en el GameManager.");
            return;
        }

        // Definir la posición correcta desde el inicio
        Vector3 spawnPosition = spawnPoint.position;

        Debug.Log("🚀 Instanciando jugador en: " + spawnPosition);

        // Instanciamos al jugador en la posición del spawn
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);

        if (player != null)
        {
            Debug.Log("✅ Jugador instanciado correctamente en: " + player.transform.position);
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0; // Desactivar la gravedad
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            Debug.LogError("❌ Error: El jugador no se instanció.");
        }
    }
}
