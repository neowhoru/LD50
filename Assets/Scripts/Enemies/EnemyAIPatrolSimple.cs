using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAIPatrolSimple : MonoBehaviour
{
    public Vector3 initialPosition;
    public float maxPos;
    public float minPos;
    public float distance = 2f;
    public float speed = 5;
    public int direction;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        direction = -1;
        maxPos = transform.position.x + distance;
        minPos = transform.position.x - distance;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = transform.rotation;

        if (!isDead)
        {
            switch (direction)
                    {
                        case -1:
                            // Moving Left
                            if( transform.position.x > minPos)
                            {
                                transform.rotation = new Quaternion(rotation.x, -180f, rotation.z , rotation.w);
                                _rigidbody2D.velocity = new Vector2(-speed,_rigidbody2D.velocity.y);
                            }
                            else
                            {
                                direction = 1;
                            }
                            break;
                        case 1:
                            //Moving Right
                            if(transform.position.x < maxPos)
                            {
                                transform.rotation = new Quaternion(rotation.x, 0f, rotation.z , rotation.w);
                                _rigidbody2D.velocity = new Vector2(speed,_rigidbody2D.velocity.y);
                            }
                            else
                            {
                                direction = -1;
                            }
                            break;
                    }
        }
        
    }

    public void IsDead()
    {
        direction = 0;
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}