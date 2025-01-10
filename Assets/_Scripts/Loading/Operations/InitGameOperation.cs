using Cysharp.Threading.Tasks;
using UnityEngine;

public class InitGameOperation : ILoadingOperation
{
    private int _lvlNum;
    public InitGameOperation(int lvlNum)
    {
        _lvlNum = lvlNum;
    }
    public async UniTask Load()
    {
        var gameManager = GameObject.FindAnyObjectByType<GameManager>();
        await gameManager.StartNewGame(_lvlNum);
    }
}
