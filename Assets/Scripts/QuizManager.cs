using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

[System.Serializable]
public class ThemeData
{
    public string[] facts;
    public string[] questions;
    public string[] correctAnswers;
    public string[][] incorrectAnswers;
}

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI themeText;
    public TextMeshProUGUI factText;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public GameObject factPanel, questionPanel;
    public AudioSource audioSource;
    public AudioClip correctSound, incorrectSound;
    public Button nextButton;

    private int currentThemeIndex = 0;
    private int currentFactIndex = 0;

    // Criação dos dados dos temas como objetos ThemeData
    private List<ThemeData> themesData = new List<ThemeData>
    {
        new ThemeData
        {
            facts = new string[] {
                "O DNA contém as instruções genéticas que regulam o desenvolvimento e funcionamento dos organismos. Ele é passado de geração em geração, garantindo a hereditariedade das características entre os seres vivos.",
                "Efeito de Estufa: Fenômeno em que gases na atmosfera da Terra, como o dióxido de carbono, retêm calor.",
                "Teoria do Big Bang: Sugere que o universo começou há cerca de 13,8 bilhões de anos a partir de uma explosão."
            },
            questions = new string[] {
                "Isto implica...",
                "Portanto o aumento do efeito de estufa leva ao...",
                "O que é a Teoria do Big Bang?"
            },
            correctAnswers = new string[] {
                " que teoricamente todos os organismos têm um ancestral comum.",
                " Aumento das temperaturas médias da Terra, causando mudanças climáticas.",
                " A sugestão de que o universo começou a partir de uma explosão."
            },
            incorrectAnswers = new string[][] {
                new string[] { " que cada espécie desenvolve seu próprio DNA sem relação com outras.", " que o DNA muda completamente em cada geração." },
                new string[] { " Resfriamento global, pois a camada de ozônio se torna mais espessa.", " Melhoria na qualidade do ar devido à redução das emissões de carbono." },
                new string[] { " A ideia de que o universo sempre existiu.", " Uma teoria sobre a formação de estrelas." }
            }
        },
        new ThemeData
        {
            facts = new string[] {
                "A inflação é o aumento geral dos preços de bens e serviços em uma economia ao longo do tempo. Isso significa que, com a inflação, o dinheiro perde seu poder de compra.",
                "Segundo a lei da oferta, quanto maior o preço, maior a oferta e menor a procura; e, consequentemente, quanto menor o preço, menor a oferta e maior a procura.",
                "Ter uma reserva de emergência é fundamental para evitar endividamento em situações inesperadas. A recomendação geral é que essa reserva cubra de 3 a 6 meses de despesas mensais básicas."
            },
            questions = new string[] {
                "Qual dos seguintes fatores pode contribuir para o aumento da inflação?",
                "O João tem uma venda de sumo de laranja e pretende aumentar o preço do seu produto. O que acontecerá à venda do João?",
                "Qual é o principal objetivo de uma reserva de emergência?"
            },
            correctAnswers = new string[] {
                " Aumento nos preços do petróleo.",
                " A procura diminui.",
                " Fornecer um suporte financeiro durante períodos de imprevistos ou perda de renda."
            },
            incorrectAnswers = new string[][] {
                new string[] { " Redução dos salários.", " Aumento do desemprego." },
                new string[] { " A oferta diminui.", " A procura aumenta." },
                new string[] { " Possibilitar investimentos em produtos financeiros de alto rendimento.", " Proteger o patrimônio contra flutuações de mercado." }
            }
        },
        new ThemeData
        {
            facts = new string[] {
                "A Revolução Industrial, que começou no final do século XVIII na Inglaterra, marcou uma transição significativa de economias agrárias e artesanais para economias industriais e urbanas, alterando a maneira como as pessoas trabalhavam e viviam.",
                "A Queda do Muro de Berlim, em 1989, foi um evento simbólico que marcou o fim da Guerra Fria. O muro, que dividia a Alemanha Oriental e Ocidental desde 1961, foi um símbolo da separação ideológica entre o bloco soviético e o ocidental.",
                "A Revolução dos Cravos, que ocorreu em 25 de abril de 1974, foi um movimento pacífico que resultou na queda da ditadura do Estado Novo em Portugal. Este evento é comemorado anualmente como o Dia da Liberdade e simboliza a transição do país para a democracia."
            },
            questions = new string[] {
                "Qual das seguintes mudanças sociais foi uma consequência direta da Revolução Industrial?",
                "Qual foi uma das principais consequências da queda do Muro de Berlim?",
                "Qual foi o principal objetivo da Revolução dos Cravos em 1974?"
            },
            correctAnswers = new string[] {
                " Crescimento das cidades e urbanização.",
                " Unificação da Alemanha.",
                " Derrubar a ditadura do Estado Novo e restaurar a democracia."
            },
            incorrectAnswers = new string[][] {
                new string[] { " Redução do comércio internacional.", " Aumento da população rural." },
                new string[] { " Aumento da imigração para a Alemanha Oriental.", " Redução do comércio entre a Alemanha e a França." },
                new string[] { " Aumentar a influência de Portugal nas colônias africanas.", " Expandir o território português na Europa." }
            }
        }
    };

    private List<int>[] questionPool;

    private void Start()
    {
        InitializeQuestionPool();
        SetTheme();
        DisplayRandomFact();
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void InitializeQuestionPool()
    {
        questionPool = new List<int>[themesData.Count];
        for (int i = 0; i < themesData.Count; i++)
        {
            questionPool[i] = new List<int> { 0, 1, 2 };
        }
    }

    private void SetTheme()
    {
        themeText.text = currentThemeIndex == 0 ? "Ciência" : currentThemeIndex == 1 ? "Finanças" : "História";
    }

    private void DisplayRandomFact()
    {
        SetRandomFactIndex();
        factText.text = themesData[currentThemeIndex].facts[currentFactIndex];
        factPanel.SetActive(true);
        questionPanel.SetActive(false);
        RemoveFromQuestionPool();
    }

    private void SetRandomFactIndex()
    {
        currentFactIndex = questionPool[currentThemeIndex][UnityEngine.Random.Range(0, questionPool[currentThemeIndex].Count)];
    }

    private void RemoveFromQuestionPool()   
    {
        questionPool[currentThemeIndex].Remove(currentFactIndex);
        if (questionPool[currentThemeIndex].Count == 0)
            questionPool[currentThemeIndex] = new List<int> { 0, 1, 2 };
    }

    public void OnNextButtonClicked()
    {

       Debug.Log("OnNextButtonClicked was called!");
       // Sua lógica existente

        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = themesData[currentThemeIndex].questions[currentFactIndex];
        SetupAnswers();
        
    }

    private void SetupAnswers()
    {
        var correctAnswer = themesData[currentThemeIndex].correctAnswers[currentFactIndex];
        var incorrectAnswers = themesData[currentThemeIndex].incorrectAnswers[currentFactIndex];

        List<string> allAnswers = new List<string>(incorrectAnswers);
        int correctPosition = UnityEngine.Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctPosition, correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            bool isCorrect = i == correctPosition;
            answerButtons[i].onClick.AddListener(() => AnswerSelected(isCorrect));
        }
    }

    private void AnswerSelected(bool isCorrect)
    {
        audioSource.PlayOneShot(isCorrect ? correctSound : incorrectSound);
        if (isCorrect) currentThemeIndex = (currentThemeIndex + 1) % themesData.Count;
        ResetQuiz();
    }

    private void ResetQuiz()
    {
        factPanel.SetActive(true);
        questionPanel.SetActive(false);
        SetTheme();
        DisplayRandomFact();
    }
}
