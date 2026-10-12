using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Pool _pool;
    [SerializeField] private BoxCollider _mainPlatform;

    [Header("Spawn Settings")]
    [SerializeField] private float _spawnInterval = 0.5f;
    [SerializeField] private float _spawnHeight = 12f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _spawnInterval)
            return;

        _timer -= _spawnInterval;

        SpawnCube();
    }

    private void SpawnCube()
    {
        if (_pool.TryGet(out Cube cube) == false)
            return;

        Bounds bounds = _mainPlatform.bounds;

        Vector3 position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), _spawnHeight, Random.Range(bounds.min.z, bounds.max.z));

        Quaternion rotation = Random.rotation;

        cube.ResetState(position, rotation);
    }
}