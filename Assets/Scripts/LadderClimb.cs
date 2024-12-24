using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 3f; // Velocidade de subida
    public float jumpForce = 5f; // For�a do salto
    private bool isClimbing = false; // Se o jogador est� na escada
    private bool canJumpAtTop = false; // Se o jogador pode saltar no topo da escada
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

            // Detecta se o jogador est� no topo da escada e pressionou o bot�o de salto
            if (verticalInput > 0 && canJumpAtTop && Input.GetButtonDown("Jump"))
            {
                JumpAtTop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta se o jogador entrou na �rea da escada
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;  // Habilita o comportamento de escalar
        }

        // Detecta se o jogador chegou ao topo da escada
        if (other.CompareTag("LadderTop"))
        {
            canJumpAtTop = true; // Permite o salto no topo
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

        // Quando o jogador sai do topo da escada
        if (other.CompareTag("LadderTop"))
        {
            canJumpAtTop = false; // Desabilita o salto ao sair do topo
        }
    }

    private void JumpAtTop()
    {
        // Adiciona for�a para o salto no topo da escada
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isClimbing = false; // Desativa o estado de escalada
        rb.gravityScale = 1; // Restaura a gravidade ap�s o salto
    }
}
