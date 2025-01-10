using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class AppStartup : MonoBehaviour
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
        _eventBase = eventBase;
        _assetProvider = assetProvider;
        _audioManager = audioManager;
    }
    private void Awake()
    {
        _assetProvider.Initialise();
    }
    private async void Start()
    {
        var loadingOperations = new Queue<ILoadingOperation>();
        loadingOperations.Enqueue(new WaitYGPluginOperation());
        loadingOperations.Enqueue(new InitialiseMenuUIOperation(_eventBase));
        loadingOperations.Enqueue(new InitialiseAudioOperation(_audioManager));
        await new LoadingScreenProvider().LoadAndDestroy(loadingOperations);
        YandexGame.GameReadyAPI();
        Destroy(gameObject);
    }
}
