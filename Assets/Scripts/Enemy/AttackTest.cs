using UnityEngine;

public class AttackTest : MonoBehaviour
{
    private Animator animator;
    public float attackDelay = 2f;
    private float timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        timer = attackDelay;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackDelay)
        {
            if (animator != null && animator.isActiveAndEnabled)
            {
                Debug.Log("Tes: Memutar animasi AttackGolem");
                animator.Play("AttackGolem");  // pastikan ini nama state di Animator
            }
            timer = 0f;
        }
    }
}
