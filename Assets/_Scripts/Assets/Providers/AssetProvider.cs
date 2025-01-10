using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

public class AssetProvider 
{
    public async UniTask<SceneInstance> LoadSceneAddressables(string sceneId)
    {
        var op = Addressables.LoadSceneAsync(sceneId);
        return await op.Task;
    }

    public async UniTask UnloadAddressablesScene(SceneInstance scene)
    {
        var op = Addressables.UnloadSceneAsync(scene);
        await op.Task;
    }
    public void Initialise()
    {
        Addressables.InitializeAsync();
    }
}
