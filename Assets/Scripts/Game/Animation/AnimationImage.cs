using UnityEngine;
using DG.Tweening;

public class AnimationImage : MonoBehaviour
{
    public enum AnimSelection
    {
        animByX,
        animByY
    }

    public enum AnimationPause
    {
        True,
        False
    }

    [Header("Animation Settings")]
    [Space(5)]
    [SerializeField] private float _duration = 1f;
    [Space(5)]
    [Header("Object Displacement")]
    [SerializeField] private float _offsetX = 0;
    [SerializeField] private float _offsetY = -100f;

    [Space(10)]
    [Header("Selection Animation")]
    [Tooltip("Выберите по каким координатам вы хотите анимировать image. X или Y")]
    [SerializeField] private AnimSelection _animSelection;
    [Space(5)]
    [Tooltip("Если нужно, чтобы анимация работала при timeScale = 0, тогда поставьте - True.\n Если нужно, чтобы анимация останавливалась при timeScale = 0, тогда поставьте - False")]
    [SerializeField] private AnimationPause _animationPause = AnimationPause.False;

    public bool startAnimation = true;

    private Vector3 startPos;

    private void Start()
    {
        if(startAnimation)
            AnimationImageUpdate();
    }

    public void AnimationImageUpdate()
    {
        startPos = transform.localPosition;
        transform.localPosition += new Vector3(_offsetX, _offsetY, 0);

        switch (_animSelection)
        {
            case AnimSelection.animByX:
                switch(_animationPause)
                {
                    case AnimationPause.True:
                        transform.DOLocalMoveX(startPos.x, _duration)
                        .SetEase(Ease.OutBack)
                        .SetUpdate(true);
                        break;
                    case AnimationPause.False:
                        transform.DOLocalMoveX(startPos.x, _duration)
                        .SetEase(Ease.OutBack);
                        break;
                }
                break;
            case AnimSelection.animByY:
                switch (_animationPause)
                {
                    case AnimationPause.True:
                        transform.DOLocalMoveY(startPos.y, _duration)
                        .SetEase(Ease.OutBack)
                        .SetUpdate(true);
                        break;
                    case AnimationPause.False:
                        transform.DOLocalMoveY(startPos.y, _duration)
                        .SetEase(Ease.OutBack);
                        break;
                }
                break;
        }
    }
}
