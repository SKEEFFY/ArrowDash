using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LoadMenuSceneOperation : ILoadingOperation
{
    public async UniTask Load()
    {
        var loadOp = SceneManager.LoadSceneAsync("MenuScene");
        while (!loadOp.isDone)
        {
            await UniTask.Yield();
        }
    }
}
