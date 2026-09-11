using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Ajustes de movimiento")]
    public float speed = 3f; // Velocidad del enemigo
    private bool movinRight = true; // Indica si el enemigo se mueve hacia la derecha

    [Header("Detectores")]
    public Transform groundCheck; // Punto desde donde se lanza el raycast hacia el suelo
    public float distanceToGround = 1.5f; // Distancia para detectar el suelo
    public float distanceToWall = 1.5f; // Distancia para detectar paredes
    public LayerMask groundLayer; // Capa que representa el suelo/paredes

    private Rigidbody2D rb; // Referencia al Rigidbody2D del enemigo

    // Se ejecuta una sola vez al inicio
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D del enemigo
    }

    // Se ejecuta en cada frame
    void Update()
    {
        // Aplica velocidad en X: si va a la derecha usa "speed", si va a la izquierda usa "-speed"
        rb.linearVelocity = new Vector2(movinRight ? speed : -speed, rb.linearVelocity.y);

        // Dirección actual (derecha o izquierda)
        Vector2 direction = movinRight ? Vector2.right : Vector2.left;

        // Raycast hacia abajo desde groundCheck para verificar si hay suelo
        RaycastHit2D isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceToGround, groundLayer);

        // Raycast hacia adelante para verificar si hay pared
        RaycastHit2D isWallAhead = Physics2D.Raycast(transform.position, direction, distanceToWall, groundLayer);

        // Si no hay suelo adelante o hay una pared, se da la vuelta
        if (isGroundAhead.collider == null || isWallAhead.collider != null)
        {
            Flip();
        }
    }

    // Método para voltear al enemigo
    private void Flip()
    {
        movinRight = !movinRight; // Cambia la dirección (true ↔ false)
        Vector3 scale = transform.localScale; // Obtiene la escala actual
        scale.x *= -1; // Invierte el eje X (como un espejo)
        transform.localScale = scale; // Aplica la nueva escala
    }

    // Método para dibujar rayos en la vista de escena (solo visible en el editor)
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            // Dibuja el raycast hacia abajo
            Gizmos.DrawRay(groundCheck.position, Vector2.down * distanceToGround);
        }

        // Dibuja el raycast hacia adelante
        Vector2 direction = movinRight ? Vector2.right : Vector2.left;
        Gizmos.DrawRay(transform.position, direction * distanceToWall);
    }
}
