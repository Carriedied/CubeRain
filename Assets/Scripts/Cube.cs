using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> Release;

    private Coroutine _disappearCoroutine;
    private ColorChanger _cubeColor;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    private void Awake()
    {
        _cubeColor = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_disappearCoroutine == null)
        {
            _cubeColor.ChangeColor();

            _disappearCoroutine = StartCoroutine(DeactivateAfterDelay(ChangeLifeTime()));
        }
    }

    private void TriggerRelease()
    {
        Release?.Invoke(this);
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

        _cubeColor.BackToInitialColor();

        TriggerRelease();
    }

    private float ChangeLifeTime()
    {
        return UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);
    }
}
