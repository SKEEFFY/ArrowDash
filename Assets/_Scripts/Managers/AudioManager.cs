using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _loseClip;

    private AudioSource _source;
    private EventBase _eventBase;
    private AudioClip _currentClip;

    private float _currentTime;

    [Inject]
    private void Construct(EventBase eventBase)
    {
        _eventBase = eventBase;
        _eventBase?.Subscribe((SoundChangedEvent chnaged) => _source.volume = chnaged.NewSoundValue);
    }
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    public async UniTask LoadAndPlayAudioClip(string audioClipName, bool isLoop)
    {
        var clip = await new AudioClipProvider().LoadAudioClip(audioClipName);
        _currentClip = clip;
        PlayClip(_currentClip);
        IsLoop(isLoop);
        await UniTask.Yield();
    }
    public void StopAudio()
    {
        _source.Stop();
        _source.clip = null;
    }
    public void PlayClip(AudioClip clip)
    {
        _source.mute = false;
        _source.clip = clip;
        _source.Play();
    }
    public void IsLoop(bool loop)
    {
        _source.loop = loop;
    }
    public void PlayLose()
    {
        _source.Stop();
        _source.clip = _loseClip;
        _source.Play();
    }
    public void RestartPlay()
    {
        PlayClip(_currentClip);
    }
    public void SwitchAudioPause()
    {
        if (Time.timeScale != 0)
        {
            _currentTime = _source.time;
            _source.Pause();
        }
        else
        {
            _source.time = _currentTime;
            _source.Play();
        }
    }
}
