using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private Button _openPause;
    [SerializeField] private PausePanel _pausePanel;

    public PausePanel PausePanel { get { return _pausePanel; } }
    public Button OpenPauseButton { get { return _openPause; } }
}
