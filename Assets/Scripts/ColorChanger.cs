using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Color InitialColor;

    private Renderer _choiceCubeColor;

    private void Awake()
    {
        _choiceCubeColor = GetComponent<Renderer>();
        InitialColor = _choiceCubeColor.material.color;
    }

    public void BackToInitialColor()
    {
        _choiceCubeColor.material.color = InitialColor;
    }

    public void ChangeColor()
    {
        _choiceCubeColor.material.color = new Color(Random.value, Random.value, Random.value);
    }
}
