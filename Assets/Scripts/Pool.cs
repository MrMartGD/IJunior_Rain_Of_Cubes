using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolSize = 20;

    [Header("Colors")]
    [SerializeField] private Color _initialColor = Color.white;
    [SerializeField] private Color _collisionColor = Color.red;

    [Header("Lifetime")]
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private List<Cube> _availableObjects;
    private List<Cube> _activeObjects;

    private void Awake()
    {
        Create();
    }

    public bool TryGet(out Cube cube)
    {
        if (_availableObjects.Count == 0)
        {
            cube = null;
            return false;
        }

        cube = _availableObjects[0];

        _availableObjects.RemoveAt(0);
        _activeObjects.Add(cube);

        cube.gameObject.SetActive(true);

        return true;
    }

    public void Release(Cube cube)
    {
        _activeObjects.Remove(cube);

        cube.gameObject.SetActive(false);

        _availableObjects.Add(cube);
    }

    private void Create()
    {
        _availableObjects = new List<Cube>();
        _activeObjects = new List<Cube>();

        for (int i = 0; i < _poolSize; i++)
        {
            Cube cube = Instantiate(_cubePrefab, transform);

            cube.Initialize(_initialColor, _collisionColor);

            cube.GetComponent<CubeLifetime>().Initialize(this, _minLifetime, _maxLifetime);

            cube.gameObject.SetActive(false);

            _availableObjects.Add(cube);
        }
    }
}