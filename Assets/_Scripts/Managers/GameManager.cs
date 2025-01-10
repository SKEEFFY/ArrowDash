using UnityEngine;
using Zenject;
using System;
using Cysharp.Threading.Tasks;
using Cinemachine;
using System.Threading;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BackgroundSpriteOffsetChanger _background;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _followingObjects;


    [SerializeField] private LvlScriptableObject[] _lvlScriptableObjects;

    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private ParticleSystem _deathParticle;

    [SerializeField] private float _deathDelay = 1;
    [SerializeField] private float _winDelay = 1.5f;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private AudioManager _audioManager;
    private Arrow _arrow;
    private GameUIController _gameUIController;
    private EventBase _eventBase;

    private GameObject _currentLvlObjects;
    private CancellationTokenSource _cancellationTokenSource;
    private LvlScriptableObject _currentLvl;

    private float _currentLvlProgress;

    [Inject]
    private void Construct(AudioManager audioManager, GameUIController gameUIController, EventBase eventBase)
    {
        _eventBase = eventBase;
        _gameUIController = gameUIController;
        _gameUIController.OnPauseSwitch += GamePauseSwitch;
        _gameUIController.OnCloseGameScene += BackToMenu;
        _audioManager = audioManager;
    }
    public async UniTask StartNewGame(int currentLvl)
    {
        _currentLvl = Array.Find(_lvlScriptableObjects, (LvlScriptableObject o) => o.LvlNum == currentLvl);
        if (_currentLvl != null)
        {
            var mat = await new BackgroundMaterialProvider().LoadMaterial(_currentLvl.LvlBackground);
            var lvlPref = await new LvlPrefabProvider().LoadLvl(_currentLvl.LvlPrefab);

            _currentLvlProgress = YandexGame.savesData.lvlsProgress[_currentLvl.LvlNum];
            _currentLvlObjects = Instantiate(lvlPref);
            var pos = _virtualCamera.transform.position;
            pos.z = 0;
            _currentLvlObjects.transform.position = pos;

            _background.SetMaterial(mat);
            _background.SetColor(_currentLvl.LvlColor);

            await _audioManager.LoadAndPlayAudioClip(_currentLvl.LvlAudioClip, false);
        }
        CreateArrow();
    }
    private void CreateArrow()
    {
        _arrow = Instantiate(_arrowPrefab, _spawnPoint.position, Quaternion.identity);
        _arrow.OnDeath += RestartGame;
        _arrow.OnFinish += FinishGame;
        _arrow.OnFixedUpdate += MoveObjects;
        _arrow.OnFinishClose += FinishClose;
        _arrow.OnStartLags += _background.ChangeOnLagsAnim;

        _virtualCamera.Follow = _arrow.transform;
    }
    private void OnDisable()
    {
        _gameUIController.OnPauseSwitch -= GamePauseSwitch;
        _gameUIController.OnCloseGameScene -= BackToMenu;

        _arrow.OnDeath -= RestartGame;
        _arrow.OnFinish -= FinishGame;
        _arrow.OnFixedUpdate -= MoveObjects;
        _arrow.OnFinishClose -= FinishClose;
        _arrow.OnStartLags -= _background.ChangeOnLagsAnim;
    }
    private void MoveObjects()
    {
        var pos = _virtualCamera.transform.position;
        pos.z = 0;
        _followingObjects.position = pos;
    }
    private void RestartGame()
    {
        PlayParticle();
        _background.ChangeSpeed(0);
        _audioManager.PlayLose();
        SaveProgress();

        _cancellationTokenSource = new CancellationTokenSource();
        DeathDelay(_cancellationTokenSource.Token).Forget();
    }
    private void FinishGame()
    {
        PlayParticle();
        _audioManager.StopAudio();

        _cancellationTokenSource = new CancellationTokenSource();
        WinDelay(_cancellationTokenSource.Token).Forget();
    }
    private void PlayParticle()
    {
        _deathParticle.transform.position = _arrow.transform.position;
        _deathParticle?.Play();
    }


    private async UniTask WinDelay(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_winDelay), cancellationToken: token);
        if (token.IsCancellationRequested) return;
        BackToMenu();
    }
    private async UniTask DeathDelay(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_deathDelay), cancellationToken: token);
        if (token.IsCancellationRequested) return;

        _arrow.transform.position = _spawnPoint.position;
        _arrow.ClearTrail();
        _arrow.ReInit();

        _audioManager.RestartPlay();
        _background.ChangeSpeed(-1);
        _background.ReturnTexturePos();
    }

    private void BackToMenu()
    {
        SaveProgress();

        _cancellationTokenSource?.Cancel();
        Time.timeScale = 1;
        _eventBase?.Invoke(new EndGameEvent());
    }
    private void SaveProgress()
    {
        CheckGameProgress();
        YandexGame.savesData.lvlsProgress[_currentLvl.LvlNum] = Mathf.RoundToInt(_currentLvlProgress);
        YandexGame.SaveProgress();
    }
    private void FinishClose()
    {
        _background.ChangeSpeed(0);
        _virtualCamera.Follow = null;
    }
    private void CheckGameProgress()
    {
        float progress = _arrow.transform.position.x / _currentLvl.Duration * 100;
        _currentLvlProgress = progress > _currentLvlProgress ? progress : _currentLvlProgress;
    }
    private void GamePauseSwitch()
    {
        _audioManager.SwitchAudioPause();
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
