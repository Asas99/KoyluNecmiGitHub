
using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform target;               // Takip edilecek nesne
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Genellikle Z offset -10 olur
    public float smoothSpeed = 5f;         // Takip hızı

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        smoothedPosition.z = offset.z; // Z sabit kalır
        transform.position = smoothedPosition;
    }
}
