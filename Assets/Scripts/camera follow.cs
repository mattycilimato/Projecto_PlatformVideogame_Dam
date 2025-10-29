using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [Header("Setting")]

    public Vector3 followoffset;
    public float SmoothSpeed = 0.2f;


    [Header("Componet")]
    public Transform playerTransform;

    float zPosition;
    Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        zPosition = transform.position.z;
    }



    public void Update()
    {
        Vector3 targetPosition =playerTransform.position + followoffset;
        targetPosition.z = zPosition;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, SmoothSpeed);
    }





























}
