using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class ButtonDailyReward : MonoBehaviour
{
    [Header("Settings Reward")]
    [SerializeField] private int _index;
    [SerializeField] private Sprite _spriteReward;
    [SerializeField] private int _plusMoney;

    [Space(10)]
    [Header("Component")]
    [Space(5)]
    [SerializeField] private Image panelTookTheReward;
    [SerializeField] private Image panelLock;
    [SerializeField] private Image iconReward;
    [Space(5)]
    [SerializeField] private Button buttonDay;
    [Space(5)]
    [SerializeField] private TMP_Text textPlusMoney;
    [SerializeField] private TMP_Text textDaysLeft;

    private Vector3 originalScaleButtonDay;

    public void Start()
    {
        originalScaleButtonDay = buttonDay.transform.localScale;

        panelLock.gameObject.SetActive(!YG2.saves.saveReward[_index]);
        iconReward.sprite = _spriteReward;
        textPlusMoney.text = $"+{_plusMoney}";
        if(YG2.lang == "en")
            textDaysLeft.text = $"Day {_index + 1}";
        else if(YG2.lang == "ru")
            textDaysLeft.text = $"День {_index + 1}";

        buttonDay.onClick.AddListener(ButtonReward);
        CheckPanelLock();
        CheckPanelTookTheReward();
    }

    private void OnValidate()
    {
        buttonDay = GetComponent<Button>();
        panelLock = transform.Find("PanelLock").GetComponent<Image>();
        textPlusMoney = transform.Find("IconReward/TextReward").GetComponent<TMP_Text>();
        textDaysLeft = transform.Find("PanelNumberDay/TextNumberDay").GetComponent<TMP_Text>();
        iconReward = transform.Find("IconReward").GetComponent<Image>();
        panelTookTheReward = transform.Find("PanelToolTheReward").GetComponent<Image>();
    }

    #region Button All

    private void ButtonReward()
    {
        AnimationManager.Instance.Activate_AnimationButtonScale(buttonDay, originalScaleButtonDay);

        if (YG2.saves.saveReward[_index])
        {
            Bootstrap.Instance.ServerTime.GiveReward(_plusMoney, _index);
        }
    }

    #endregion

    public void CheckPanelLock()
    {
        panelLock.gameObject.SetActive(!YG2.saves.saveReward[_index]);
    }

    public void CheckPanelTookTheReward()
    {
        panelTookTheReward.gameObject.SetActive(YG2.saves.saveTookTheReward[_index]);
    }
}
