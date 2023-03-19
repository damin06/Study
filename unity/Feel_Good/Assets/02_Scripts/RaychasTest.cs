using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaychasTest : MonoBehaviour
{
    [SerializeField] private float Distance = 10;
    [SerializeField] LayerMask lay;
    Renderer mat;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RaycastT()
    {

    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Distance, lay))
        {
            if (hit.collider)
            {
                var chang = hit.collider.gameObject.GetComponent<Renderer>();
                chang.material.color = Color.black;
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            }


        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * Distance);
        }
    }
}
