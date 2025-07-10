using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask bossLayer;
    [SerializeField] private LayerMask slimeLayer;
    [SerializeField] private float attackDamage = 10f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;


    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    void Update()
    {
        m_timeSinceAttack += Time.deltaTime;

        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        // Grounded check
        bool wasGrounded = m_grounded;
        m_grounded = m_groundSensor.State();
        m_animator.SetBool("Grounded", m_grounded);

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        if (!m_rolling)
            m_body2d.linearVelocity = new Vector2(inputX * m_speed, m_body2d.linearVelocity.y);

        m_animator.SetFloat("AirSpeedY", m_body2d.linearVelocity.y);

        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        // ========== INPUT ==========

        // Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
        // Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
        {
            m_animator.SetTrigger("Hurt");
        }
        // Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;
            if (m_currentAttack > 3) m_currentAttack = 1;
            if (m_timeSinceAttack > 1.0f) m_currentAttack = 1;

            m_animator.SetTrigger("Attack" + m_currentAttack);
            m_timeSinceAttack = 0.0f;

            TryAttackBoss();
            TryAttackSlime();
        }
        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
        }
        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.linearVelocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.linearVelocity.y);
        }
        // Jump (✅ hanya 1 kali)
        else if (Input.GetKeyDown("space") && m_groundSensor.State() && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", false);
            m_body2d.linearVelocity = new Vector2(m_body2d.linearVelocity.x, m_jumpForce);

            // Disable sensor sementara agar tidak mendeteksi tanah terlalu cepat
            m_groundSensor.Disable(0.2f);
        }

        // Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        // Idle
        else
        {
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }
    private void TryAttackBoss()
    {
        Collider2D[] hitBosses = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, bossLayer);
        foreach (Collider2D boss in hitBosses)
        {
            BossHealth bossHealth = boss.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
                Debug.Log("Boss terkena serangan player");
            }
        }
    }

    private void TryAttackSlime()
    {
        Collider2D[] hitSlimes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, slimeLayer);
        foreach (Collider2D slime in hitSlimes)
        {
            SlimeHealth slimeHealth = slime.GetComponent<SlimeHealth>();
            if (slimeHealth != null)
            {
                slimeHealth.TakeDamage(attackDamage);
                Debug.Log("Slime terkena serangan player!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void AE_SlideDust()
    {
        Vector3 spawnPosition = m_facingDirection == 1 ? m_wallSensorR2.transform.position : m_wallSensorL2.transform.position;
        if (m_slideDust != null)
        {
            GameObject dust = Instantiate(m_slideDust, spawnPosition, transform.localRotation) as GameObject;
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}
