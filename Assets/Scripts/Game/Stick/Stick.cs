using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Stick : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Space(10)]
    [Header("Settings Animation Stick")]
    [SerializeField] private float _moveDistance = 3f;
    [SerializeField] private float _duration = 1f;

    private void Start()
    {
        _button.onClick.AddListener(ButtonStickOn);
    }

    private void ButtonStickOn()
    {
        if(MenuManager.perfomanceOfTheStick)
        {
            Vector3 targetPos = transform.position + transform.right * _moveDistance;
            transform.DOMove(targetPos, _duration).SetEase(Ease.OutQuad);
        }
    }
}
