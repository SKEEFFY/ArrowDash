using DG.Tweening;
using System;
using UnityEngine;
using YG;

public class MenuUIView
{
    public Action<float> OnVolumeChange;

    private MenuCanvas _menuCanvas;

    #region LvlPanelParam
    private Sequence _lvlSwipeSequence;

    private float _leftHidePosition = -1240;
    private float _rightHidePosition = 1240;
    private float _moveLvlBlockDuration = 0.15f;
    #endregion LvlPanelParam

    #region SettingPanelParam
    private Tween _fadeTween;
    private Sequence _settingSequence;

    private Color _fadeColor;
    private Color _normalColor;

    private float _fadeDuration = 0.7f;
    private float _moveSoundBockDuration = 0.3f;
    private float _moveButtonDuration = 0.3f;

    private float _defaultSoundBlockPosY = 0;
    private float _hideSoundBlockPosY = 600;

    private float _defaultButtonBlockPosX = 0;
    private float _hideButtonBlockPosX = 275;
    #endregion SettingPanelParam

    public MenuUIView(MenuCanvas menuCanvas, EventBase eventBase)
    {
        _menuCanvas = menuCanvas;

        _menuCanvas.LvlChoisePanel.PlayButton.onClick.AddListener(
            () => eventBase?.Invoke(new StartGameEvent(_menuCanvas.LvlChoisePanel.FaceIndex)));

        _menuCanvas.SettingPanel.SoundSlider.onValueChanged.AddListener((float volume) => OnVolumeChange?.Invoke(volume));
        _menuCanvas.MainPanel.SettingButton.onClick.AddListener(OpenSettings);
        _menuCanvas.SettingPanel.BackButton.onClick.AddListener(CloseSettings);

        _menuCanvas.MainPanel.PlayButton.onClick.AddListener(OpenLvlChoisePanel);
        _menuCanvas.LvlChoisePanel.BackButton.onClick.AddListener(CloseLvlChoisePanel);

        _menuCanvas.LvlChoisePanel.LeftSwipeButton.onClick.AddListener(SwipeLvlsLeft);
        _menuCanvas.LvlChoisePanel.RightSwipeButton.onClick.AddListener(SwipeLvlsRight);

        _normalColor = _menuCanvas.SettingPanel.Background.color;
        _fadeColor = _normalColor;
        _fadeColor.a = 0;
    }
    
    public void Initialize()
    {
        GameObject.Destroy(_menuCanvas.BlackPanel);
        _menuCanvas.SettingPanel.gameObject.SetActive(false);
        _menuCanvas.LvlChoisePanel.gameObject.SetActive(false);
        _menuCanvas.SettingPanel.SoundSlider.value = YandexGame.savesData.soundVolume;
        OnVolumeChange?.Invoke(YandexGame.savesData.soundVolume);
    }
    private void OpenLvlChoisePanel()
    {
        _menuCanvas.LvlChoisePanel.gameObject.SetActive(true);
        _menuCanvas.MainPanel.gameObject.SetActive(false);
    }
    private void CloseLvlChoisePanel()
    {
        _menuCanvas.MainPanel.gameObject.SetActive(true);
        _menuCanvas.LvlChoisePanel.gameObject.SetActive(false);
    }
    private void SwipeLvlsLeft()
    {
        _lvlSwipeSequence = DOTween.Sequence();
        var fpart = _menuCanvas.LvlChoisePanel.LvlBlock.DOAnchorPosX(_rightHidePosition, _moveLvlBlockDuration).OnComplete(ActionsAfterFirstPartLeftSwipe);
        _lvlSwipeSequence.Append(fpart);
        _lvlSwipeSequence.Append(_menuCanvas.LvlChoisePanel.LvlBlock.
            DOAnchorPosX(0, _moveLvlBlockDuration)).SetEase(Ease.Linear);
    }
    private void ActionsAfterFirstPartLeftSwipe()
    {
        _menuCanvas.LvlChoisePanel.LvlBlock.anchoredPosition = new Vector2(_leftHidePosition, 0);
        _menuCanvas.LvlChoisePanel.ChangeLvlFace(-1);
    }
    private void SwipeLvlsRight()
    {
        _lvlSwipeSequence = DOTween.Sequence();
        var fpart = _menuCanvas.LvlChoisePanel.LvlBlock.DOAnchorPosX(_leftHidePosition, _moveLvlBlockDuration).
            OnComplete(ActionsAfterFirstPartRightSwipe);
        _lvlSwipeSequence.Append(fpart);
        _lvlSwipeSequence.Append(_menuCanvas.LvlChoisePanel.LvlBlock.
            DOAnchorPosX(0, _moveLvlBlockDuration)).SetEase(Ease.Linear);
    }
    private void ActionsAfterFirstPartRightSwipe()
    {
        _menuCanvas.LvlChoisePanel.LvlBlock.anchoredPosition = new Vector2(_rightHidePosition, 0);
        _menuCanvas.LvlChoisePanel.ChangeLvlFace(1);
    }
    private void OpenSettings()
    {
        _menuCanvas.SettingPanel.gameObject.SetActive(true);
        _menuCanvas.SettingPanel.Background.color = _fadeColor;
        _menuCanvas.SettingPanel.ButtonBlock.anchoredPosition = new Vector2(_hideButtonBlockPosX, 0);
        _menuCanvas.SettingPanel.SoundBlock.anchoredPosition = new Vector2(0, _hideSoundBlockPosY);

        _fadeTween.Kill();
        _settingSequence.Kill();
        _settingSequence = DOTween.Sequence();

        _fadeTween = _menuCanvas.SettingPanel.Background.DOFade(_normalColor.a, _fadeDuration);
        _settingSequence.Append(_menuCanvas.SettingPanel.ButtonBlock.
            DOAnchorPosX(_defaultButtonBlockPosX, _moveButtonDuration)).SetEase(Ease.Linear);
        _settingSequence.Append(_menuCanvas.SettingPanel.SoundBlock.
            DOAnchorPosY(_defaultSoundBlockPosY, _moveSoundBockDuration)).SetEase(Ease.Linear);

    }
    private void CloseSettings()
    {
        _fadeTween.Kill();
        _settingSequence.Kill();
        _settingSequence = DOTween.Sequence();

        _fadeTween = _menuCanvas.SettingPanel.Background.DOFade(0, _fadeDuration);
        _settingSequence.Append(_menuCanvas.SettingPanel.ButtonBlock.
            DOAnchorPosX(_hideButtonBlockPosX, _moveButtonDuration)).SetEase(Ease.Linear);
        _settingSequence.Append(_menuCanvas.SettingPanel.SoundBlock.
            DOAnchorPosY(_hideSoundBlockPosY, _moveSoundBockDuration)).SetEase(Ease.Linear);
        _settingSequence.OnComplete(() => _menuCanvas.SettingPanel.gameObject.SetActive(false));
    }
}
