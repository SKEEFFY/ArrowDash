using Cysharp.Threading.Tasks;

public class InitialiseMenuUIOperation : ILoadingOperation
{
    private EventBase _eventBase;
    public InitialiseMenuUIOperation(EventBase eventBase)
    {
        _eventBase = eventBase;
    }
    public async UniTask Load()
    {
        _eventBase?.Invoke(new InitMenuUIEvent());
        await UniTask.Yield();
    }
}
