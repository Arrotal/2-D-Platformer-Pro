using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] private MeshRenderer _callButton;
    [SerializeField] private int _coinRequirement=8;
    [SerializeField] private Elevator _elevator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (other.GetComponent<Player>().CheckCoins() >= _coinRequirement)
                {
                    if (!_elevator.isMoving() && _callButton.material.color == Color.green)
                    {
                        _callButton.material.color = Color.red;
                        _elevator.CallElevator();
                    }
                    else if (!_elevator.isMoving())
                    {

                        _callButton.material.color = Color.green;
                        _elevator.CallElevator();
                    }
                }
                else
                {
                    Debug.Log("Not enough coins");
                }
            }
        }
    }
}
