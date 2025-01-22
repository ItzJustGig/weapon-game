using UnityEngine;

public class CameraCanvas : MonoBehaviour
{
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = FindAnyObjectByType<Camera>();
    }
}
