using System;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundSpriteOffsetChanger : MonoBehaviour
{
    private Action OnMaterialAnim;

    private Renderer _renderer;
    private Material m_Material;
    private float _distance;

    [SerializeField] float _speed = 0.5f;

    private float _defaultSpeed;
    private void Awake()
    {
        _defaultSpeed = _speed;
        _renderer = GetComponent<Renderer>();
        m_Material = _renderer.material;
    }
    private void OnEnable()
    {
        OnMaterialAnim += DefaultAnim;
    }
    private void OnDisable()
    {
        OnMaterialAnim = null;
    }
    public void SetMaterial(Material material)
    {
        m_Material = material;
        _renderer.material = m_Material;
    }
    public void SetColor(Color color)
    {
        m_Material.color = color;
    }
    public void ChangeSpeed(float speed)
    {
        _speed = speed == -1 ? _defaultSpeed : speed;
    }
    public void ReturnTexturePos()
    {
        _distance = 0;
        m_Material.SetTextureOffset("_MainTex", Vector2.zero);
    }
    public void ChangeOnLagsAnim()
    {
        m_Material.SetTextureOffset("_MainTex", Vector2.zero);
        OnMaterialAnim -= DefaultAnim;
        OnMaterialAnim += LagsAnim;
    }
    private void Update()
    {
        OnMaterialAnim?.Invoke();
    }
    private void DefaultAnim()
    {
        _distance += Time.deltaTime * _speed;
        m_Material.SetTextureOffset("_MainTex", Vector2.left * _distance);
    }
    private void LagsAnim()
    {
        Vector2 offset = m_Material.GetTextureOffset("_MainTex");
        offset.y += Mathf.Sin(offset.x * 6.5f + Time.time);
        m_Material.SetTextureOffset("_MainTex", offset);
    }
}
