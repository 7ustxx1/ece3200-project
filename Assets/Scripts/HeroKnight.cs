using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    public GameObject boltPrefab;
    public GameObject waveformPrefab;
    public GameObject crossedPrefab;

    public ProgressBar healthBar;
    public float health = 100;
    public Transform rightAtteckPos;
    public Transform leftAtteckPos;
    public LayerMask whatIsEnemies;
    public float atteckRange;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private bool                isBlocking;
    private bool                m_attack = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 1;// default weapon is weapon 1
    private int                 jumpCount = 2;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_attackGap = 0.75f;// weapon 1's attack gap

    private bool acidHurt;
    private bool gazeHurt;
    private float petrifyTime = 3f;
    private float petrifyTimeLeft = 0f;


    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        healthBar.BarValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        healthBar.BarValue = health;
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            jumpCount = 2;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0 && !m_attack && petrifyTimeLeft <= 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputX < 0 && !m_attack && petrifyTimeLeft <= 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling && !isBlocking && !m_attack && petrifyTimeLeft <= 0)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        if (!m_rolling && !m_attack)
        {
            m_animator.SetBool("WallSlide", (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State()));
        }

        ////Death
        //if (Input.GetKeyDown("e") && !m_rolling)
        //{
        //    m_animator.SetBool("noBlood", m_noBlood);
        //    m_animator.SetTrigger("Death");
        //}

        ////Hurt
        //else if (Input.GetKeyDown("q") && !m_rolling)
        //{
        //    m_animator.SetTrigger("Hurt");
        //    health -= 10;
        //}
        //else if (Input.GetKeyDown("r"))
        //{
        //    health = 100;
        //}

        // change weapon
        else if (Input.GetKeyDown("1"))// weapon 1 ==> bolt
        {
            m_currentAttack = 1;
            m_attackGap = 0.75f;
        }
        else if (Input.GetKeyDown("2"))// weapon 2 ==> waveform
        {
            m_currentAttack = 2;
            m_attackGap = 0.5f;
        }
        else if (Input.GetKeyDown("3"))// weapon 3 ==> crossed
        {
            m_currentAttack = 3;
            m_attackGap = 1f;
        }

        //Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > m_attackGap && !m_rolling &&
                 !((m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State())))
        {
            // Change the stage to attack
            m_attack = true;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            Collider2D[] enemiseToDamage;
            if (m_facingDirection == 1)
            {
                enemiseToDamage = Physics2D.OverlapCircleAll(rightAtteckPos.position, atteckRange, whatIsEnemies);
                for (int i = 0; i < enemiseToDamage.Length; i++)
                {
                    enemiseToDamage[i].GetComponent<EnemyDamage>().TakeDamageNear();
                }
            }
            else if ((m_facingDirection == -1))
            {
                enemiseToDamage = Physics2D.OverlapCircleAll(leftAtteckPos.position, atteckRange, whatIsEnemies);
                for (int i = 0; i < enemiseToDamage.Length; i++)
                {
                    enemiseToDamage[i].GetComponent<EnemyDamage>().TakeDamageNear();
                }
            }

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            isBlocking = true;
        }

        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
            isBlocking = false;
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && m_grounded && petrifyTimeLeft <= 0)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }

        //Jump
        else if (Input.GetKeyDown("space") && !m_rolling && (m_grounded || jumpCount > 0) && !m_attack && petrifyTimeLeft <= 0)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
            jumpCount -= 1;
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }

        CheckAcidHurt();
        CheckGazedHurt();
    }

    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
    }

    // Called in end of attack animation.
    void AE_ResetAttack()
    {
        m_attack = false;
    }

    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    public void BoltAttack()
    {
        // get the spawn position according to the facing direction
        Transform t_spawn;
        if (m_facingDirection == 1)
        {
            t_spawn = transform.Find("RightBoltSpawnPos");
        }
        else
        {
            t_spawn = transform.Find("LeftBoltSpawnPos");
        }

        // instantiate a bolt
        GameObject t_bolt = Instantiate(boltPrefab, t_spawn.position, t_spawn.rotation) as GameObject;

    }
    public void WaveformAttack()
    {
        // get the spawn position according to the facing direction
        Transform t_spawn;
        if (m_facingDirection == 1)
        {
            t_spawn = transform.Find("RightWaveformSpawnPos");
        }
        else
        {
            t_spawn = transform.Find("LeftWaveformSpawnPos");
        }


        // instantiate a bolt
        GameObject t_bolt = Instantiate(waveformPrefab, t_spawn.position, t_spawn.rotation) as GameObject;
    }

    public void CrossedAttack()
    {
        // get the spawn position according to the facing direction
        Transform t_spawn;
        if (m_facingDirection == 1)
        {
            t_spawn = transform.Find("RightCrossedSpawnPos");
        }
        else
        {
            t_spawn = transform.Find("LeftCrossedSpawnPos");
        }


        // instantiate a bolt
        GameObject t_bolt = Instantiate(crossedPrefab, t_spawn.position, t_spawn.rotation) as GameObject;
    }

    void OnCollisionEnter2D(Collision2D bol)
    {
        if (bol.gameObject.tag == "small snake" && !isBlocking)
        {
            health -= 1;
            m_animator.SetTrigger("Hurt");
        }
        if (bol.gameObject.tag == "Tail")
        {
            health -= 10;
            m_animator.SetTrigger("Hurt");
        }
        
    }

    private void CheckAcidHurt()
    {
        if (acidHurt)
        {
            health -= 1;
            m_animator.SetTrigger("Hurt");
            
            acidHurt = false;
        }
    }

    void setacidHurt(bool hurt)
    {
        acidHurt = hurt;
    }

    private void CheckGazedHurt()
    {
        if (gazeHurt && !isBlocking && petrifyTimeLeft <= 0)
        {
            health -= 10;
            petrifyTimeLeft = petrifyTime;
            
            gazeHurt = false;
        }
        if(petrifyTimeLeft > 0)
        {
            petrifyTimeLeft -= Time.deltaTime;
            gazeHurt = false;
            m_animator.SetTrigger("Hurt");
        }
    }

    void setgazeHurt(bool hurt)
    {
        gazeHurt = hurt;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftAtteckPos.position, atteckRange);
        Gizmos.DrawWireSphere(rightAtteckPos.position, atteckRange);
    }

    public void PlayerDamage(float amount)
    {
        health -= amount;
        m_animator.SetTrigger("Hurt");
    }
}
