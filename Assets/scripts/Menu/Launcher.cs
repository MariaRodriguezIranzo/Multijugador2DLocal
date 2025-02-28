using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Asegúrate de importar esta línea para manejar los botones

public class Launcher : MonoBehaviourPunCallbacks
{
    // Referencia al botón que usaremos para la conexión
    public Button connectButton;

    void Start()
    {
        // Configuramos el botón para que cuando se haga clic, se llame al método OnClickConnect
        connectButton.onClick.AddListener(OnClickConnect);
    }

    // Este método se llama cuando el botón es presionado
    public void OnClickConnect()
    {
        // Intentamos conectarnos a Photon
        PhotonNetwork.ConnectUsingSettings();
    }

    // Este callback se llama cuando el jugador se conecta exitosamente a Photon
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor de Photon.");

        // Intentamos unirnos a una sala aleatoria o creamos una nueva si no hay disponible
        PhotonNetwork.JoinRandomRoom();
    }

    // Si no se puede unir a una sala, creamos una nueva sala
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No se pudo unir a una sala aleatoria. Creando una nueva sala...");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }

    // Cuando el jugador se une correctamente a la sala
    public override void OnJoinedRoom()
    {
        Debug.Log("Unido a la sala.");

        // Ahora que estamos en la sala, cambiamos a la escena del juego
        SceneManager.LoadScene("Game");
    }
}
