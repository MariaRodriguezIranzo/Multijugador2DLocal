using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float speed = 60f; // Ajusta la velocidad a un valor más alto
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!photonView.IsMine)
        {
            rb.isKinematic = true; // Desactivar la física para los jugadores remotos
            return;
        }

        // Aseguramos que no hay gravedad ni movimiento no deseado al comenzar
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Obtener las entradas del jugador para el movimiento
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine && rb != null)
        {
            // Aplicar la velocidad correctamente
            rb.velocity = movement * speed;
        }
        else if (rb != null)
        {
            // Evitar que los jugadores remotos se muevan
            rb.velocity = Vector2.zero;
        }
    }
}
