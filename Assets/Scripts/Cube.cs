using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private CubesPool _cubePool;
    private Coroutine _disappearCoroutine;
    private Renderer _choiceCubeColor;
    private Color _defaultColor;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    private void Awake()
    {
        _choiceCubeColor = GetComponent<Renderer>();
        _defaultColor = _choiceCubeColor.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_disappearCoroutine == null)
        {
            ChangeColor();
            _disappearCoroutine = StartCoroutine(DeactivateAfterDelay(ChangeLifeTime()));
        }
    }

    public void Initialize(CubesPool pool)
    {
        _cubePool = pool;
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Disappear();
    }

    private void Disappear()
    {
        if (_disappearCoroutine != null)
        {
            StopCoroutine(_disappearCoroutine);
            _disappearCoroutine = null;
        }

        _choiceCubeColor.material.color = _defaultColor;
        _cubePool.ReleaseCube(gameObject);
    }

    private float ChangeLifeTime()
    {
        return Random.Range(_minLifeTime, _maxLifeTime + 1);
    }

    private void ChangeColor()
    {
        _choiceCubeColor.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
