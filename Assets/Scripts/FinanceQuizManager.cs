using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FinanceQuizManager : MonoBehaviour
{
    public TextMeshProUGUI factText; // Texto do fato
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Bot�es de resposta
    public AudioSource audioSource; // AudioSource para tocar os sons
    public AudioClip correctSound; // Som de acerto
    public AudioClip incorrectSound; // Som de erro
    public GameObject factPanel; // Painel do fato
    public GameObject questionPanel; // Painel da pergunta
    public TextMeshProUGUI themeText;
    public GameObject Canva; // Painel para controle adicional

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement; // Refer�ncia ao script PlayerMovement
    public GameObject book1; // Refer�ncia ao objeto do livro
    public GameObject player; // Refer�ncia ao jogador

    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] financeFacts = {
        "A poupan�a � o ato de guardar parte da renda para uso futuro, criando uma base financeira para imprevistos e objetivos de longo prazo.",
        "Planejar a aposentadoria � essencial para garantir uma fonte de renda no futuro, normalmente atrav�s de contribui��es para planos de previd�ncia p�blica e privada.",
        "O IRS (Imposto sobre o Rendimento das Pessoas Singulares) � um tributo cobrado em Portugal sobre os rendimentos de pessoas f�sicas, variando conforme a faixa de renda anual.",
        "O cart�o de cr�dito permite compras e pagamentos a prazo, mas os juros cobrados por atrasos no pagamento podem ser muito elevados.",
        "O empr�stimo estudantil permite que estudantes financiem sua educa��o e paguem o valor emprestado posteriormente, geralmente com juros baixos e prazos flex�veis.",
        "Definir objetivos financeiros claros, como comprar uma casa ou pagar d�vidas, ajuda a direcionar o planejamento financeiro e a alcan�ar metas."
    };

    private string[] financeQuestions = {
        "Qual � a principal vantagem de poupar parte da renda mensal?",
        "Por que � importante planejar a aposentadoria desde cedo?",
        "Para que serve o IRS (Imposto sobre o Rendimento das Pessoas Singulares) em Portugal?",
        "Qual � uma das principais desvantagens do cart�o de cr�dito?",
        "Qual � o prop�sito de um empr�stimo estudantil?",
        "Como definir objetivos financeiros ajuda no planejamento pessoal?"
    };

    private string[] correctAnswers = {
        "Cria uma base financeira para imprevistos e para realizar objetivos de longo prazo.",
        "Para garantir uma fonte de renda no futuro e manter o padr�o de vida desejado.",
        "� um tributo obrigat�rio que financia servi�os p�blicos e funciona conforme a faixa de renda anual da pessoa.",
        "Juros elevados em caso de atrasos, podendo gerar d�vidas dif�ceis de quitar.",
        "Permite financiar a educa��o e pagar o valor emprestado com condi��es facilitadas.",
        "Ajuda a direcionar o planejamento financeiro e alcan�ar metas espec�ficas."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "� um recurso opcional, sem grande impacto no futuro financeiro.", "Serve apenas para gastar em viagens e lazer." },
        new string[] { "Para aumentar a quantidade de gastos na juventude.", "Para garantir benef�cios como f�rias prolongadas na aposentadoria." },
        new string[] { "Para diminuir os custos de bens e servi�os pessoais.", "Para ajudar exclusivamente no financiamento de d�vidas pessoais." },
        new string[] { "Possibilidade de acumular recompensas de fidelidade.", "Melhorar o cr�dito sem custos extras, mesmo com atrasos." },
        new string[] { "Servir como apoio de curto prazo para despesas pessoais.", "Cobrir apenas materiais e despesas de lazer dos estudantes." },
        new string[] { "Permite focar mais em compras de alto valor e bens de luxo.", "Faz com que o planejamento se torne mais restritivo e limitado." }
    };

    private void Start()
    {
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5 }; // �ndices para controle de exibi��o de fatos/perguntas
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes(); // Reinicia a lista se todas as perguntas foram feitas
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];
        themeText.text = "Literacia Financeira";
        factText.text = financeFacts[currentFactIndex]; // Atualiza o texto do fato
        factIndexes.Remove(currentFactIndex); // Remove o �ndice exibido

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = financeQuestions[currentFactIndex];
        SetupAnswers();
    }

    private void SetupAnswers()
    {
        string correctAnswer = correctAnswers[currentFactIndex];
        string[] incorrectAnswersArray = incorrectAnswers[currentFactIndex];

        List<string> allAnswers = new List<string>(incorrectAnswersArray);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[i];
            answerButtons[i].onClick.RemoveAllListeners();

            bool isCorrect = (i == correctAnswerPosition);
            answerButtons[i].onClick.AddListener(() => AnswerSelected(isCorrect));
        }
    }

    private void AnswerSelected(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Resposta correta!");
            audioSource.clip = correctSound;
            DisableBookCollider(book1);
            Canva.SetActive(false);
            playerMovement.collidedStop = false;
        }
        else
        {
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;
            playerMovement.collidedStop = false;
            Canva.SetActive(false);
            if (player != null)
            {
                player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z); // Reposicionar jogador
                Debug.Log("Jogador reposicionado para a posi��o inicial.");
            }
        }

        audioSource.Play();
        DisplayRandomFact();
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
            Debug.LogWarning("Livro n�o encontrado.");
        }
    }
}
