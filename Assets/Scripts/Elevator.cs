using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _targetA, _targetB;
    private float _speed = 3.0f;
    private bool _moving,_down;

    void Start()
    {
        _moving = false;
        _down = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moving)
        {
            if (_down)
            {
                if (transform.position.y <= _targetB.position.y)
                {
                    _moving = false;
                    _down = false;
                }
                
                else{
                    transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
                }
            }
            if (!_down)
            {
                if (transform.position.y >= _targetA.position.y)
                {
                    _moving = false;
                    _down = true;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);

                }
            }
        }
    }
    public void CallElevator()
    {
        _moving = true;
        
    }
    public bool isMoving()
    {
        return _moving;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
