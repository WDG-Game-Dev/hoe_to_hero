using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom);  // Jika pemain masuk ke pintu dari kiri, pindah ke nextRoom
            else
                cam.MoveToNewRoom(previousRoom);  // Jika pemain masuk ke pintu dari kanan, pindah ke previousRoom
        }
    }
}
