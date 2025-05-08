//using UnityEngine;
//using UnityEngine.InputSystem;

//public class InteractionDetector : MonoBehaviour
//{
//    private IInteractable interactableInRange = null;
//    public GameObject interactionIcon;

//    private void Start()
//    {
//        interactionIcon.SetActive(false);
//    }

//    public void OnIntercat(InputAction.CallbackContext context)
//    {
//        if(context.performed)
//        {
//            interactableInRange?.Interact();
//        }
//    }    

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
//        {
//            interactableInRange = interactable;
//            interactionIcon.SetActive(true);
//        }    
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
//        {
//            interactableInRange = null;
//            interactionIcon.SetActive(false);   
//        }
//    }
//}


using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;

    private void Start()
    {
        interactionIcon.SetActive(false);  // Pastikan ikon interaksi tidak muncul di awal
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interacting with the paper.");
            interactableInRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
            Debug.Log("Interaction range entered.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
            Debug.Log("Interaction range exited.");
        }
    }

}
