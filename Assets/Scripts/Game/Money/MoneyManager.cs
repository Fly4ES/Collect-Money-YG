using UnityEngine;
using TMPro;
using YG;
using DG.Tweening;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private TMP_Text textMoney;

    private void Start()
    {
        Instance = this;

        textMoney = GetComponentInChildren<TMP_Text>();

        TextMoneyUpdate();
    }

    public void TextMoneyUpdate()
    {
        textMoney.text = FormatMoney(YG2.saves.money);
    }

    public void AddMoney(int money)
    {
        int startValue = YG2.saves.money;
        int targetValue = YG2.saves.money + money;
        YG2.saves.money += money;
        YG2.SaveProgress();

        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            textMoney.text = FormatMoney(startValue);
        }, targetValue, 1f).SetEase(Ease.OutQuad);

        // В конце можно дополнительно обновить текст, чтобы избежать мелких рассинхронов
        TextMoneyUpdate();
    }

    private string FormatMoney(int value)
    {
        if (value >= 1000000)
        {
            return (value / 1000000f).ToString("0.#") + "M";
        }
        else if (value >= 1000)
        {
            return (value / 1000f).ToString("0.#") + "K";
        }
        else
        {
            return value.ToString();
        }
    }
}
