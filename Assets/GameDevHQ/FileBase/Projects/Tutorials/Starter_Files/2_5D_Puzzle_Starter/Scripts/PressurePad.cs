using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Moveable")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb == null)
            {
                Debug.LogError("Pad could not reach the movable box's Rigidbody");
            }
            else
            {
                float distance = Vector3.Distance(transform.position, other.transform.position);
                if (distance < 0.05f)
                {
                    MeshRenderer _display = GetComponentInChildren<MeshRenderer>();
                    if (_display == null)
                    {
                        Debug.LogError("Pressure pad could not get the child's material");
                    }
                    rb.isKinematic = true;
                    _display.material.color = Color.green;
                    Destroy(this);
                }
            }
        }
    }
}
