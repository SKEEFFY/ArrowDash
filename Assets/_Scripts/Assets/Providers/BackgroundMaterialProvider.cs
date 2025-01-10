using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BackgroundMaterialProvider 
{
    private Material _backgroundMaterial;
    public async UniTask<Material> LoadMaterial(string asset)
    {
        AsyncOperationHandle<Material> handle = Addressables.LoadAssetAsync<Material>(asset);
        var mat = await handle.Task;
        if (mat == null)
        {
            throw new NullReferenceException($"Object of type {typeof(Material)} is null on " +
                                             "attempt to load it from addressables");
        }
        Unload();
        _backgroundMaterial = mat;
        return mat;
    }
    public void Unload()
    {
        if (_backgroundMaterial == null)
            return;
        Addressables.Release(_backgroundMaterial);
        _backgroundMaterial = null;
    }
}
