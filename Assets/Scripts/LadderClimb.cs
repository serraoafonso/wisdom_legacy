using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public float climbSpeed = 3f; // Velocidade de subida
    public float jumpForce = 5f; // Força do salto
    private bool isClimbing = false; // Se o jogador está na escada
    private bool canJumpAtTop = false; // Se o jogador pode saltar no topo da escada
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

            // Detecta se o jogador está no topo da escada e pressionou o botão de salto
            if (verticalInput > 0 && canJumpAtTop && Input.GetButtonDown("Jump"))
            {
                JumpAtTop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta se o jogador entrou na área da escada
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
        // Adiciona força para o salto no topo da escada
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isClimbing = false; // Desativa o estado de escalada
        rb.gravityScale = 1; // Restaura a gravidade após o salto
    }
}
