using UnityEngine;
using UnityEngine.UI;

public class BackgroundOffsetChanger : MonoBehaviour
{
    [SerializeField] private float _groundSpeed = 0.2f;

    private Material m_Material;
    private float _distance;

    private Image m_Image;
    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Material = m_Image.material;
    }
    public void SetMaterial(Material material)
    {
        m_Material = material;
        m_Image.material = material;
    }
    public void SetColor(Color color)
    {
        m_Image.color = color;
    }
    private void Update()
    {
        _distance += _groundSpeed * Time.deltaTime;
        if (m_Material != null)
        {
            m_Material.SetTextureOffset("_MainTex", Vector2.right * _distance);
        }
    }
}
