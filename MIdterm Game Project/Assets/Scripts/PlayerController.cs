using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float speed = 100f; //how fast charcter walks
    public float jumpForce = 150f; //how much force when character jumps

    public bool grounded; //check if player is on platform

    private Rigidbody2D rigiBody; //gives access to the RigidBOdy2D of the controller gameobject

    private Animator anim; //gives acess to the Animator of the controller gameobject

    public AudioClip jumping; //jumping audio
    public AudioClip coinCollect;
    public AudioClip gemCollect;
    AudioSource audio; //audio source setup

    [SerializeField]
    private string newLevel; //credit to this video https://www.youtube.com/watch?v=QJBL9eHBsso


    public GameObject respawn;

    private GameMaster gm;

    public int health;
    public int maxHealth = 3;

    public AudioClip damageSfx;
    public bool damageTaken;
    public bool deathCheck;
    public bool hurt;

    //projectile
    public Transform bulletPoint;
    public GameObject bullet;

    public GameObject goOverlay;

    void Start()
    {
        rigiBody = gameObject.GetComponent<Rigidbody2D>(); //getting RigidBody2D info from gameobject
        anim = gameObject.GetComponent<Animator>(); //getting Animator info drom gameobject

        audio = GetComponent<AudioSource>(); //access audio source

        gm = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GameMaster>();

        goOverlay.SetActive(false);

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("max speed" + maxSpeed + ", speed:" + speed);
        anim.SetBool("IsGrounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rigiBody.velocity.x));
        anim.SetBool("IsDamage", damageTaken);
        anim.SetBool("IsAlive", deathCheck);

        float h = Input.GetAxis("Horizontal");

        //flipping character's sprite face to the correct direction on the x axis
        if (Input.GetAxis("Horizontal") < -.001f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        //Moving Character

        if (grounded)
        {
            rigiBody.AddForce((Vector2.right * speed) * h);
        }

        if (Input.GetAxis("Horizontal") > .001f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigiBody.AddForce(Vector2.up * jumpForce);
            audio.PlayOneShot(jumping, 1.0f);
        }

        if (!grounded)
        {
            speed = 300f;
        }

        else
        {
            speed = 400f;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            Debug.Log("Restart Scene");
            StartCoroutine("DelayedRestart");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        }

        if (Input.GetKeyDown(KeyCode.R) && deathCheck)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (!deathCheck)
        {
            Time.timeScale = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Kill Zone"))
        {
            transform.position = respawn.transform.position;
        }

        if (col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            audio.PlayOneShot(coinCollect, .45f);

            gm.points++;
        }

        if (col.CompareTag("Gem"))
        {
            Destroy(col.gameObject);
            audio.PlayOneShot(gemCollect, .45f);

            gm.points += 5;
        }

        if (col.CompareTag("Level 2 Trigger"))
        {
            SceneManager.LoadScene(newLevel);

            Debug.Log("Scene Changed");
        }
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rigiBody.velocity;
        easeVelocity.y = rigiBody.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        if (grounded)
        {
            rigiBody.velocity = easeVelocity;
        }

        if (rigiBody.velocity.x > maxSpeed)
        {
            rigiBody.velocity = new Vector2(maxSpeed, 0f);
        }

        if (rigiBody.velocity.x < -maxSpeed)
        {
            rigiBody.velocity = new Vector2(-maxSpeed, 0f);
        }
    }

    void Death()
    {
        deathCheck = true;

        goOverlay.SetActive(true);

        if (deathCheck)
        {
            Time.timeScale = 0;
        }
    }

    IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds(1);

        Death();

        Debug.Log("Player is Dead");
    }

    public void Damage(int dmg)
    {
        audio.PlayOneShot(damageSfx, 1.0f);
        health -= dmg;
    }
}
