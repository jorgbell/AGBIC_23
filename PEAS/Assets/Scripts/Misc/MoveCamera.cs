using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float smoothness = 0.1f;

    public Transform topLimit;    // GameObject que representa el límite superior
    public Transform bottomLimit; // GameObject que representa el límite inferior

    private Vector3 targetPosition;
    private Vector3 velocity;
    private float cameraHeight, topLimitY, bottomLimitY;

    private void Start()
    {
        cameraHeight = 2f * Camera.main.orthographicSize;                                                       
        topLimitY = topLimit.position.y - cameraHeight / 2f;
        bottomLimitY = bottomLimit.position.y + cameraHeight / 2f;
    }

    private void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Calcular el desplazamiento de la posición objetivo
        float scrollDelta = scrollInput * scrollSpeed;
        float targetY = targetPosition.y + scrollDelta;



        // Limitar el movimiento en el eje Y
        targetY = Mathf.Clamp(targetY, bottomLimitY, topLimitY);
        targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

        // Aplicar suavidad al movimiento utilizando Lerp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);
    }
}