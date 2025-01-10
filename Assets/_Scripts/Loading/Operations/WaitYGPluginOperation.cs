using Cysharp.Threading.Tasks;
using System;
using YG;

public sealed class WaitYGPluginOperation : ILoadingOperation
{
    public async UniTask Load()
    {
        while (!YandexGame.SDKEnabled)
        {
            await UniTask.Yield();
        }
    }
}
