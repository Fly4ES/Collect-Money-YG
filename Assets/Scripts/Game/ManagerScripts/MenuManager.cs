using UnityEngine;
using UnityEngine.UI;
using YG;
using TMPro;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("Panel All")]
    [Space(5)]
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelGame;
    [Space(5)]
    [SerializeField] private GameObject _panelMoney;

    [Space(10)]
    [Header("Text All")]
    [Space(5)]
    [SerializeField] private TMP_Text _textName;

    [Space(10)]
    [Header("Button All")]
    [Space(5)]
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonMusic;
    [Space(5)]
    [Header("Animation Button")]
    [SerializeField] private float _scaleFactorButton = 0.8f;
    [SerializeField] private float _durationScaleButton = 0.1f;

    [Space(10)]
    [Header("Settings Audio")]
    [Space(5)]
    [SerializeField] private Image _imageIconMusic;
    [Space(5)]
    [SerializeField] private float _durationColorChange = 2f;

    public static bool perfomanceOfTheStick = false;

    // Animation Button
    private Vector3 originalScaleButtonPlay;
    private Vector3 originalScaleButtonMusic;

    public void Initialize()
    {
        #region Button Original Scale

        originalScaleButtonPlay = _buttonPlay.transform.localScale;
        originalScaleButtonMusic = _buttonMusic.transform.localScale;

        #endregion

        #region Button

        _buttonPlay.onClick.AddListener(ButtonPlay);
        _buttonMusic.onClick.AddListener(ButtonMusic);

        #endregion

        perfomanceOfTheStick = false;

        MusicOnAndOff();
    }

    #region Button All

    public void ButtonPlay()
    {
        AnimationScaleButton(_buttonPlay, originalScaleButtonPlay);

        Invoke("ChangePanel", 0.1f);
        perfomanceOfTheStick = true;
    }

    public void ButtonMusic()
    {
        AnimationScaleButton(_buttonMusic, originalScaleButtonMusic);
        Bootstrap.Instance.VibrationManager.StartVibration(500);

        if (YG2.saves.music)
        {
            YG2.saves.music = false;
            YG2.SaveProgress();
            MusicOnAndOff();
        }
        else
        {
            YG2.saves.music = true;
            YG2.SaveProgress();
            MusicOnAndOff();
        }
    }

    #endregion

    private void MusicOnAndOff()
    {
        if (YG2.saves.music)
            _imageIconMusic.DOColor(Color.green, _durationColorChange);
        else
            _imageIconMusic.DOColor(Color.red, _durationColorChange);
    }

    private void ChangePanel()
    {
        _panelMenu.SetActive(false);
        _panelGame.SetActive(true);
    }

    private void AnimationScaleButton(Button button, Vector3 originalScale)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(button.transform.DOScale(originalScale * _scaleFactorButton, _durationScaleButton) // Уменьшение
            .SetEase(Ease.OutQuad))
            .Append(button.transform.DOScale(originalScale, _durationScaleButton) // Возврат
            .SetEase(Ease.OutQuad));
    }
}
