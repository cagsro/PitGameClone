using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 minScale;
    public Vector3 maxScale;
    public bool repeatable;
    public float speed = 2f;
    public float duration = 5f;
    IEnumerator Start()
    {
        minScale = transform.localScale;
        while(repeatable)
        {
            yield return RepeatLerp(minScale, maxScale, duration);
            yield return RepeatLerp(maxScale, minScale, duration);
        }
    }
    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while(i<1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }




    public GameObject player;
    public float radius;
    public bool isInside = false;
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

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
                other.GetComponent<Player>().smashSize -= 1;
                Physics(false);
                this.GetComponent<BoxCollider>().enabled = false;
                DisabledMesh();
                speed = 0;
            }
            else if(this.transform.tag=="Enemy")
            {
                Destroy(other.gameObject);
            }
            else
            {
                return;
            }
        }
    }
    public void Physics(bool value)
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody childrensPhysics in rb)
        {
            childrensPhysics.isKinematic = value;
            childrensPhysics.AddExplosionForce(3000, transform.position, 2.5f);
            //childrensPhysics.useGravity = value;
        }
    }
    public void DisabledMesh()
    {
        Collider[] mesh = this.GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider childrensMesh in mesh)
        {
            childrensMesh.enabled = false;
        }
    }
}
