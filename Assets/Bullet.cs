using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float range;
    int damage;
    public int speed = 20;

    void Start()
    {
        Destroy(gameObject, range / speed);
    }


    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<Ememy>().GetDamage(damage);
        }
        Destroy(gameObject);
    }
    public void SetRange(float r)
    {
        range = r;
    }

    public void SetDamage(int d)
    {
        damage = d;
    }
}
