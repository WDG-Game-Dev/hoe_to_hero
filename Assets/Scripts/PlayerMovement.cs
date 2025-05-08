//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{

//    [SerializeField] private float speed;
//    [SerializeField] private float jumpPower;
//    [SerializeField] private LayerMask groundLayer;
//    [SerializeField] private LayerMask wallLayer;
//    private Rigidbody2D body;
//    private Animator anim;
//    private BoxCollider2D boxCollider;
//    private float wallJumpCooldown;
//    private int horizontalInput;

//    private void Awake()
//    {
//        // Untuk refrensi rigidbody dan animator dari objek 
//        body = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();
//        boxCollider = GetComponent<BoxCollider2D>();
//    }

//    private void Update()
//    {
//        horizontalInput = (int)Input.GetAxisRaw("Horizontal");
//        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

//        if (horizontalInput > 0)
//            transform.localScale = new Vector3(2, 2, 2);
//        else if (horizontalInput < 0)
//            transform.localScale = new Vector3(-2, 2, 2);

//        if (Input.GetKey(KeyCode.Space))
//            Jump();

//        anim.SetBool("run", horizontalInput != 0);
//        anim.SetBool("grounded", isGrounded());

//        if (wallJumpCooldown < 0.2f)
//        {
//            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

//            if (onWall() && !isGrounded())
//            {
//                body.gravityScale = 0;
//                body.linearVelocity = Vector2.zero;
//            }
//            else body.gravityScale = 3;

//            if (Input.GetKey(KeyCode.Space))
//                Jump();
//        }
//        else
//            wallJumpCooldown += Time.deltaTime;
//    }

//    private void Jump()
//    {
//        if (isGrounded())
//        {
//            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
//            anim.SetTrigger("jump");
//        }
//        else if (onWall() && !isGrounded())
//        {
//            if (horizontalInput == 0)
//            {
//                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
//                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
//            }
//            else
//                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

//            wallJumpCooldown = 0;

//        }

//    }


//    private bool isGrounded()
//    {
//        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector3.down, 0.1f, groundLayer);
//        return raycastHit.collider != null;
//    }

//    private bool onWall()
//    {
//        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
//        return raycastHit.collider != null;
//    }

//    public bool canAttack()
//    {
//        return horizontalInput == 0 && isGrounded() && !onWall();   
//    }
//}



//////////////////////////////////////////////////////////////////////////////
///



using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private int horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = (int)Input.GetAxisRaw("Horizontal");

        // Menggunakan linearVelocity daripada velocity
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Mengatur skala karakter berdasarkan arah pergerakan (kiri/kanan)
        if (!onWall())  // Jika karakter tidak menempel pada dinding
        {
            if (horizontalInput > 0)
                transform.localScale = new Vector3(2, 2, 2);  // Arah kanan
            else if (horizontalInput < 0)
                transform.localScale = new Vector3(-2, 2, 2);  // Arah kiri
        }

        // Pengecekan input untuk lompatan
        if (Input.GetKey(KeyCode.Space))
            Jump();

        // Update animasi gerakan
        anim.SetBool("run", horizontalInput != 0);  // "run" adalah parameter di Animator
        anim.SetBool("grounded", isGrounded());

        // Menambahkan animasi wall sliding
        if (onWall() && !isGrounded())  // Jika karakter berada di tembok dan tidak di tanah
        {
            body.gravityScale = 0;  // Menonaktifkan gravitasi agar karakter tidak jatuh dengan cepat
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Clamp(body.linearVelocity.y, -2f, 0f));  // Membatasi kecepatan jatuh
            anim.SetBool("wallSliding", true);  // Memulai animasi wall sliding
        }
        else
        {
            body.gravityScale = 3;  // Mengaktifkan gravitasi normal
            anim.SetBool("wallSliding", false);  // Menonaktifkan animasi wall sliding
        }

        // Menambahkan animasi Fall
        if (body.linearVelocity.y < 0 && !isGrounded()) // Jika kecepatan vertikal negatif (karakter jatuh)
        {
            anim.SetBool("fall", true);  // Aktifkan animasi fall
        }
        else
        {
            anim.SetBool("fall", false); // Nonaktifkan animasi fall ketika karakter berada di tanah atau tidak jatuh
        }

        // Wall jump logic
        if (wallJumpCooldown < 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
                body.gravityScale = 3;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
        HandleRolling();
        HandleBlock();
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector3.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(Mathf.Sign(transform.localScale.x), 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return isGrounded() && !onWall(); // Hanya di tanah dan tidak di dinding
    }

    private void HandleRolling()
    {
        // Memeriksa apakah pemain menekan tombol Shift untuk melakukan rolling
        if (Input.GetKeyDown(KeyCode.LeftShift) && horizontalInput != 0 && isGrounded())
        {
            anim.SetTrigger("rolling");  // Trigger animasi rolling
        }
    }

    private void HandleBlock()
    {
        // Memeriksa apakah pemain menekan tombol B untuk melakukan block
        if (Input.GetKeyDown(KeyCode.Q) && isGrounded())
        {
            anim.SetTrigger("block");  // Trigger animasi block
        }
    }
}