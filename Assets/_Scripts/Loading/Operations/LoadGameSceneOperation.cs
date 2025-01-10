using Cysharp.Threading.Tasks;

public class LoadGameSceneOperation : ILoadingOperation
{
    private AssetProvider _assetProvider;
    public LoadGameSceneOperation(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }
    public async UniTask Load()
    {
        await _assetProvider.LoadSceneAddressables(AssetsConstants.GameScene);
    }
}
