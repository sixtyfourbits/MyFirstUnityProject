using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] float ufoSpeed;
    void Start()
    {
        ufoSpeed = 0.8f;
    }

    void Update()
    {
        transform.Translate(Vector3.left * ufoSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * 0.3f * Time.deltaTime);
    }
}
