using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubesPool _cubesPool;
    [SerializeField] private Transform _platform;

    private float _amountTimeStart = 1f;
    private float _spawnInterval = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), _amountTimeStart, _spawnInterval);
    }

    private void SpawnCube()
    {
        Cube cube = _cubesPool.GetCube();

        cube.transform.position = DetermineSpawnPoints();
    }

    private Vector3 DetermineSpawnPoints()
    {
        Vector3 spawnPoint;
        float valueBisection = 2f;

        float minValueXAxis = _platform.transform.position.x - (_platform.transform.localScale.x / valueBisection);
        float maxValueXAxis = _platform.transform.position.x + (_platform.transform.localScale.x / valueBisection);
        float minValueZAxis = _platform.transform.position.z - (_platform.transform.localScale.z / valueBisection);
        float maxValueZAxis = _platform.transform.position.z + (_platform.transform.localScale.z / valueBisection);

        float valueAlongYAxis = 15;

        spawnPoint.x = Random.Range(minValueXAxis, maxValueXAxis + 1);
        spawnPoint.z = Random.Range(minValueZAxis, maxValueZAxis + 1);
        spawnPoint.y = valueAlongYAxis;

        return spawnPoint;
    }
}
