using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

        // Configurar botões de resposta
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
        }
        else
        {
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;
        }

        audioSource.Play();
        DisplayRandomFact();
    }
}
