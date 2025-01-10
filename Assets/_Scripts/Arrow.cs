using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Action OnDeath;
    public Action OnFinish;
    public Action OnFinishClose;
    public Action OnStartLags;
    public Action OnFixedUpdate;

    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private TrailRenderer _trailRenderer;

    private Quaternion _upAngle = Quaternion.Euler(new Vector3(0f, 0f, 315));
    private Quaternion _downAngle = Quaternion.Euler(new Vector3(0f, 0f, 225));
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.rotation = _downAngle;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            transform.rotation = transform.rotation.eulerAngles.z == 315 ? _downAngle : _upAngle;
        }
        _rigidbody.velocity = transform.up * _moveSpeed;
    }
    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
    public void ReInit()
    {
        gameObject.SetActive(true);
        transform.rotation = _downAngle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Death();
        }
        else
        {
            Finish();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lags"))
        {
            OnStartLags?.Invoke();
        }
        else
        {
            OnFinishClose?.Invoke();
        }
    }
    public void ClearTrail()
    {
        _trailRenderer.Clear();
    }
    private void Death()
    {
        gameObject.SetActive(false);
        OnDeath?.Invoke();
    }
    private void Finish()
    {
        gameObject.SetActive(false);
        OnFinish?.Invoke();
    }
}
