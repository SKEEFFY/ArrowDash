using System;
using YG;

public class MenuUIModel
{
    public Action<int> OnScoreChage;

    private int _score;

    private int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            OnScoreChage?.Invoke(_score);
        }
    }

    public void Initialize()
    {
        Score = YandexGame.savesData.score;
    }
    public void ChangeVolumeModel(float volume)
    {
        YandexGame.savesData.soundVolume = volume;
        YandexGame.SaveProgress();
    }
    public void ChangeScoreModel(int score)
    {
        Score += score;
    }
}
