using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    float x;
    Rigidbody2D rb;
    [SerializeField] Transform body;
    [SerializeField] Transform arm;

    [SerializeField] int health;
    int oriHealth;
    [SerializeField] Text healthText;
    [SerializeField] RectTransform healthBar;

    bool facingright = true;
    bool jumping = false;

    Animator anim;

    [SerializeField] GameObject[] weapons;
    int index;


    void Start()
    {
        speed = 3;
        jumpForce = 8;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        oriHealth = health;
        SetStatus();
    }


    void Update()
    {
        x = Input.GetAxisRaw("Horizontal") * speed;
        anim.SetFloat("Speed", Mathf.Abs(x));

        if (x > 0 && !facingright) Flip();

        else if (x < 0 && facingright) Flip();

        transform.Translate(Vector3.right * x * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(0);
        if(Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(1);
        
    }

    void Flip()
    {
        facingright = !facingright;
        Vector3 bS = body.localScale;
        Vector3 aS = arm.localScale;
        body.localScale = new Vector3(-bS.x, bS.y, bS.z);
        arm.localScale = new Vector3(aS.x, -aS.y, aS.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && transform.position.y > collision.transform.position.y)
        {
            jumping = false;
            anim.SetBool("Jumping", jumping);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumping = true;
            anim.SetBool("Jumping", jumping);
        }
    }

    void ChangeWeapon(int i)
    {
        weapons[index].SetActive(false);
        weapons[i].SetActive(true);
        index = i;
    }

    public void GetDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            WaveSpawner.waveSpawner.enabled = false;
            Destroy(this.gameObject, 0.5f);
        }
        SetStatus();
    }

    public void SetStatus()
    {
        healthText.text = health.ToString() + " / " + oriHealth.ToString() + "HP";
        healthBar.localScale = new Vector3((float)health / (float)oriHealth, 1, 1);

    }
}
