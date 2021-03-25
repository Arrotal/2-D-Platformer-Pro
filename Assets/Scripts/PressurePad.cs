using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            float _distanceBetweenBoxes = Vector3.Distance(transform.position, other.transform.position);
            if (_distanceBetweenBoxes < .1f)
            {
                Rigidbody box = other.GetComponent<Rigidbody>();
                if(box !=null)
                box.isKinematic = true;
                MeshRenderer plateColor = GetComponentInChildren<MeshRenderer>();
                if(plateColor!=null)
                    plateColor.material.color = Color.green;
                Destroy(this);
            }
        }
    }
}
