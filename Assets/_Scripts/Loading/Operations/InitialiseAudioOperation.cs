using Cysharp.Threading.Tasks;

public class InitialiseAudioOperation : ILoadingOperation
{
    private AudioManager _audioManager;
    public InitialiseAudioOperation(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    public async UniTask Load()
    {
        await _audioManager.LoadAndPlayAudioClip(AssetsConstants.MenuAudioClip, true);
    }
}
