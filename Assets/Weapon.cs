using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 1;
    public LayerMask whatToHit;
    ArmRotator armRotator;

    float timeToFire = 0;
    Transform firePoint;
    [SerializeField] float range;
    [SerializeField] int damage;

    [SerializeField] Transform bulletPrefab;
    [SerializeField] bool isRay;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null) Debug.LogError("파이어포인트 없다고");

        armRotator = transform.parent.GetComponent<ArmRotator>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
               Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        Effect();

        if (isRay)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, armRotator.GetVector2Drection(), range, whatToHit);
            if (hit.collider != null && hit.transform.gameObject.tag == "Enemy")
            {
                hit.transform.GetComponent<Ememy>().GetDamage(damage);
            }
        }
    }

    void Effect()
    {
        Bullet bulletEffect = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
        bulletEffect.SetRange(range);

        if(!isRay) bulletEffect.SetDamage(damage);
    }
}
