using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public CharacterType characterType;//archetype/movesets
    public GameObject og;
    public float speed;
    public int hp;
    public GameObject hypno;
    Vector3 prevPos;//to make physics stuff look a bit smoother with some bounceback

    public Text name;
    public Text health;
    public GameObject bullet;

    private Animator animator;

    // Use this for initialization
	void Start ()
    {
        if (characterType == null)
        {
            characterType = GetComponent<MindControlType>();
        }
        animator = GetComponent<Animator>();
        speed = characterType.GetMoveSpeed() + GlobalMoveSpeed.GetSpeedDelta();
        prevPos = transform.position;
        hp = characterType.GetHP();
        name.text = characterType.name;
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            characterType.Primary();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            characterType.Secondary();
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            characterType.Tertiary();
        }
        else if (Input.GetButtonDown("Hypno"))
        {
            if (characterType.name != "Hypno")
            {
                PlayerController np = og.GetComponent<PlayerController>();
                np.enabled = true;
                characterType.rotationTrans.SetActive(false);
                np.characterType.rotationTrans.SetActive(true);
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<BoxCollider2D>().enabled = false;
                np.name = name;
                np.name.text = np.GetComponent<CharacterType>().name;
                characterType.dead = true;//it should kill itself
                Destroy(this);
            }
            else
            {
                StartCoroutine(Hypno());
            }
        }

        UpdateUI();
        CheckState();

    }

    void CheckState()
    {
        if(bullet != null && bullet.GetComponent<MindProjectile>().nextHost != null)
        {
            GameObject next = bullet.GetComponent<MindProjectile>().nextHost;
            next.AddComponent<PlayerController>();
            PlayerController np = next.GetComponent<PlayerController>();
       
            np.characterType = next.GetComponent<CharacterType>();

            //turn aiming thingyf rom here to there
            np.characterType.rotationTrans.gameObject.SetActive(true);
            characterType.rotationTrans.gameObject.SetActive(false);
            np.og = og;
            np.hypno = hypno;
            next.GetComponent<PathFollower>().enabled = false;
            next.GetComponent<BasicAI>().enabled = false;
            next.tag = "Player";

            Destroy(bullet);
            np.name = name;
            np.health = health;
            enabled = false;


        }
    }

    private IEnumerator Hypno()
    {
        print("Trying to hypno");

        //get aim dir

        float startup = .1f;
        yield return new WaitForSeconds(startup);

        //create projectile and set its position and parents
        bullet = Instantiate(hypno, transform);
        
        bullet.transform.eulerAngles = characterType.rotationTrans.transform.eulerAngles;
        bullet.transform.SetParent(null);
        float time = 0;
        for(float x = 10; x < time; time += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return null;

    }

    void UpdateUI()
    {
        health.text = hp + "";
    }

    // Update movement on physics due to collisions
    void FixedUpdate()
    {
        prevPos = transform.position;
        Move();
        UpdateAnimationVars();
    }

    //basic movement. Might make this check to see if it's doing stuff but idk
    void Move()
    {
        transform.Translate(new Vector3((GlobalMoveSpeed.GetSpeedDelta() + speed) * Input.GetAxis("Horizontal") * Time.deltaTime, (GlobalMoveSpeed.GetSpeedDelta() + speed) * Input.GetAxis("Vertical") * Time.deltaTime));
    }

    //record pos.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = prevPos;
    }

    void UpdateAnimationVars() {
      animator.SetFloat("x_mov", (GlobalMoveSpeed.GetSpeedDelta() + speed) * Input.GetAxis("Horizontal"));
      animator.SetFloat("y_mov", (GlobalMoveSpeed.GetSpeedDelta() + speed) * Input.GetAxis("Vertical"));
    }


}
