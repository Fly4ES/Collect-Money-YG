using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationPanel : MonoBehaviour
{
    [Tooltip("Понадобится, если хотите запускать анимцию при старте")]
    [Header("Settings Animation Panel")]
    [Space(5)]
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _transparency = 0.85f;

    [Space(5)]
    [Tooltip("Поставьте True, если хотите запускать анимацию при старте игры \n Если не хотите запускать при старте поставьте - False")]
    [SerializeField] private bool _startAnimation = true;

    [Space(5)]
    [SerializeField] private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        if (_startAnimation)
            AnimationPanelUpdate(_duration, _transparency, 0);
    }

    public void AnimationPanelUpdate(float duration, float transparency, float startColor)
    {
        Color startColorImage = buttonImage.color;
        startColorImage.a = startColor;
        buttonImage.color = startColorImage;

        buttonImage.DOFade(transparency, duration);
    }
}