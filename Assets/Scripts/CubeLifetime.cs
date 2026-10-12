using System.Collections;
using UnityEngine;

public class CubeLifetime : MonoBehaviour
{
    private Pool _pool;
    private float _minLifetime;
    private float _maxLifetime;
    private Coroutine _lifetimeCoroutine;
    private Cube _cube;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    public void Initialize(Pool pool, float minLifetime, float maxLifetime)
    {
        _pool = pool;

        _minLifetime = minLifetime;
        _maxLifetime = maxLifetime;
    }

    public void StartLifetime()
    {
        if (_lifetimeCoroutine != null)
            return;

        float lifetime = Random.Range(_minLifetime, _maxLifetime);

        _lifetimeCoroutine = StartCoroutine(ReturnAfterLifetime(lifetime));
    }

    public void ResetLifetime()
    {
        if (_lifetimeCoroutine == null)
            return;

        StopCoroutine(_lifetimeCoroutine);
        _lifetimeCoroutine = null;
    }

    private IEnumerator ReturnAfterLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);

        _lifetimeCoroutine = null;

        _pool.Release(_cube);
    }
}