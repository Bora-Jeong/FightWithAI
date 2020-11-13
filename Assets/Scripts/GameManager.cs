using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public int bgIndex; // 배경 사진 몇번째
    public string content;
}

public enum Ingredient
{
    TopBread,
    Tomato,
    Cabbage,
    Cheese,
    Patty,
    BottomBread
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] DialoguePanel _dialoguePanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] Sprite[] _ingredientSprites;

    [Header("Prologue")]
    [SerializeField] Sprite[] _prologueBG;
    [SerializeField] Dialogue[] _dialogues;

    [Header("Game")]
    [SerializeField] Text _dayText;
    [SerializeField] Slider _timeSlider;
    [SerializeField] Text _timeText;
    [SerializeField] Text _playerScoreText;
    [SerializeField] Text _aiScoreText;

    [SerializeField] Transform _recipeRoot;
    [SerializeField] Transform _aiRecipeRoot;
    [SerializeField] GameObject _recipe;

    private int _day;
    private float _totalTime;
    private float _remainTime;
    private int _playerScore;
    private int _aiScore;

    private Queue<Hamburger> _recipeQ = new Queue<Hamburger>(); // 플레이어가 만들어야 하는 주문서들

    public Hamburger playerHamburger;

    private Queue<Hamburger> _aiRecipeQ = new Queue<Hamburger>(); // AI의 주문서들

    public bool isPlaying { get; private set; } // 게임 중?
    public int playerScore
    {
        get => _playerScore;
        set
        {
            _playerScore = value;
            _playerScoreText.text = $"Score {_playerScore}";
        }
    }

    public int aiScore
    {
        get => _aiScore;
        set
        {
            _aiScore = value;
            _aiScoreText.text = $"Score {_aiScore}";
        }
    }

    public float remainTime
    {
        get => _remainTime;
        set
        {
            _remainTime = value;
            _timeText.text = $"{_remainTime}초";
            _timeSlider.value = _remainTime / _totalTime;
        }
    }

    private void Awake()
    {
        //StartCoroutine(StartPrologue());

        // 임시코드 프롤로그 시작 안하고 바로 게임 시작
        _dialoguePanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);
        RoundStart(1, 60f);
        // 임시 코드
    }

    IEnumerator StartPrologue()  // 프롤로그 시작
    {
        _gamePanel.gameObject.SetActive(false);
        _dialoguePanel.gameObject.SetActive(true);
        for (int i = 0; i < _dialogues.Length; i++)
        {
            _dialoguePanel.Set(_prologueBG[_dialogues[i].bgIndex], _dialogues[i].content);
            yield return new WaitForSeconds(2f);
        }

        _dialoguePanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);
        RoundStart(1, 60f);
    }

    private void RoundStart(int day, float time) // 라운드 시작
    {
        _day = day;
        _dayText.text = $"{_day}일차";
        _totalTime = time;
        remainTime = _totalTime;
        playerScore = 0;
        aiScore = 0;
        _recipeQ.Clear();

        isPlaying = true;
        RefreshRecipe();
        StartCoroutine(GameSchedulling());
    }

    IEnumerator GameSchedulling()
    {
        while(remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            yield return null;
        }

        GameOver();
    }

    private void GameOver() // 게임 오버
    {
        isPlaying = false;
    }

    private void RefreshRecipe()
    {
        for(int i = 0; i < 30; i++) // 라운드 시작시 일단 6개 레시피 로드해놓음
        {
            Hamburger hamburger = GetRandomHamburger();
            GameObject recipe = Instantiate(_recipe, _recipeRoot);
            hamburger.transform.SetParent(recipe.transform);
            hamburger.transform.localPosition = new Vector3(0,  -30, 0);
            GameObject copy = Instantiate(recipe, _aiRecipeRoot);
            copy.transform.localScale = Vector3.one * 0.6f;
            copy.transform.localPosition = new Vector3(0, -30, 0);
            _recipeQ.Enqueue(hamburger);
            _aiRecipeQ.Enqueue(hamburger);
        }
    }

    public bool OnServingButton() // 플레이어가 만든 햄버거가 맞게 만들었는지
    {
        Queue<Ingredient> player = playerHamburger.ingredients;
        Hamburger recipe = _recipeQ.Dequeue(); // 레시피 제일 앞 버거

        bool success = true;
        while(player.Count > 0 && recipe.ingredients.Count > 0)
        {
            if(player.Dequeue() != recipe.ingredients.Dequeue())
            {
                success = false;
                break;
            }
        }

        if (player.Count > 0 || recipe.ingredients.Count > 0)
            success = false;

        playerHamburger.ingredients.Clear();

        Destroy(recipe.transform.parent.gameObject);
        // 새로 레시피 추가

        return success;
    }


    private Hamburger GetRandomHamburger()
    {
        Hamburger hamburger = new GameObject("Hamburger").AddComponent<Hamburger>();
        int count = Random.Range(_day + 1, _day + 3); // 1일차 최소 2 , 최대 3개
        hamburger.StackIngredientUI(Ingredient.BottomBread);
        for (int i = 0; i < count; i++)
            hamburger.StackIngredientUI((Ingredient)Random.Range(1, 5));
        hamburger.StackIngredientUI(Ingredient.TopBread);
        return hamburger;
    }

    public Sprite GetIngredientSprite(Ingredient ingredient) => _ingredientSprites[(int)ingredient]; // 재료 사진 얻는 함수!!!
}