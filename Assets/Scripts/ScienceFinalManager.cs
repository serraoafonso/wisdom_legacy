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
    public TextMeshProUGUI sciencePointsText;

    public GameObject finalCanvas; // Canvas final
    public GameObject scienceBook; // Objeto associado ao quiz (ex.: livro de ci�ncia)
    public PlayerMovement playerMovement; // Refer�ncia ao script de movimento do jogador

    private int correctAnswersCount; // Contador de respostas corretas seguidas
    private int currentQuestionIndex;
    private List<int> questionIndexes;

    // Dados de perguntas e respostas de ci�ncia
    private string language;

    // Perguntas e respostas em portugu�s
    private string[] questionsPT = {
        "O que � a gravidade e qual sua fun��o no sistema solar?",
        "Qual � a fun��o da camada de oz�nio na atmosfera terrestre?",
        "Qual � a teoria das placas tect�nicas e quais fen�menos ela ajuda a explicar?",
        "Qual � a fun��o das c�lulas nos organismos vivos?",
        "Como funciona o processo de evolu��o?",
        "Para que serve a fotoss�ntese nas plantas?",
        "O que � o DNA e qual a sua fun��o nos organismos vivos?",
        "Por que a �gua � essencial para a vida na Terra?",
        "O que s�o buracos negros e quais s�o suas caracter�sticas?",
        "Como os v�rus se replicam e qual seu impacto na sa�de?",
        "O que � o efeito estufa e como ele influencia o clima da Terra?"
    };

    private string[] correctAnswersPT = {
        "� a for�a que atrai os objetos uns aos outros e mant�m os planetas em �rbita ao redor do Sol.",
        "Absorver a maior parte da radia��o ultravioleta prejudicial, protegendo a Terra.",
        "Que a crosta terrestre � dividida em placas que se movem, causando terremotos e a forma��o de montanhas.",
        "Servem como unidades b�sicas de vida, permitindo a forma��o e o funcionamento dos organismos.",
        "� o processo de diversifica��o das esp�cies atrav�s da sele��o natural ao longo do tempo.",
        "Permite que as plantas produzam energia e oxig�nio, essenciais para a vida na Terra.",
        "Cont�m as instru��es gen�ticas que regulam o desenvolvimento e o funcionamento dos organismos vivos.",
        "� essencial para processos biol�gicos como hidrata��o celular e transporte de nutrientes.",
        "S�o regi�es com gravidade t�o forte que nem a luz pode escapar, deformando o espa�o ao seu redor.",
        "Dependem de hospedeiros vivos para se replicar, causando doen�as ao invadir c�lulas hospedeiras.",
        "� um fen�meno que mant�m a Terra aquecida, mas sua intensifica��o pode levar a mudan�as clim�ticas."
    };

    private string[][] incorrectAnswersPT = new string[][] {
        new string[] { "� uma for�a que apenas afeta objetos grandes como planetas e estrelas.", "� uma for�a que repele objetos uns dos outros no espa�o." },
        new string[] { "Reflete a radia��o solar para o espa�o, regulando o clima.", "Diminui a quantidade de oxig�nio na atmosfera." },
        new string[] { "Que os continentes s�o im�veis e fixos na crosta terrestre.", "Que a superf�cie terrestre � completamente s�lida e inquebr�vel." },
        new string[] { "Somente armazenam informa��es gen�ticas sem realizar nenhuma fun��o.", "Atuam apenas na reprodu��o dos organismos." },
        new string[] { "� um processo que ocorre em dias de chuva e influencia o comportamento animal.", "Um ciclo biol�gico que apenas afeta os animais." },
        new string[] { "Produz apenas glicose, mas n�o tem efeito no oxig�nio.", "� apenas um processo de absor��o de luz solar sem outra fun��o." },
        new string[] { "� uma mol�cula que serve apenas como fonte de energia para c�lulas.", "Atua exclusivamente na reprodu��o de organismos multicelulares." },
        new string[] { "� importante apenas para a forma��o de oceanos e rios.", "Apenas organismos aqu�ticos precisam dela para sobreviver." },
        new string[] { "S�o estrelas em colapso que brilham intensamente.", "S�o buracos que liberam luz e mat�ria no espa�o." },
        new string[] { "Se replicam sozinhos sem precisar de c�lulas hospedeiras.", "Atuam apenas de forma ben�fica para os organismos vivos." },
        new string[] { "Aumenta a quantidade de oxig�nio na atmosfera.", "N�o tem impacto direto na temperatura da Terra." }
    };

    // Perguntas e respostas em ingl�s
    private string[] questionsEN = {
        "What is gravity and what is its function in the solar system?",
        "What is the function of the ozone layer in Earth's atmosphere?",
        "What is the theory of plate tectonics and what phenomena does it help explain?",
        "What is the function of cells in living organisms?",
        "How does the process of evolution work?",
        "What is the purpose of photosynthesis in plants?",
        "What is DNA and what is its function in living organisms?",
        "Why is water essential for life on Earth?",
        "What are black holes and what are their characteristics?",
        "How do viruses replicate and what is their impact on health?",
        "What is the greenhouse effect and how does it influence Earth's climate?"
    };

    private string[] correctAnswersEN = {
        "It is the force that attracts objects to each other and keeps planets in orbit around the Sun.",
        "It absorbs most of the harmful ultraviolet radiation, protecting Earth.",
        "That the Earth's crust is divided into plates that move, causing earthquakes and the formation of mountains.",
        "They serve as the basic units of life, allowing for the formation and functioning of organisms.",
        "It is the process of species diversification through natural selection over time.",
        "It allows plants to produce energy and oxygen, which are essential for life on Earth.",
        "It contains the genetic instructions that regulate the development and functioning of living organisms.",
        "It is essential for biological processes such as cellular hydration and nutrient transport.",
        "They are regions with such strong gravity that even light cannot escape, distorting space around them.",
        "They depend on living hosts to replicate, causing diseases by invading host cells.",
        "It is a phenomenon that keeps Earth warm, but its intensification can lead to climate changes."
    };

    private string[][] incorrectAnswersEN = new string[][] {
        new string[] { "It is a force that only affects large objects like planets and stars.", "It is a force that repels objects from each other in space." },
        new string[] { "It reflects solar radiation into space, regulating the climate.", "It decreases the amount of oxygen in the atmosphere." },
        new string[] { "That continents are immobile and fixed on the Earth's crust.", "That the Earth's surface is completely solid and unbreakable." },
        new string[] { "They only store genetic information without performing any function.", "They only act in the reproduction of organisms." },
        new string[] { "It is a process that occurs on rainy days and influences animal behavior.", "A biological cycle that only affects animals." },
        new string[] { "It only produces glucose, but has no effect on oxygen.", "It is just a process of absorbing sunlight with no other function." },
        new string[] { "It is a molecule that only serves as an energy source for cells.", "It acts exclusively in the reproduction of multicellular organisms." },
        new string[] { "It is important only for the formation of oceans and rivers.", "Only aquatic organisms need it to survive." },
        new string[] { "They are collapsing stars that shine intensely.", "They are holes that emit light and matter into space." },
        new string[] { "They replicate by themselves without needing host cells.", "They only act in a beneficial way for living organisms." },
        new string[] { "It increases the amount of oxygen in the atmosphere.", "It has no direct impact on Earth's temperature." }
    };

    private void Start()
    {
        // Assume gameData.language is set somewhere in the game
        language = GameData.language;
        StartQuiz();
    }

    private void StartQuiz()
    {
        // Inicializa o quiz com todas as perguntas
        questionIndexes = new List<int>();
        for (int i = 0; i < GetQuestions().Length; i++)
        {
            questionIndexes.Add(i);
        }
        ShuffleQuestions();
        correctAnswersCount = 0; // Reseta contador de respostas corretas seguidas
        LoadQuestion();

        // Desativa o movimento do jogador enquanto o quiz est� ativo
        if (playerMovement != null)
        {
            playerMovement.collidedStop = true;
        }
    }

    private string[] GetQuestions()
    {
        return language == "en" ? questionsEN : questionsPT;
    }

    private string[] GetCorrectAnswers()
    {
        return language == "en" ? correctAnswersEN : correctAnswersPT;
    }

    private string[][] GetIncorrectAnswers()
    {
        return language == "en" ? incorrectAnswersEN : incorrectAnswersPT;
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

        questionText.text = GetQuestions()[currentQuestionIndex];
        List<string> answerOptions = new List<string>(GetIncorrectAnswers()[currentQuestionIndex]);
        answerOptions.Add(GetCorrectAnswers()[currentQuestionIndex]);
        ShuffleList(answerOptions); // M�todo para embaralhar respostas

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i];
            answerButtons[i].onClick.RemoveAllListeners();
            bool isCorrect = answerOptions[i] == GetCorrectAnswers()[currentQuestionIndex];
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
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic; // Define o corpo como din�mico
                bookRigidbody.gravityScale = 10;
            }

            Destroy(book, 5); // Destroi o objeto ap�s 5 segundos
        }
    }
}
