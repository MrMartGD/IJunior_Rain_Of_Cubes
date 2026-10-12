using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CubeLifetime))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private CubeLifetime _lifetime;

    private Color _initialColor;
    private Color _collisionColor;

    private bool _hasCollided;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _lifetime = GetComponent<CubeLifetime>();
    }

    public void Initialize(Color initialColor, Color collisionColor)
    {
        _initialColor = initialColor;
        _collisionColor = collisionColor;
    }

    public void ResetState(Vector3 position, Quaternion rotation)
    {
        _hasCollided = false;

        _lifetime.ResetLifetime();

        transform.SetPositionAndRotation(position, rotation);

        _rigidbody.position = position;
        _rigidbody.rotation = rotation;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _meshRenderer.material.color = _initialColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        _hasCollided = true;

        _meshRenderer.material.color = _collisionColor;

        _lifetime.StartLifetime();
    }
}