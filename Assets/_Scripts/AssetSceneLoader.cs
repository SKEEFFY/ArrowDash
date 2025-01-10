using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class AssetSceneLoader
{
    private AssetProvider _assetProvider;
    private EventBase _eventBase;
    private AudioManager _audioManager;

    [Inject]
    private void Construct( 
        AssetProvider assetProvider, 
        EventBase eventBase, 
        AudioManager audioManager)
    {
        _audioManager = audioManager;
        _eventBase = eventBase;
        _assetProvider = assetProvider;

        _eventBase?.Subscribe<StartGameEvent>(LoadGameScene);
        _eventBase?.Subscribe<EndGameEvent>(LoadMenuScene);
    }

    private async void LoadGameScene(StartGameEvent gameEvent)
    {
        _eventBase?.Unsubscribe<InitMenuUIEvent>();
        _audioManager.StopAudio();

        var loadingOperations = new Queue<ILoadingOperation>();
        loadingOperations.Enqueue(new LoadGameSceneOperation(_assetProvider));
        loadingOperations.Enqueue(new InitGameOperation(gameEvent.Index));
        await new LoadingScreenProvider().LoadAndDestroy(loadingOperations);
    }
    private async void LoadMenuScene(EndGameEvent gameEvent)
    {
        _eventBase?.Unsubscribe<ChangeSpriteEvent>();
        _audioManager.StopAudio();

        var loadingOperations = new Queue<ILoadingOperation>();
        loadingOperations.Enqueue(new LoadMenuSceneOperation());
        loadingOperations.Enqueue(new InitialiseMenuUIOperation(_eventBase));
        loadingOperations.Enqueue(new InitialiseAudioOperation(_audioManager));
        await new LoadingScreenProvider().LoadAndDestroy(loadingOperations);
        YandexGame.FullscreenShow();
    }
}
