using UnityEngine;
using UnityEngine.Pool;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<GameObject> _poolCubes;

    private void Awake()
    {
        _poolCubes = new ObjectPool<GameObject>
        (
            createFunc: CreateCube,
            actionOnGet: ActivateCube,
            actionOnRelease: DeactivateCube,
            actionOnDestroy: DestroyCube,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    public void ReleaseCube(GameObject cube)
    {
        _poolCubes.Release(cube);
    }

    public GameObject GetCube()
    {
        return _poolCubes.Get();
    }

    private GameObject CreateCube()
    {
        return Instantiate(_cubePrefab);
    }

    private void ActivateCube(GameObject cube)
    {
        cube.SetActive(true);
    }

    private void DeactivateCube(GameObject cube)
    {
        cube.SetActive(false);
    }

    private void DestroyCube(GameObject cube)
    {
        Destroy(cube);
    }
}
