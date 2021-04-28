using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    Material _callButtonMat;
    Player _player;
    Elevator elevator;
    // Start is called before the first frame update
    void Start()
    {
        _callButtonMat = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        elevator = FindObjectOfType<Elevator>().GetComponent<Elevator>();
        if(_player == null)
        {
            Debug.LogError("elevator panel could not reach to the player");
        }
            if(_callButtonMat == null)
        {
            Debug.Log("Child object's material could not be reached");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && _player.Coins >= 8)
            {
                if(!elevator.elevatorCalledDown)
                {
                    _callButtonMat.color = Color.green;
                    elevator.elevatorCalledDown = true;
                    Debug.Log("Elevator going down");
                }
                else
                {
                    elevator.elevatorCalledDown = false;
                    _callButtonMat.color = Color.red;
                    Debug.Log("Elevator going up");
                }
            }
        }
    }
}
