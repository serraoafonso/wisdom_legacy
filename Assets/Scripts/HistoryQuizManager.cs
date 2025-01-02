using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class HistoryQuizManager : MonoBehaviour
{
    public TextMeshProUGUI factText; // Texto do fato histórico
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Botões de resposta
    public AudioSource audioSource; // AudioSource para sons de resposta
    public AudioClip correctSound; // Som de resposta correta
    public AudioClip incorrectSound; // Som de resposta incorreta
    public GameObject factPanel; // Painel que exibe o fato histórico
    public GameObject questionPanel; // Painel que exibe a pergunta
    public TextMeshProUGUI themeText;
    public GameObject Canva; // Painel para controle adicional

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement; // Referência ao script PlayerMovement
    public GameObject book1; // Referência ao objeto do livro
    public GameObject book2; // Referência ao objeto do livro
    public GameObject book3; // Referência ao objeto do livro
    public GameObject book4; // Referência ao objeto do livro
    public GameObject player; // Referência ao jogador

    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] historyFacts = {
        "O Renascimento foi um movimento cultural que ocorreu na Europa entre os séculos XIV e XVII, caracterizado pelo ressurgimento das artes, ciências e do pensamento humanista.",
        "A Revolução Francesa, que começou em 1789, foi um marco que derrubou a monarquia absolutista na França, resultando em profundas mudanças sociais e políticas.",
        "A Guerra Civil Americana (1861-1865) foi um conflito entre os estados do Norte e do Sul dos EUA, em grande parte devido à questão da escravidão e à luta pela preservação da União.",
        "A Primeira Guerra Mundial (1914-1918) envolveu grandes potências mundiais e resultou em milhões de mortos, além de mudanças significativas no mapa político da Europa.",
        "A Era das Grandes Navegações, entre os séculos XV e XVII, foi um período de expansão marítima europeia que levou à descoberta de novas rotas e territórios ao redor do mundo.",
        "A criação da Organização das Nações Unidas (ONU) em 1945 teve como objetivo promover a paz e a cooperação internacional após a devastação da Segunda Guerra Mundial."
    };

    private string[] historyQuestions = {
        "Qual foi o impacto cultural do Renascimento na Europa?",
        "Quais foram algumas das principais consequências da Revolução Francesa?",
        "Qual foi a causa central da Guerra Civil Americana?",
        "Qual foi o principal resultado da Primeira Guerra Mundial para o mapa político europeu?",
        "Qual era o principal objetivo da Era das Grandes Navegações?",
        "Por que foi criada a ONU em 1945?"
    };

    private string[] correctAnswers = {
        "Ressurgimento das artes e ciências e fortalecimento do pensamento humanista.",
        "Derrubada da monarquia e estabelecimento de princípios de igualdade e liberdade.",
        "Conflito entre estados do Norte e do Sul sobre a questão da escravidão e a preservação da União.",
        "Mudanças significativas nas fronteiras europeias e o enfraquecimento de impérios tradicionais.",
        "Descobrir novas rotas e territórios para expandir o comércio e a influência europeia.",
        "Promover a paz e a cooperação internacional após a Segunda Guerra Mundial."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "Declínio do interesse em ciências e artes, com foco exclusivo em religião.", "Expansão da monarquia absoluta na Europa." },
        new string[] { "Fortalecimento da monarquia na França.", "Criação de uma aliança entre França e Inglaterra contra a Áustria." },
        new string[] { "Questões territoriais com o México.", "Conflito religioso entre estados do Norte e do Sul dos EUA." },
        new string[] { "Unificação completa dos países europeus sob um governo único.", "Expansão do império alemão sobre toda a Europa Ocidental." },
        new string[] { "Exploração de minas de ouro no continente africano.", "Conquista e colonização da Ásia exclusivamente." },
        new string[] { "Promover a divisão dos territórios coloniais da Europa.", "Organizar o comércio internacional e a exploração econômica global." }
    };

    private void Start()
    {
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5 }; // Índices para selecionar perguntas não repetidas
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes(); // Reinicia a lista se todas as perguntas foram feitas
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];
        themeText.text = "História";
        factText.text = historyFacts[currentFactIndex]; // Atualiza o fato histórico exibido
        factIndexes.Remove(currentFactIndex); // Remove o índice exibido

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = historyQuestions[currentFactIndex];
        SetupAnswers();
    }

    private void SetupAnswers()
    {
        // Obter a resposta correta
        string correctAnswer = correctAnswers[currentFactIndex];

        // Obter as respostas incorretas
        string[] incorrectAnswersArray = incorrectAnswers[currentFactIndex];

        // Criar lista para todas as respostas e adicionar a correta em posição aleatória
        List<string> allAnswers = new List<string>(incorrectAnswersArray);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        // Certifique-se de que temos o mesmo número de botões que respostas
        if (answerButtons.Length < allAnswers.Count)
        {
            Debug.LogError("O número de botões é menor que o número de respostas disponíveis!");
            return;
        }

        // Configurar botões de resposta
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < allAnswers.Count)
            {
                int buttonIndex = i; // Captura o índice localmente para evitar problemas com closures

                // Configurar o botão com o texto da resposta
                answerButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[buttonIndex];
                answerButtons[buttonIndex].onClick.RemoveAllListeners();

                bool isCorrect = (buttonIndex == correctAnswerPosition);
                answerButtons[buttonIndex].onClick.AddListener(() => AnswerSelected(isCorrect, answerButtons[buttonIndex].gameObject));

                answerButtons[buttonIndex].gameObject.SetActive(true); // Certifique-se de ativar o botão
            }
            else
            {
                // Desativar botões extras
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }



    private void AnswerSelected(bool isCorrect, GameObject botao)
    {
        Vector3 playerPosition = player.transform.position;
        GameObject targetBook = DetermineBookBasedOnPlayerPosition();

        if (isCorrect)
        {
            botao.GetComponent<Image>().color = Color.green; // Indica resposta correta
            Debug.Log("Resposta correta!");
            audioSource.clip = correctSound;

            if (targetBook != null)
            {
                DisableBookCollider(targetBook);
            }

            // Mantém o Canvas ativo por um tempo para mostrar a cor/som
            StartCoroutine(HandleAnswerFeedback(true, botao));
        }
        else
        {
            botao.GetComponent<Image>().color = Color.red; // Indica resposta incorreta
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;

            if (player != null)
            {
                // Reposiciona o jogador com base na posição Y
                if (playerPosition.y < 10)
                {
                    player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z);
                    Debug.Log("Jogador reposicionado para a posição inicial.");
                }
                else
                {
                    player.transform.position = new Vector3(-130, 37, player.transform.position.z); // Reposicionar jogador
                    Debug.Log("Jogador reposicionado para a posição inicial.");
                }
            }

            // Mantém o Canvas ativo por um tempo para mostrar a cor/som
            StartCoroutine(HandleAnswerFeedback(false, botao));
        }

        audioSource.Play();
    }

    // Corrotina para atrasar o fechamento do Canvas
    private IEnumerator HandleAnswerFeedback(bool isCorrect, GameObject botao)
    {
        // Aguarda 1 segundo para mostrar a cor e tocar o som
        yield return new WaitForSeconds(0.2f);

        // Reseta a cor do botão para a cor padrão
        botao.GetComponent<Image>().color = Color.white;

        // Fecha o Canvas após o feedback
        Canva.SetActive(false);
        playerMovement.collidedStop = false;

        // Exibe um novo fato apenas após o feedback
        DisplayRandomFact();
    }


    // Método para determinar qual livro alterar com base na posição do jogador
    private GameObject DetermineBookBasedOnPlayerPosition()
    {
        Vector3 playerPosition = player.transform.position;

        if (playerPosition.y < 10) // Exemplo de condição
        {
            return book1;
        }
        else if (playerPosition.x <= -100)
        {
            return book2;
        }
        else if (playerPosition.x <= -80)
        {
            return book3;
        }
        else
        {
            return book4;
        }
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book != null)
        {
            BoxCollider2D bookCollider = book.GetComponent<BoxCollider2D>();
            Rigidbody2D bookRigidbody = book.GetComponent<Rigidbody2D>();

            if (bookCollider != null)
            {
                bookCollider.enabled = false;
                Debug.Log($"Box Collider 2D do livro {book.name} desativado.");
            }
            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic;
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} alterado para Dynamic.");
            }
            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning("Livro não encontrado.");
        }
    }
}
