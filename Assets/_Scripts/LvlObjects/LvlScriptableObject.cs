using UnityEngine;

[CreateAssetMenu(menuName = "LvlSettings", order = 1)]
public class LvlScriptableObject : ScriptableObject
{
    [SerializeField] private int _duration;
    [SerializeField] private int _lvlNum = 0;
    [SerializeField] private string _lvlAudioClip;
    [SerializeField] private string _lvlBackground;
    [SerializeField] private string _lvlPrefab;
    [SerializeField] private Color _lvlColor;

    public int Duration { get { return _duration; } }
    public int LvlNum { get { return _lvlNum; } }
    public string LvlAudioClip { get { return _lvlAudioClip; } }
    public string LvlPrefab { get { return _lvlPrefab; } }
    public string LvlBackground { get { return _lvlBackground; } }
    public Color LvlColor { get { return _lvlColor; } }

    public void SetProgress(int progress)
    {
        _duration = progress;
    }
}
