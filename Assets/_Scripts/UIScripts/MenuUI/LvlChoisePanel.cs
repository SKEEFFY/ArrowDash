using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LvlChoisePanel : MonoBehaviour
{
    [SerializeField] private Button _leftSwipeButton;
    [SerializeField] private Button _rightSwipeButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private RectTransform _lvlBlock;

    [SerializeField] private Sprite[] _lvlFaces;

    private int _faceIndex;
    private Image _lvlButtonImage;
    public int FaceIndex
    {
        get
        {
            return _faceIndex;
        }
        private set
        {
            _faceIndex = value;
            if (_faceIndex < 0)
            {
                _faceIndex = _lvlFaces.Length - 1;
            } else if(_faceIndex >= _lvlFaces.Length)
            {
                _faceIndex = 0;
            }
        }
    }

    private void Awake()
    {
        _lvlButtonImage = _playButton.GetComponent<Image>();
    }
    private void OnEnable()
    {
        YandexGame.GetDataEvent += SetProgress;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= SetProgress;
    }
    private void Start()
    {
        FaceIndex = 0;
        ChangeLvlFace(0);
    }

    public RectTransform LvlBlock { get { return _lvlBlock; } }
    public Button BackButton { get { return _backButton; } }
    public Button LeftSwipeButton { get { return _leftSwipeButton; } }
    public Button RightSwipeButton { get { return _rightSwipeButton; } }
    public Button PlayButton { get { return _playButton; } }

    private void SetProgress()
    {
        int progress = YandexGame.savesData.lvlsProgress[FaceIndex];
        progress = progress <= 100 ? progress : 100;
        _progressText.text = progress.ToString() + "%";
    }
    public void ChangeLvlFace(int direction)
    {
        FaceIndex += direction;
        _lvlButtonImage.sprite = _lvlFaces[FaceIndex];
        SetProgress();
    }
}
