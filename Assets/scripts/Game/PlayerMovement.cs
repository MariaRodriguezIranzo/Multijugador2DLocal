using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public float speed = 10f; // Velocidad ajustada
    public float jumpForce = 7f; // Fuerza de salto
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>(); // Detectamos el collider automáticamente

        if (!photonView.IsMine)
        {
            rb.isKinematic = true; // Desactivar la física para los jugadores remotos
            return;
        }

        // Activamos la gravedad para evitar que el personaje flote
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Movimiento solo en horizontal
            movement.x = Input.GetAxis("Horizontal");

            // Si está en el suelo y presionamos "W", salta
            if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine && rb != null)
        {
            // Movimiento horizontal sin afectar el eje Y
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        }
    }

    // Detectar si el personaje está tocando el suelo
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
}
