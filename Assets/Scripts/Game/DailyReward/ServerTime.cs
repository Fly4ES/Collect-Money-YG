using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using YG;

public class ServerTime : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_Text _timerText; // UI-текст для таймера
    [SerializeField] private GameObject _iconSelect;

    [Header("Buttons")]
    [SerializeField] private Button _buttonDailyRewardOn;
    [SerializeField] private Button _buttonDailyRewardOff;

    [Space(10)]
    [Header("Panel Daily Reward")]
    [SerializeField] private GameObject _panelDailyReward;
    [Space(5)]
    [SerializeField] public TMP_Text TextDailyReward;

    [Space(10)]
    [Header("Content")]
    [SerializeField] private ButtonDailyRewardAllContent buttonDailyRewardAllContent;

    [Space(10)]
    [Header("Animation")]
    [SerializeField] private AnimationImage _animPanelDailyReward;
    [SerializeField] private AnimationPanel _animBackground;

    private Vector3 originalSclBtnDailyRewardOn;
    private Vector3 originalSclBtnDailyRewardOff;

    private long lastClaimTime; // Последнее получение награды (в миллисекундах)
    private const long oneDayMilliseconds = 86400000; // 24 часа в миллисекундах
    private bool canClaim = false;

    public void Initialize()
    {
        if (YG2.saves.saveReward.Count == 0 || YG2.saves.saveTookTheReward.Count == 0)
        {
            for (int i = 0; i < buttonDailyRewardAllContent.ButtonDailyRewards.Count; i++)
            {
                YG2.saves.saveReward.Add(false);
                YG2.saves.saveTookTheReward.Add(false);
                YG2.SaveProgress();
            }
        }

        //for (int i = 0; i < buttonDailyRewardAllContent.ButtonDailyRewards.Count; i++)
          //  buttonDailyRewardAllContent.ButtonDailyRewards[i].Initialize();

        _panelDailyReward.SetActive(false);

        #region Button Original Scale All

        originalSclBtnDailyRewardOn = _buttonDailyRewardOn.transform.localScale;
        originalSclBtnDailyRewardOff = _buttonDailyRewardOff.transform.localScale;

        #endregion

        #region Button

        _buttonDailyRewardOn.onClick.AddListener(ButtonDailyRewardOn);
        _buttonDailyRewardOff.onClick.AddListener(ButtonDailyRewardOff);

        #endregion

        CheckDailyReward();
    }

    private void OnValidate()
    {
        TextDailyReward = _panelDailyReward.transform.Find("PanelDailyReward/TextDailyReward").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!canClaim)
            UpdateTimer();
    }

    #region Button All

    private void ButtonDailyRewardOn()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonDailyRewardOn, originalSclBtnDailyRewardOn);

        _panelDailyReward.SetActive(true);

        _animBackground.AnimationPanelUpdate(0.5f, 0.85f, 0f);
        _animPanelDailyReward.AnimationImageUpdate();
    }

    private void ButtonDailyRewardOff()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(_buttonDailyRewardOff, originalSclBtnDailyRewardOff);

        StartCoroutine(TimeDailyRewardOff());
    }

    IEnumerator TimeDailyRewardOff()
    {
        yield return new WaitForSeconds(0.1f);
        _panelDailyReward.SetActive(false);
    }

    #endregion

    #region Text All

    public void TextDailyRewardUpdate(bool reward)
    {
        if(reward)
        {
            if(YG2.lang == "en")
                TextDailyReward.text = "Get your reward!";
            else if(YG2.lang == "ru")
                TextDailyReward.text = "Получите свою награду!";
            TextDailyReward.color = Color.yellow;
        }
        else
        {
            if (YG2.lang == "en")
                TextDailyReward.text = "Can't pick it up yet...";
            else if (YG2.lang == "ru")
                TextDailyReward.text = "Пока не можешь забрать...";
            TextDailyReward.color = Color.red;
        }
    }

    #endregion

    // Проверяем, можно ли забрать награду
    private void CheckDailyReward()
    {
        lastClaimTime = YG2.saves.serverTime; // Загружаем сохраненное время
        long currentTime = YG2.ServerTime(); // Получаем текущее серверное время

        long timePassed = currentTime - lastClaimTime;

        if (lastClaimTime == 0 || timePassed >= oneDayMilliseconds) // Прошло 24 часа?
        {
            _timerText.gameObject.SetActive(false);
            _iconSelect.SetActive(true);

            canClaim = true;

            Debug.Log("🎁 Можно получить награду!");

            if(!YG2.saves.hasRewardSaved)
            {
                for (int i = 0; i < YG2.saves.saveReward.Count; i++)
                {
                    if (!YG2.saves.saveReward[i])
                    {
                        YG2.saves.saveReward[i] = true;
                        YG2.saves.hasRewardSaved = true;
                        YG2.SaveProgress();
                        break;
                    }
                }
            }

            TextDailyRewardUpdate(true);
        }
        else
        {
            _timerText.gameObject.SetActive(true);
            _iconSelect.SetActive(false);

            canClaim = false;

            Debug.Log("⏳ Еще нельзя забрать награду");
            YG2.saves.hasRewardSaved = false;
            YG2.SaveProgress();
            TextDailyRewardUpdate(false);
        }
    }

    // Выдаем награду
    public void GiveReward(int moneyPlus, int index)
    {
        if (canClaim) // Проверяем, можно ли забрать награду
        {
            _timerText.gameObject.SetActive(true);
            _iconSelect.SetActive(false);

            Debug.Log("✅ Награда выдана!");

            lastClaimTime = YG2.ServerTime(); // Запоминаем момент получения награды
            YG2.saves.serverTime = lastClaimTime; // Сохраняем в Yandex Game
            MoneyManager.Instance.AddMoney(moneyPlus);
            YG2.saves.hasRewardSaved = false;
            YG2.saves.saveTookTheReward[index] = true;
            YG2.SaveProgress(); // Сохраняем прогресс

            for (int i = 0; i < ButtonDailyRewardAllContent.Instance.ButtonDailyRewards.Count; i++)
                ButtonDailyRewardAllContent.Instance.ButtonDailyRewards[i].CheckPanelTookTheReward();

            canClaim = false; // Теперь награду взять нельзя

            TextDailyRewardUpdate(false);
        }
    }

    // Обновляем таймер до следующей награды
    void UpdateTimer()
    {
        long currentTime = YG2.ServerTime();
        long timeLeft = (lastClaimTime + oneDayMilliseconds) - currentTime;

        if (timeLeft > 0)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(timeLeft);
            _timerText.text = $"{time.Hours:D2}:{time.Minutes:D2}";
        }
        else
        {
            //canClaim = true;
            //TextDailyReward.text = "Get your reward!";
            //TextDailyReward.color = Color.yellow;
            ////_timerText.text = "Награда доступна!";

            CheckDailyReward();
        }
    }
}
