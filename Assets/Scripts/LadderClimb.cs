using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 3f; // Velocidade de subida
    private bool isClimbing = false; // Se o jogador está na escada
    private Rigidbody2D rb; // Referência ao Rigidbody2D do jogador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D do jogador
    }

    void Update()
    {
        // Só processa a movimentação vertical se o jogador estiver na escada
        if (isClimbing)
        {
            // Movimento vertical ao pressionar as setas ou o eixo "Vertical"
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Define a velocidade do Rigidbody para movimentação na escada
            rb.velocity = new Vector2(0f, verticalInput * climbSpeed); // Desativa a movimentação horizontal

            // Desativa a gravidade enquanto estiver subindo a escada
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta se o jogador entrou na área da escada
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;  // Habilita o comportamento de escalar
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Quando o jogador sai da escada
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;  // Desabilita o comportamento de escalar
            rb.gravityScale = 1; // Restaura a gravidade normal do jogador
        }
    }
}
