using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button _closePause;
    [SerializeField] private Button _backToMenu;

    public Button ClosePausePanelButton {  get { return _closePause; } }
    public Button BackToMenuButton { get { return _backToMenu; } }
}
