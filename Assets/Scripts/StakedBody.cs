using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakedBody : MonoBehaviour
{
    public int myOrder;
    public Transform head;

    // Start is called before the first frame update
    void Start()
    {
        head = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        for(int i=0;i<head.GetComponent<Player>().BodyParts.Count;i++)
        {
            if(gameObject==head.GetComponent<Player>().BodyParts[i].gameObject)
            {
                myOrder = i;
            }
        }
    }
    private Vector3 movementVelocity;
    [Range(0.0f, 1.0f)]
    public float overTime = 0.5f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (myOrder == 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.position, ref movementVelocity, overTime);
            transform.LookAt(head.transform.position);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, head.GetComponent<Player>().BodyParts[myOrder - 1].transform.position, ref movementVelocity, overTime);
            transform.LookAt(head.transform.position);
        }
    }
    

}
