using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 0.3f;

    private Vector3 originalPos;

    public void Shake()
    {
        originalPos = transform.localPosition;
        InvokeRepeating("DoShake", 0f, 0.01f);
        Invoke("StopShake", duration);
    }

    void DoShake()
    {
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;

        transform.localPosition = originalPos + new Vector3(x, y, 0);
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        transform.localPosition = originalPos;
    }
}