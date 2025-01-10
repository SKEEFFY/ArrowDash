using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameCanvas _gameCanvasPrefab;
    public override void InstallBindings()
    {
        CanvasBinding();
        UIControllerBinding();
    }
    private void CanvasBinding()
    {
        Container
            .Bind<GameCanvas>()
            .FromComponentInNewPrefab(_gameCanvasPrefab)
            .AsSingle()
            .NonLazy();
    }
    private void UIControllerBinding()
    {
        Container
            .Bind<GameUIController>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}