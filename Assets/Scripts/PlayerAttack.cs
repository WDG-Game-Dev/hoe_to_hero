//using UnityEngine;

//public class PlayerAttack : MonoBehaviour
//{
//    [SerializeField] private float attackCooldown;
//    private Animator anim;
//    private PlayerMovement playerMovement;
//    private float cooldownTimer = Mathf.Infinity;



//    private void Awake()
//    {
//        anim = GetComponent<Animator>();
//        playerMovement = GetComponent<PlayerMovement>();
//    }

//    private void Update()
//    {
//        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
//            Attack();

//        cooldownTimer +=  Time.deltaTime;
//    }

//    private void Attack()
//    {
//        anim.SetTrigger("attack");
//        cooldownTimer = 0;
//    }


//}


using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attack2Cooldown;
    [SerializeField] private float attack3Cooldown;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private float cooldownTimer2 = Mathf.Infinity;
    private float cooldownTimer3 = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Handle attack 1 with left mouse button (mouse button 0)
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        // Handle attack 2 with right mouse button (mouse button 1)
        if (Input.GetMouseButton(1) && cooldownTimer2 > attack2Cooldown && playerMovement.canAttack())
            Attack2();

        // Handle attack 3 with left control key (CTRL)
        if (Input.GetKey(KeyCode.LeftControl) && cooldownTimer3 > attack3Cooldown && playerMovement.canAttack())
            Attack3();

        // Update cooldown timers
        cooldownTimer += Time.deltaTime;
        cooldownTimer2 += Time.deltaTime;
        cooldownTimer3 += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");  // Serangan pertama
        cooldownTimer = 0;
    }

    private void Attack2()
    {
        anim.SetTrigger("attack2");  // Serangan kedua
        cooldownTimer2 = 0;
    }

    private void Attack3()
    {
        anim.SetTrigger("attack3");  // Serangan ketiga (CTRL)
        cooldownTimer3 = 0;
    }
}
