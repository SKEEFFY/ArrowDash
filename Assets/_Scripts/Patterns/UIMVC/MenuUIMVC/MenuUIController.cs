using YG;
using Zenject;

public class MenuUIController
{
    private MenuUIModel _model;
    private MenuUIView _view;
    private EventBase _eventBase;

    [Inject]
    private void Construct(MenuCanvas menuCanvas, EventBase eventBase)
    {
        _eventBase = eventBase;
        _eventBase?.Subscribe<InitMenuUIEvent>(Initialize);
        _view = new MenuUIView(menuCanvas, eventBase);
        _view.OnVolumeChange += ChangeVolume;

        _model = new MenuUIModel();
    }
    public void Initialize(InitMenuUIEvent uIEvent)
    {
        _model.Initialize();
        _view.Initialize();
    }
    private void ChangeVolume(float volume)
    {
        _model.ChangeVolumeModel(volume);
        _eventBase?.Invoke(new SoundChangedEvent(YandexGame.savesData.soundVolume));
    }
}

