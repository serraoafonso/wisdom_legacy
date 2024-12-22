using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 3f; // Velocidade de subida
    private bool isClimbing = false; // Se o jogador est� na escada
    private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do jogador

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obt�m o Rigidbody2D do jogador
    }

    void Update()
    {
        // S� processa a movimenta��o vertical se o jogador estiver na escada
        if (isClimbing)
        {
            // Movimento vertical ao pressionar as setas ou o eixo "Vertical"
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Define a velocidade do Rigidbody para movimenta��o na escada
            rb.velocity = new Vector2(0f, verticalInput * climbSpeed); // Desativa a movimenta��o horizontal

            // Desativa a gravidade enquanto estiver subindo a escada
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta se o jogador entrou na �rea da escada
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
