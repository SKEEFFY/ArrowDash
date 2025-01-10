using System;
using Zenject;

public class GameUIController 
{
    public Action OnPauseSwitch;
    public Action OnCloseGameScene;

    private GameCanvas _gameCanvas;

    [Inject]
    private void Construct(GameCanvas gameCanvas)
    {
        _gameCanvas = gameCanvas;

        _gameCanvas.PausePanel.gameObject.SetActive(false);
        _gameCanvas.OpenPauseButton.onClick.AddListener(PauseEnable);
        _gameCanvas.PausePanel.ClosePausePanelButton.onClick.AddListener(PauseDisable);
        _gameCanvas.PausePanel.BackToMenuButton.onClick.AddListener(() => OnCloseGameScene?.Invoke());
    }
    private void PauseEnable()
    {
        OnPauseSwitch?.Invoke();
        _gameCanvas.PausePanel.gameObject.SetActive(true);
    }
    private void PauseDisable()
    {
        OnPauseSwitch?.Invoke();
        _gameCanvas.PausePanel.gameObject.SetActive(false);
    }
}
