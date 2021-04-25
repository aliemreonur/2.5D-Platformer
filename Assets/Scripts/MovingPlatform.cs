using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform _pointA, _pointB;
    [SerializeField] private bool _isReturning = false;
    private float _speed = 3f;

    void FixedUpdate()
    {
        if(!_isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed*Time.deltaTime);
        }
        else if(_isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed*Time.deltaTime);
        }

        if(transform.position == _pointA.position)
        {
            _isReturning = false;
        }
        else if(transform.position == _pointB.position)
        {
            _isReturning = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
