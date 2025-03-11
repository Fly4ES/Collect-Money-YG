using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [Header("Animation Button Scale")]
    [SerializeField] private float _scaleFactorButton = 0.8f;
    [SerializeField] private float _durationScaleButton = 0.1f;
    [Space(10)]
    [Header("Animation Text Plus Scale")]
    [SerializeField] private float _scaleFactorTextPlus = 1.3f;
    [SerializeField] private float _durationScaleTextPlus = 0.1f;

    private void Start() => Instance = this;

    public void Activate_AnimationButtonScale(Button button, Vector3 originalScaleButton)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(button.transform.DOScale(originalScaleButton * _scaleFactorButton, _durationScaleButton) // Уменьшение
            .SetEase(Ease.OutQuad))
            .Append(button.transform.DOScale(originalScaleButton, _durationScaleButton) // Возврат
            .SetEase(Ease.OutQuad));
    }

    public void Activate_AnimationScaleTextPlus(TMP_Text text, Vector3 orginalScaleText)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(text.transform.DOScale(orginalScaleText * _scaleFactorTextPlus, _durationScaleTextPlus) // Уменьшение
            .SetEase(Ease.OutQuad))
            .Append(text.transform.DOScale(orginalScaleText, _durationScaleTextPlus) // Возврат
            .SetEase(Ease.OutQuad));
    }
}
