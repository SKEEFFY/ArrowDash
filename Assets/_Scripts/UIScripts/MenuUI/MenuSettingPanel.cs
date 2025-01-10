using UnityEngine;
using UnityEngine.UI;

public class MenuSettingPanel : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Button _backButton;
    [SerializeField] private RectTransform _soundBlock;
    [SerializeField] private RectTransform _buttonBlock;

    public Image Background {  get { return _background; } }
    public RectTransform ButtonBlock { get { return _buttonBlock; } }
    public RectTransform SoundBlock {  get { return _soundBlock; } }
    public Slider SoundSlider { get { return _soundSlider; } }
    public Button BackButton { get { return _backButton; } }
}
