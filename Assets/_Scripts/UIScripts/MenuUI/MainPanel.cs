using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingButton;

    public Button PlayButton { get { return _playButton; } }
    public Button SettingButton { get { return _settingButton; } }
}
