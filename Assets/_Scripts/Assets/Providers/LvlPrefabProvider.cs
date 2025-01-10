using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LvlPrefabProvider 
{
    private GameObject _lvlPrefab;
    public async UniTask<GameObject> LoadLvl(string asset)
    {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(asset);
        var lvl = await handle.Task;
        if (lvl == null)
        {
            throw new NullReferenceException($"Object of type {typeof(GameObject)} is null on " +
                                             "attempt to load it from addressables");
        }
        Unload();
        _lvlPrefab = lvl;
        return lvl;
    }
    public void Unload()
    {
        if (_lvlPrefab == null)
            return;
        Addressables.Release(_lvlPrefab);
        _lvlPrefab = null;
    }
}
