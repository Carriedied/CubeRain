using UnityEngine;
using UnityEngine.Pool;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<Cube> _poolCubes;

    private void Awake()
    {
        _poolCubes = new ObjectPool<Cube>
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

    public void ReleaseCube(Cube cube)
    {
        cube.Release -= ReleaseCube;

        _poolCubes.Release(cube);
    }

    public Cube GetCube()
    {
        return _poolCubes.Get();
    }

    private Cube CreateCube()
    {
        return Instantiate(_cubePrefab);
    }

    private void ActivateCube(Cube cube)
    {
        cube.Release += ReleaseCube;

        cube.gameObject.SetActive(true);
    }

    private void DeactivateCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private void DestroyCube(Cube cube)
    {
        Destroy(cube);
    }
}
