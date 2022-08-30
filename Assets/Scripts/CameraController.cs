using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _target;

    [SerializeField]
    private float _lag = 0;

    [SerializeField]
    private float _smoothing = 0.3f;

    public bool following = true;

    private Vector2 velocity;

    public void Initialize(Rigidbody2D target, float lag = 0, float smoothing = 1)
    {
        _lag = lag;
        _target = target;
        _smoothing = smoothing;
    }


    void LateUpdate()
    {
        if (_target && following)
        {
            //Vector3 p = Vector2.Lerp(transform.position, _target.transform.position + new Vector3(0, 1f) - new Vector3(_target.velocity.x, _target.velocity.y) * _lag, _smoothing);
            Vector2 target = _target.transform.position + new Vector3(0, 1) - new Vector3(_target.velocity.x, _target.velocity.y) * _lag;
            Vector3 p = Vector2.SmoothDamp(transform.position, target, ref velocity, _smoothing);
            Vector2.ClampMagnitude(p, 7);
            p.z = -10;
            transform.position = p;
        }
    }
}
