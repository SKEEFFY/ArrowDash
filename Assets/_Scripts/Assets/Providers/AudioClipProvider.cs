using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioClipProvider 
{
    private AudioClip _audioClip;
    public async UniTask<AudioClip> LoadAudioClip(string asset)
    {
        AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(asset);
        var clip = await handle.Task;
        if (clip == null)
        {
            throw new NullReferenceException($"Object of type {typeof(AudioClip)} is null on " +
                                             "attempt to load it from addressables");
        }
        Unload();
        _audioClip = clip;
        return clip;
    }
    public void Unload()
    {
        if (_audioClip == null)
            return;
        Addressables.Release(_audioClip);
        _audioClip = null;
    }
}
