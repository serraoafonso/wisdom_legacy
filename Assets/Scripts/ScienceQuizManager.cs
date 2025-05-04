using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class ScienceQuizManager : MonoBehaviour
{
    public TextMeshProUGUI factText;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public GameObject factPanel;
    public GameObject questionPanel;
    public TextMeshProUGUI themeText;
    public TextMeshProUGUI sciencePointsText;
    public GameObject Canva;

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement;
    public GameObject book1;
    public GameObject book2;
    public GameObject book3;
    public GameObject book4;
    public GameObject player;
    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] scienceFactsPT = {
        "A gravidade é a força que atrai objetos uns aos outros e é responsável por manter planetas em órbita ao redor do Sol.",
        "A camada de ozônio na estratosfera protege a Terra dos raios ultravioletas prejudiciais, absorvendo a maior parte dessa radiação.",
        "A teoria das placas tectônicas sugere que a crosta terrestre é dividida em placas que se movem, causando fenômenos como terremotos e a formação de montanhas.",
        "As células são as unidades básicas de vida e compõem todos os organismos vivos, sejam eles unicelulares ou multicelulares.",
        "A evolução é o processo pelo qual diferentes espécies se desenvolvem e se diversificam ao longo do tempo, principalmente através da seleção natural.",
        "A fotossíntese é o processo pelo qual plantas e outros organismos convertem luz solar, água e dióxido de carbono em oxigênio e glicose, essenciais para a vida na Terra.",
        "O DNA é o material genético encontrado em células, que contém as instruções para o desenvolvimento e funcionamento de todos os organismos vivos.",
        "A água cobre cerca de 71% da superfície da Terra e é essencial para a sobrevivência de todos os seres vivos.",
        "Os buracos negros são regiões no espaço onde a gravidade é tão forte que nem mesmo a luz pode escapar.",
        "Os vírus são organismos microscópicos que dependem de hospedeiros vivos para se replicar, causando diversas doenças.",
        "O efeito estufa é um fenômeno natural que mantém a Terra aquecida, mas sua intensificação pode levar a mudanças climáticas."
    };

    private string[] scienceFactsEN = {
        "Gravity is the force that attracts objects toward one another and is responsible for keeping planets in orbit around the Sun.",
        "The ozone layer in the stratosphere protects Earth from harmful ultraviolet rays by absorbing most of this radiation.",
        "The theory of plate tectonics suggests that Earth's crust is divided into moving plates, causing phenomena like earthquakes and mountain formation.",
        "Cells are the basic units of life and make up all living organisms, whether unicellular or multicellular.",
        "Evolution is the process through which different species develop and diversify over time, mainly through natural selection.",
        "Photosynthesis is the process by which plants and other organisms convert sunlight, water, and carbon dioxide into oxygen and glucose, essential for life on Earth.",
        "DNA is the genetic material found in cells that contains instructions for the development and functioning of all living organisms.",
        "Water covers about 71% of Earth's surface and is essential for the survival of all living beings.",
        "Black holes are regions in space where gravity is so strong that not even light can escape.",
        "Viruses are microscopic organisms that depend on living hosts to replicate, causing various diseases.",
        "The greenhouse effect is a natural phenomenon that keeps Earth warm, but its intensification can lead to climate change."
    };

    private string[] scienceQuestionsPT = {
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

    private string[] scienceQuestionsEN = {
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

    private string[] correctAnswersPT = {
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

    private string[] correctAnswersEN = {
        "It is the force that attracts objects and keeps planets in orbit around the Sun.",
        "To absorb most of the harmful ultraviolet radiation, protecting Earth.",
        "That Earth's crust is divided into moving plates, causing earthquakes and mountain formation.",
        "They serve as basic units of life, allowing organisms to form and function.",
        "It is the process of species diversification through natural selection over time.",
        "It allows plants to produce energy and oxygen, essential for life on Earth.",
        "It contains genetic instructions that regulate the development and functioning of living organisms.",
        "It is essential for biological processes like cellular hydration and nutrient transport.",
        "They are regions with gravity so strong that not even light can escape, distorting space around them.",
        "They depend on living hosts to replicate, causing diseases by invading host cells.",
        "It is a phenomenon that keeps Earth warm, but its intensification can lead to climate change."
    };

    private string[][] incorrectAnswersPT = {
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

    private string[][] incorrectAnswersEN = {
        new string[] { "It only affects large objects like planets and stars.", "It repels objects away from each other in space." },
        new string[] { "It reflects solar radiation into space, regulating climate.", "It reduces the amount of oxygen in the atmosphere." },
        new string[] { "That continents are immobile and fixed on Earth's crust.", "That Earth's surface is completely solid and unbreakable." },
        new string[] { "They only store genetic information without any function.", "They act only in reproduction of organisms." },
        new string[] { "It happens during rainy days and influences animal behavior.", "A biological cycle that only affects animals." },
        new string[] { "It only produces glucose but has no effect on oxygen.", "It is just a light absorption process with no other function." },
        new string[] { "It is a molecule that only serves as an energy source for cells.", "It acts only in the reproduction of multicellular organisms." },
        new string[] { "It is only important for forming oceans and rivers.", "Only aquatic organisms need it to survive." },
        new string[] { "They are collapsing stars that shine intensely.", "They are holes that release light and matter into space." },
        new string[] { "They replicate on their own without host cells.", "They only act beneficially to living organisms." },
        new string[] { "It increases the amount of oxygen in the atmosphere.", "It has no direct impact on Earth's temperature." }
    };

    private void Start()
    {
        audioSource.clip = null;
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes();
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];

        themeText.text = GameData.language == "en" ? "Science" : "Ciência";
        factText.text = GameData.language == "en" ? scienceFactsEN[currentFactIndex] : scienceFactsPT[currentFactIndex];

        factIndexes.Remove(currentFactIndex);

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = GameData.language == "en" ? scienceQuestionsEN[currentFactIndex] : scienceQuestionsPT[currentFactIndex];
        SetupAnswers();
    }

    private void SetupAnswers()
    {
        string correctAnswer = GameData.language == "en" ? correctAnswersEN[currentFactIndex] : correctAnswersPT[currentFactIndex];
        string[] incorrects = GameData.language == "en" ? incorrectAnswersEN[currentFactIndex] : incorrectAnswersPT[currentFactIndex];

        List<string> allAnswers = new List<string>(incorrects);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < allAnswers.Count)
            {
                int buttonIndex = i;
                answerButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[buttonIndex];
                answerButtons[buttonIndex].onClick.RemoveAllListeners();

                bool isCorrect = (buttonIndex == correctAnswerPosition);
                answerButtons[buttonIndex].onClick.AddListener(() => AnswerSelected(isCorrect, answerButtons[buttonIndex].gameObject));
                answerButtons[buttonIndex].gameObject.SetActive(true);
            }
            else
            {
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
            botao.GetComponent<Image>().color = Color.green;
            Debug.Log("Resposta correta!");
            audioSource.clip = correctSound;

            if (targetBook != null) DisableBookCollider(targetBook);

            GameData.sciencePoints++;
            sciencePointsText.text = GameData.sciencePoints.ToString();

            StartCoroutine(HandleAnswerFeedback(true, botao));
        }
        else
        {
            botao.GetComponent<Image>().color = Color.red;
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;

            GameData.sciencePoints = Mathf.Max(GameData.sciencePoints - 1, 0);
            sciencePointsText.text = GameData.sciencePoints.ToString();

            if (player != null)
            {
                player.transform.position = playerPosition.y < 10
                    ? new Vector3(-39, 9.531775f, player.transform.position.z)
                    : new Vector3(-130, 79, player.transform.position.z);
            }

            StartCoroutine(HandleAnswerFeedback(false, botao));
        }

        audioSource.Play();
    }

    private IEnumerator HandleAnswerFeedback(bool isCorrect, GameObject botao)
    {
        yield return new WaitForSeconds(0.2f);
        botao.GetComponent<Image>().color = Color.white;
        Canva.SetActive(false);
        playerMovement.collidedStop = false;
        DisplayRandomFact();
    }

    private GameObject DetermineBookBasedOnPlayerPosition()
    {
        Vector3 pos = player.transform.position;

        if (pos.y < 10) return book1;
        if (pos.x <= -100) return book2;
        if (pos.x <= -80) return book3;
        return book4;
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book == null) return;

        BoxCollider2D collider = book.GetComponent<BoxCollider2D>();
        Rigidbody2D rb = book.GetComponent<Rigidbody2D>();

        if (collider != null) collider.enabled = false;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 10;
        }

        Destroy(book, 5);
    }
}
