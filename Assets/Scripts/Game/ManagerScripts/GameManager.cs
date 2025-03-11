using UnityEngine;
using UnityEngine.UI;
using YG;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Animation Background Black")]
    [SerializeField] private Image _animBackgroundBlack;
    [Space(5)]
    [SerializeField] private float _durationAnimBackgroundBlack = 1f;

    [Header("Pause Button")]
    [Space(5)]
    [SerializeField] private Button _buttonPause;
    [SerializeField] private Button _buttonExitePause;
    [SerializeField] private Button _buttonGoToMenu_Pause;
    [Space(10)]
    [Header("Next Level Button")]
    [SerializeField] private Button _buttonNoThanks;
    [Space(10)]
    [Header("Game Over Button")]
    [SerializeField] private Button _buttonGoToMenu_GameOver;
    [SerializeField] private Button _buttonSkipLevel;

    [Space(10)]
    [Header("Panel All")]
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelGame;
    [SerializeField] private GameObject _panelPause;
    [SerializeField] private GameObject _panelNextLevel;
    [SerializeField] private GameObject _panelGameOver;
    [Space(5)]
    [SerializeField] private GameObject _panelMoney;

    [Space(10)]
    [Header("Text All")]
    [SerializeField] private TMP_Text _textLevel;

    [Space(10)]
    [Header("Settings Next Level")]
    [SerializeField] private GameObject _particleImageCoin;
    [SerializeField] private GameObject _particleConfettiFullScreen;
    [Space(5)]
    [Tooltip("Введите сумму, которую может получить игрок при прохождении уровня")]
    [SerializeField] private int _moneyNextLevel = 20;

    // Pause
    private Vector3 originalScaleButtonPause;

    // Next Level
    private Vector3 originalScaleButtonNoThanks;

    // Game Over
    private Vector3 originalScaleButtonMenu_GameOver;
    private Vector3 originalScaleButtonSkipLevel;

    private MoneySliderLevel moneySliderLevel;
    private MoneyAll moneyAll;
    private AnimationImage animExitePause;
    private AnimationImage animGoToMenu;

    public void Initialize()
    {
        moneyAll = FindObjectOfType<MoneyAll>();
        animGoToMenu = _buttonGoToMenu_Pause.GetComponent<AnimationImage>();
        animExitePause = _buttonExitePause.GetComponent<AnimationImage>();

        #region Button Original Scale All

        // Pause
        originalScaleButtonPause = _buttonPause.transform.localScale;

        // Next Level
        originalScaleButtonNoThanks = _buttonNoThanks.transform.localScale;

        // Game Over
        originalScaleButtonMenu_GameOver = _buttonGoToMenu_GameOver.transform.localScale;
        originalScaleButtonSkipLevel = _buttonSkipLevel.transform.localScale;

        #endregion

        #region List Button All

        // Pause
        _buttonPause.onClick.AddListener(ButtonPause);
        _buttonExitePause.onClick.AddListener(ButtonExitePause);
        _buttonGoToMenu_Pause.onClick.AddListener(ButtonGoToMenu);

        // Next Level
        _buttonNoThanks.onClick.AddListener(ButtonNoThanks);

        // Game Over
        _buttonSkipLevel.onClick.AddListener(ButtonSkipLevel);
        _buttonGoToMenu_GameOver.onClick.AddListener(ButtonMenu_GameOver);

        #endregion

        TextLevelUpdate();
        AnimationBackgroundBlackOn();
    }

    #region Button All

    #region Pause

    private void ButtonPause()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonPause, originalScaleButtonPause);

        _panelPause.SetActive(true);

        Time.timeScale = 0;

        animExitePause.AnimationImageUpdate();
        animExitePause.startAnimation = false;
        animGoToMenu.AnimationImageUpdate();
        animGoToMenu.startAnimation = false;
    }

    private void ButtonExitePause()
    {
        _panelPause.SetActive(false);

        Time.timeScale = 1;
    }

    private void ButtonGoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    #endregion

    #region GameOver

    private void ButtonMenu_GameOver()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonGoToMenu_GameOver, originalScaleButtonMenu_GameOver);

        YG2.InterstitialAdvShow();

        AnimationBackgroundBlackOff();
        StartCoroutine(TimeMenu_GameOver());
    }

    IEnumerator TimeMenu_GameOver()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("Game");
    }

    private void ButtonSkipLevel()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonSkipLevel, originalScaleButtonSkipLevel);
    }

    #endregion

    #region Next Level

    private void ButtonNoThanks()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonNoThanks, originalScaleButtonNoThanks);

        YG2.InterstitialAdvShow();

        StartCoroutine(TimeNextLevel());
    }

    IEnumerator TimeNextLevel()
    {
        _particleImageCoin.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        MoneyManager.Instance.AddMoney(_moneyNextLevel);

        yield return new WaitForSeconds(1f);
        AnimationBackgroundBlackOff();

        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("Game");

        YG2.saves.level++;
        YG2.saves.levelText++;
        YG2.SaveProgress();
    }

    #endregion

    #endregion

    #region Text All

    private void TextLevelUpdate()
    {
        if (YG2.lang == "en")
            _textLevel.text = "Level: " + YG2.saves.levelText;
        else if(YG2.lang == "ru")
            _textLevel.text = "Уровень: " + YG2.saves.levelText;
    }

    #endregion

    #region Animation All

    private void AnimationBackgroundBlackOn()
    {
        _animBackgroundBlack.gameObject.SetActive(true);

        Color startColor = _animBackgroundBlack.color;
        startColor.a = 1;
        _animBackgroundBlack.color = startColor;

        _animBackgroundBlack.DOFade(0, _durationAnimBackgroundBlack);

        StartCoroutine(AnimationBackgroundBlackObjectOff());
    }

    IEnumerator AnimationBackgroundBlackObjectOff()
    {
        yield return new WaitForSeconds(1.5f);
        _animBackgroundBlack.gameObject.SetActive(false);
    }

    private void AnimationBackgroundBlackOff()
    {
        _animBackgroundBlack.gameObject.SetActive(true);

        Color startColor = _animBackgroundBlack.color;
        startColor.a = 0;
        _animBackgroundBlack.color = startColor;

        _animBackgroundBlack.DOFade(1, _durationAnimBackgroundBlack);
    }

    #endregion

    #region Next Level

    private void OpenPanelNextLevel()
    {
        _panelMoney.SetActive(true);
        _panelNextLevel.SetActive(true);

        StartCoroutine(NextLevelStart());
    }

    IEnumerator NextLevelStart()
    {
        yield return new WaitForSeconds(0.5f);
        _particleConfettiFullScreen.SetActive(true);
    }

    #endregion

    #region GameOver

    private void OpenGameOver()
    {
        _panelGameOver.SetActive(true);
    }

    #endregion

    public void CheckNextLevelAndGameOver()
    {
        if (moneySliderLevel == null)
        {
            moneySliderLevel = GameObject.Find("SliderBackground").GetComponent<MoneySliderLevel>();

            if (moneySliderLevel._currentMoney == moneySliderLevel._maxMoney)
                OpenPanelNextLevel();
            else if (moneyAll.Money_All <= 0 && moneySliderLevel._currentMoney < moneySliderLevel._maxMoney)
                OpenGameOver();
        }
        else
        {
            if (moneySliderLevel._currentMoney == moneySliderLevel._maxMoney)
                OpenPanelNextLevel();
            else if (moneyAll.Money_All <= 0 && moneySliderLevel._currentMoney < moneySliderLevel._maxMoney)
                OpenGameOver();
        }
    }
}
