using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScienceFinalManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Botões de resposta
    public AudioSource audioSource; // AudioSource para sons de resposta
    public AudioClip correctSound; // Som de resposta correta
    public AudioClip incorrectSound; // Som de resposta incorreta
    public GameObject quizPanel; // Painel que exibe a pergunta e opções
    public GameObject victoryPanel; // Painel de vitória
    public TextMeshProUGUI sciencePointsText;

    public GameObject finalCanvas; // Canvas final
    public GameObject scienceBook; // Objeto associado ao quiz (ex.: livro de ciência)
    public PlayerMovement playerMovement; // Referência ao script de movimento do jogador

    private int correctAnswersCount; // Contador de respostas corretas seguidas
    private int currentQuestionIndex;
    private List<int> questionIndexes;

    // Dados de perguntas e respostas de ciência
    private string[] questions = {
       "O que é a gravidade e qual sua função no sistema solar?",
    "Qual é a função da camada de ozônio na atmosfera terrestre?",
    "Qual é a teoria das placas tectônicas e quais fenômenos ela ajuda a explicar?",
    "Qual é a função das células nos organismos vivos?",
    "Como funciona o processo de evolução?",
    "Para que serve a fotossíntese nas plantas?",
    "O que é o DNA e qual a sua função nos organismos vivos?",
    "Por que a água é essencial para a vida na Terra?",
    "O que são buracos negros e quais são suas características?",
    "Como os vírus se replicam e qual seu impacto na saúde?",
    "O que é o efeito estufa e como ele influencia o clima da Terra?"
    };

    private string[] correctAnswers = {
    "É a força que atrai os objetos uns aos outros e mantém os planetas em órbita ao redor do Sol.",
    "Absorver a maior parte da radiação ultravioleta prejudicial, protegendo a Terra.",
    "Que a crosta terrestre é dividida em placas que se movem, causando terremotos e a formação de montanhas.",
    "Servem como unidades básicas de vida, permitindo a formação e o funcionamento dos organismos.",
    "É o processo de diversificação das espécies através da seleção natural ao longo do tempo.",
    "Permite que as plantas produzam energia e oxigênio, essenciais para a vida na Terra.",
    "Contém as instruções genéticas que regulam o desenvolvimento e o funcionamento dos organismos vivos.",
    "É essencial para processos biológicos como hidratação celular e transporte de nutrientes.",
    "São regiões com gravidade tão forte que nem a luz pode escapar, deformando o espaço ao seu redor.",
    "Dependem de hospedeiros vivos para se replicar, causando doenças ao invadir células hospedeiras.",
    "É um fenômeno que mantém a Terra aquecida, mas sua intensificação pode levar a mudanças climáticas."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "É uma força que apenas afeta objetos grandes como planetas e estrelas.", "É uma força que repele objetos uns dos outros no espaço." },
    new string[] { "Reflete a radiação solar para o espaço, regulando o clima.", "Diminui a quantidade de oxigênio na atmosfera." },
    new string[] { "Que os continentes são imóveis e fixos na crosta terrestre.", "Que a superfície terrestre é completamente sólida e inquebrável." },
    new string[] { "Somente armazenam informações genéticas sem realizar nenhuma função.", "Atuam apenas na reprodução dos organismos." },
    new string[] { "É um processo que ocorre em dias de chuva e influencia o comportamento animal.", "Um ciclo biológico que apenas afeta os animais." },
    new string[] { "Produz apenas glicose, mas não tem efeito no oxigênio.", "É apenas um processo de absorção de luz solar sem outra função." },
    new string[] { "É uma molécula que serve apenas como fonte de energia para células.", "Atua exclusivamente na reprodução de organismos multicelulares." },
    new string[] { "É importante apenas para a formação de oceanos e rios.", "Apenas organismos aquáticos precisam dela para sobreviver." },
    new string[] { "São estrelas em colapso que brilham intensamente.", "São buracos que liberam luz e matéria no espaço." },
    new string[] { "Se replicam sozinhos sem precisar de células hospedeiras.", "Atuam apenas de forma benéfica para os organismos vivos." },
    new string[] { "Aumenta a quantidade de oxigênio na atmosfera.", "Não tem impacto direto na temperatura da Terra." }
    };

    private void Start()
    {
        StartQuiz();
    }

    private void StartQuiz()
    {
        // Inicializa o quiz com todas as perguntas
        questionIndexes = new List<int>();
        for (int i = 0; i < questions.Length; i++)
        {
            questionIndexes.Add(i);
        }
        ShuffleQuestions();
        correctAnswersCount = 0; // Reseta contador de respostas corretas seguidas
        LoadQuestion();

        // Desativa o movimento do jogador enquanto o quiz está ativo
        if (playerMovement != null)
        {
            playerMovement.collidedStop = true;
        }
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < questionIndexes.Count; i++)
        {
            int temp = questionIndexes[i];
            int randomIndex = Random.Range(i, questionIndexes.Count);
            questionIndexes[i] = questionIndexes[randomIndex];
            questionIndexes[randomIndex] = temp;
        }
    }

    private void LoadQuestion()
    {
        if (questionIndexes.Count == 0)
        {
            Debug.Log("No more questions available.");
            return;
        }

        currentQuestionIndex = questionIndexes[0];
        questionIndexes.RemoveAt(0);

        questionText.text = questions[currentQuestionIndex];
        List<string> answerOptions = new List<string>(incorrectAnswers[currentQuestionIndex]);
        answerOptions.Add(correctAnswers[currentQuestionIndex]);
        ShuffleList(answerOptions); // Método para embaralhar respostas

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i];
            answerButtons[i].onClick.RemoveAllListeners();
            bool isCorrect = answerOptions[i] == correctAnswers[currentQuestionIndex];
            answerButtons[i].onClick.AddListener(() => Answer(isCorrect));
        }
    }

    private void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void Answer(bool isCorrect)
    {
        if (isCorrect)
        {
            correctAnswersCount++;
            audioSource.PlayOneShot(correctSound);

            GameData.sciencePoints++;
            sciencePointsText.text = GameData.sciencePoints.ToString();
            if (correctAnswersCount >= 3)
            {
                EndQuiz();
                return;
            }
        }
        else
        {
            if (GameData.sciencePoints > 1)
            {
                GameData.sciencePoints -= 2;

            }
            else
            {
                GameData.sciencePoints = 0;
            }
            sciencePointsText.text = GameData.sciencePoints.ToString();
            audioSource.PlayOneShot(incorrectSound);
            correctAnswersCount = 0; // Reinicia contagem de respostas corretas seguidas
            StartQuiz(); // Reinicia o quiz
            return;
        }
        LoadQuestion();
    }

    private void EndQuiz()
    {
        GameData.concluded = true;
        GameData.sciencePoints += 3;
        sciencePointsText.text = GameData.sciencePoints.ToString();
        
        // Finaliza o quiz
        quizPanel.SetActive(false);
        victoryPanel.SetActive(true);
      
        // Reativa o movimento do jogador
        if (playerMovement != null)
        {
            playerMovement.collidedStop = false;
        }

        // Desativa o colisor do objeto associado ao quiz (ex.: livro)
        DisableBookCollider(scienceBook);
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book != null)
        {
            BoxCollider2D bookCollider = book.GetComponent<BoxCollider2D>();
            Rigidbody2D bookRigidbody = book.GetComponent<Rigidbody2D>();

            if (bookCollider != null)
            {
                bookCollider.enabled = false; // Desativa o Box Collider 2D
            }

            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic; // Define o corpo como dinâmico
                bookRigidbody.gravityScale = 10;
            }

            Destroy(book, 5); // Destroi o objeto após 5 segundos
        }
    }
}
