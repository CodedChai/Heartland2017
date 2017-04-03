using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public CharacterType characterType;//archetype/movesets
    float speed;
    int maxHP;
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
        speed = characterType.GetMoveSpeed();
        maxHP = characterType.GetHP();
        hp = maxHP;
        prevPos = transform.position;

        hp = maxHP;
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
                //go back to default char
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
            np.name = name;
            np.health = health;
            Destroy(bullet);
            Destroy(this);


        }
    }

    private IEnumerator Hypno()
    {

        //get aim dir

        float startup = .1f;
        yield return new WaitForSeconds(startup);

        //create projectile and set its position and parents
        Destroy(bullet);
        bullet = Instantiate(hypno, transform);
        
        bullet.transform.eulerAngles = characterType.rotationTrans.eulerAngles;
        bullet.transform.SetParent(null);
        yield return null;

    }

    void UpdateUI()
    {
        health.text = hp+ "/" + maxHP;
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
        transform.Translate(new Vector3(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime));
    }

    //record pos.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = prevPos;
    }

    void UpdateAnimationVars() {
      //animator.SetFloat("x_mov", speed * Input.GetAxis("Horizontal"));
      //animator.SetFloat("y_mov", speed * Input.GetAxis("Vertical"));
    }


}
