using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MoneySliderLevel : MonoBehaviour
{
    [Header("Settings Slider")]
    [Space(5)]
    [SerializeField] public int _currentMoney = 0;
    [SerializeField] public int _maxMoney = 23;

    private Image imageSlider;
    private TMP_Text textMoneySlider;

    private Vector3 originalScaleTextMoney;

    void Start()
    {
        imageSlider = GameObject.Find("Slider").GetComponent<Image>();
        textMoneySlider = GameObject.Find("TextNumberMoney").GetComponent<TMP_Text>();

        originalScaleTextMoney = textMoneySlider.transform.localScale;

        if (imageSlider == null)
            Debug.LogError("imageSlider не назначен!");

        if (textMoneySlider == null)
            Debug.LogError("textMoneySlider не найден!");

        UpdateSlider();
    }

    public void UpdateSlider()
    {
        // Вычисляем заполнение в диапазоне от 0 до 1
        float fillAmount = (float)_currentMoney / _maxMoney;

        // Устанавливаем заполнение для Image
        imageSlider.fillAmount = fillAmount;

        TextSliderLevelUpdate();
    }

    private void TextSliderLevelUpdate()
    {
        textMoneySlider.text = _currentMoney + "/" + _maxMoney;
    }

    public void Activate_AnimationTextPlusMoney()
    {
        AnimationManager.Instance.Activate_AnimationScaleTextPlus(textMoneySlider, originalScaleTextMoney);
    }
}
