using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBullet : BulletBase
{

    public Transform target;
    private float speed=4.0f;
    private Vector2 m_MoveVec;
    private GameObject item;
    //private float speedCenter;
    //private float speedOneGround;
    //private float speedTwoGround;
    //float x = 0;
    private void Start()
    {
        m_MoveVec = target.position - transform.position;
        m_MoveVec.Normalize();

    
    }
    private void Update()
    {
        BulletUpdata();

        SpeedUpdata();
    }

    private void SpeedUpdata()
    {

    }

   
    void BulletUpdata()
    {
           transform.Rotate(new Vector3(0, 0, 1 * Time.deltaTime), 5.0f);
           transform.Translate(m_MoveVec * Time.deltaTime*speed,Space.World);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
