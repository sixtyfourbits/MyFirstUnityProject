using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotator : MonoBehaviour
{
    Vector3 difference;
    float rotZ;
    void Start()
    {
        
    }

    void Update()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // 팔과 마우스의 위치를 사용해서 방향벡터를 구한다.
        difference.Normalize(); // 벡터의 크기를 1로 줄어줌 & 방향유지
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // difference 벡터의 y와 x를 사용해서 각도를 구한다
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ); // 구한 각도 적용 (Quaternion.Euler = 걍)
    }

    public Vector2 GetVector2Drection()
    {
        return new Vector2(difference.x, difference.y);
    }
}
