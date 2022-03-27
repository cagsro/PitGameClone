using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float radius;
    public bool isInside = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        
        Vector3 objPos = player.transform.position;
        Vector3 origin = transform.position;
        float distance = Vector3.Distance(objPos,origin);
        Gizmos.DrawWireSphere(origin, radius);
        if (distance<radius)
        {
            isInside = true;
        }
        else
        {
            isInside = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player")
        {
            if(other.GetComponent<Player>().isSmash)
            {
                Physics(false);
                this.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                Destroy(other.gameObject);
            }
            
        }
    }
    public void Physics(bool value)
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childrensPhysics in rb)
        {
            childrensPhysics.isKinematic = value;
            childrensPhysics.useGravity = value;
        }
    }
}
