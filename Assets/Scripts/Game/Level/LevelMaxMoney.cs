using UnityEngine;

public class LevelMaxMoney : MonoBehaviour
{
    [Header("Settings Level")]
    [Space(5)]
    [SerializeField] private int _maxMoneyLevel;
    [Space(10)]
    [SerializeField] private MoneySliderLevel _moneySliderLevel;

    private void Start() => _moneySliderLevel._maxMoney = _maxMoneyLevel;
}
