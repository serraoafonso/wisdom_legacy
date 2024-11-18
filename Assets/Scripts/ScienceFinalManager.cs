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
        "Para que serve a fotossíntese nas plantas?"
    };

    private string[] correctAnswers = {
        "É a força que atrai os objetos uns aos outros e mantém os planetas em órbita ao redor do Sol.",
        "Absorver a maior parte da radiação ultravioleta prejudicial, protegendo a Terra.",
        "Que a crosta terrestre é dividida em placas que se movem, causando terremotos e a formação de montanhas.",
        "Servem como unidades básicas de vida, permitindo a formação e o funcionamento dos organismos.",
        "É o processo de diversificação das espécies através da seleção natural ao longo do tempo.",
        "Permite que as plantas produzam energia e oxigênio, essenciais para a vida na Terra."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "É uma força que apenas afeta objetos grandes como planetas e estrelas.", "É uma força que repele objetos uns dos outros no espaço." },
        new string[] { "Filtrar os gases nocivos produzidos pelos vulcões.", "Gerar calor na atmosfera superior." },
        new string[] { "Que a Terra é composta por uma camada sólida imutável.", "Que a crosta terrestre é uma única peça inquebrável." },
        new string[] { "Somente armazenam informações genéticas sem realizar nenhuma função.", "Atuam apenas na reprodução dos organismos." },
        new string[] { "É um processo que ocorre de maneira instantânea em todas as espécies.", "É uma mudança aleatória sem propósito na natureza." },
        new string[] { "É um processo pelo qual as plantas absorvem água sem produzir energia.", "É uma função que ocorre apenas em organismos aquáticos." }
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
            if (correctAnswersCount >= 3)
            {
                Victory();
                return;
            }
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound);
            correctAnswersCount = 0; // Reinicia contagem de respostas corretas seguidas
            StartQuiz(); // Reinicia o quiz
            return;
        }
        LoadQuestion();
    }

    private void Victory()
    {
        quizPanel.SetActive(false);
        victoryPanel.SetActive(true);
    }
}
