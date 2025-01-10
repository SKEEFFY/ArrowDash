using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private AudioManager _audioManagerPrefab;
    [SerializeField] private AppStartup _appStartupPrefab;
    public override void InstallBindings()
    {
        EventBaseBinding();
        AudioBinding();
        AssetProviderBinding();
        AssetSceneLoaderBinding();
        AppStartupBinding();
    }
    private void EventBaseBinding()
    {
        Container
        .Bind<EventBase>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
    private void AudioBinding()
    {
        Container
            .Bind<AudioManager>()
            .FromComponentInNewPrefab(_audioManagerPrefab)
            .AsSingle()
            .NonLazy();
    }
    private void AppStartupBinding()
    {
        Container
            .Bind<AppStartup>()
            .FromComponentInNewPrefab(_appStartupPrefab)
            .AsSingle()
            .NonLazy();
    }
    private void AssetProviderBinding()
    {
        Container
            .Bind<AssetProvider>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
    private void AssetSceneLoaderBinding()
    {
        Container
            .Bind<AssetSceneLoader>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}