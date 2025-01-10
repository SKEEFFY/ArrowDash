using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private MenuSettingPanel _settingPanel;
    [SerializeField] private MainPanel _mainPanel;
    [SerializeField] private LvlChoisePanel _lvlChoisePanel;
    [SerializeField] private Image _blackPanel;

    public Image BlackPanel { get {  return _blackPanel; } }
    public LvlChoisePanel LvlChoisePanel { get { return _lvlChoisePanel; } }
    public MainPanel MainPanel {  get { return _mainPanel; } }
    public MenuSettingPanel SettingPanel { get {  return _settingPanel; } }
}