using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScienceFinalManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Bot�es de resposta
    public AudioSource audioSource; // AudioSource para sons de resposta
    public AudioClip correctSound; // Som de resposta correta
    public AudioClip incorrectSound; // Som de resposta incorreta
    public GameObject quizPanel; // Painel que exibe a pergunta e op��es
    public GameObject victoryPanel; // Painel de vit�ria

    private int correctAnswersCount; // Contador de respostas corretas seguidas
    private int currentQuestionIndex;
    private List<int> questionIndexes;

    // Dados de perguntas e respostas de ci�ncia
    private string[] questions = {
        "O que � a gravidade e qual sua fun��o no sistema solar?",
        "Qual � a fun��o da camada de oz�nio na atmosfera terrestre?",
        "Qual � a teoria das placas tect�nicas e quais fen�menos ela ajuda a explicar?",
        "Qual � a fun��o das c�lulas nos organismos vivos?",
        "Como funciona o processo de evolu��o?",
        "Para que serve a fotoss�ntese nas plantas?"
    };

    private string[] correctAnswers = {
        "� a for�a que atrai os objetos uns aos outros e mant�m os planetas em �rbita ao redor do Sol.",
        "Absorver a maior parte da radia��o ultravioleta prejudicial, protegendo a Terra.",
        "Que a crosta terrestre � dividida em placas que se movem, causando terremotos e a forma��o de montanhas.",
        "Servem como unidades b�sicas de vida, permitindo a forma��o e o funcionamento dos organismos.",
        "� o processo de diversifica��o das esp�cies atrav�s da sele��o natural ao longo do tempo.",
        "Permite que as plantas produzam energia e oxig�nio, essenciais para a vida na Terra."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "� uma for�a que apenas afeta objetos grandes como planetas e estrelas.", "� uma for�a que repele objetos uns dos outros no espa�o." },
        new string[] { "Filtrar os gases nocivos produzidos pelos vulc�es.", "Gerar calor na atmosfera superior." },
        new string[] { "Que a Terra � composta por uma camada s�lida imut�vel.", "Que a crosta terrestre � uma �nica pe�a inquebr�vel." },
        new string[] { "Somente armazenam informa��es gen�ticas sem realizar nenhuma fun��o.", "Atuam apenas na reprodu��o dos organismos." },
        new string[] { "� um processo que ocorre de maneira instant�nea em todas as esp�cies.", "� uma mudan�a aleat�ria sem prop�sito na natureza." },
        new string[] { "� um processo pelo qual as plantas absorvem �gua sem produzir energia.", "� uma fun��o que ocorre apenas em organismos aqu�ticos." }
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
        ShuffleList(answerOptions); // M�todo para embaralhar respostas

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
