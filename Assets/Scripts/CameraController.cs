using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 velocity = Vector3.zero;
    private Transform target;

    private void Start()
    {
        // Menetapkan target kamera (pemain) sebagai target default
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target != null)
        {
            // Membuat kamera bergerak menuju posisi target
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);
        }
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        // Menggerakkan kamera ke posisi ruangan baru
        Vector3 newRoomPosition = new Vector3(_newRoom.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newRoomPosition, ref velocity, speed);
    }
}
