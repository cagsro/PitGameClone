using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CameraShake cameraShake;
    public Vector3 defaultScale=new Vector3(1.5f,1.5f,1.5f);
    public Vector3 maxScale = new Vector3(2f,0.5f,2f);
    public float lerp=0.03f;
    public bool holdButton;
    public bool onGround=true;
    public float jumpStake=1;
    public bool isSmash;


    public float velocity;
    public float speed = 3f;
    public float smashSize;

    public ParticleSystem SmashParticle;
    public ParticleSystem SmokeParticle;
    public Transform parent;

    public Rigidbody rb;

    public GameObject topBlock;
    public float dist;
    RaycastHit nesne;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isSmash = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        if (onGround)
        {
            
            SmashParticle.Stop();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                holdButton = false;  
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (!holdButton)
                {
                    jumpStake = 0;
                    //Debug.Log("SpaceDown");
                    holdButton = true;
                    isSmash = false;
                    StartCoroutine(StakeJump());  
                }
                //Debug.Log("SpaceHoldDown");
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, maxScale, lerp);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //Debug.Log("KeyUp");
                holdButton = false;
                StopCoroutine(StakeJump());
                if (jumpStake >= 1)
                {
                    //Debug.Log("SpaceUp");
                    rb.AddForce(transform.up*100*jumpStake); 
                }
            }
        }

        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, defaultScale, lerp * 7);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Raycast();
                Debug.Log("yuseklik" + dist);
                SmashParticle.Play();
                SmashSize();   
            }
            if (Input.GetKey(KeyCode.Space))
            {
                velocity = rb.velocity.y;
                Debug.Log("max velo" + velocity);
                if (smashSize>=0)
                {
                    isSmash = true;
                    //Debug.Log("Smash");
                    rb.AddForce(-transform.up * 25, ForceMode.Acceleration);
                }
                else
                {
                    isSmash = false;
                }  
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isSmash = false;
                SmashParticle.Stop();
            }
        }
    }

    public IEnumerator StakeJump()
    {
        while(holdButton)
        {
            yield return new WaitForSeconds(0.02f);
            //Debug.Log("Coroutine" + jumpStake);
            jumpStake++;
            if (jumpStake >= 15) jumpStake = 15;
            if (jumpStake <= 5) jumpStake = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Road")
        {
            onGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Road")
        {
            
            if (isSmash)
            {
                
                CheckCubes();
                Vector3 pos = this.transform.position;
                Instantiate(SmokeParticle, pos, Quaternion.Euler(-90, 0, 0));
                StartCoroutine(cameraShake.Shake(.15f, .4f));
                SmashParticle.Stop();
                isSmash = false;
            }
            StartCoroutine(Wait1Second());
        }
        if (other.transform.tag == "Block")
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -41);
        }
        if (other.transform.tag == "Trampoline")
        {
            //Debug.Log("Trampoline");    
            rb.AddForce(transform.up * 1500);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.transform.tag == "Enemy")
        {
            if(isSmash)
            {
                other.GetComponent<Enemy>().Physics(false);
                //Physics(false);
                other.GetComponent<BoxCollider>().enabled = false;
                other.GetComponent<Enemy>().DisabledMesh();
                other.GetComponent<Enemy>().speed = 0;
                //Destroy(other.gameObject);
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -41);
            }
        }
        if (other.transform.tag == "topBlock")
        {
            if (isSmash)
            {
                /*velocity= rb.velocity.y;
                Debug.Log("" + velocity);*/
            }
        }
    }

    public IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(0.2f);
        onGround = true;
    }
    public void CheckCubes()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);
        foreach(Collider c in colliders)
        {
            if(c.GetComponent<Enemy>())
            {
                c.GetComponent<Enemy>().Physics(false);
                c.GetComponent<BoxCollider>().enabled = false;
                c.GetComponent<Enemy>().speed = 0;

                Rigidbody[] enemyrb = c.GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody childrensRB in enemyrb)
                {
                    childrensRB.AddExplosionForce(3000, new Vector3(transform.position.x+Random.Range(-2,+2), transform.position.y, transform.position.z + Random.Range(-2, +2)) +Vector3.down*2, 2.5f);
                }
                Collider[] mesh = c.GetComponentsInChildren<MeshCollider>();
                foreach (MeshCollider childrensMesh in mesh)
                {
                    childrensMesh.enabled = false;
                }
            }
        }
    }
    public void SmashSize()
    {
        smashSize =Mathf.Floor(dist);

    }
    public void Raycast()
    {
        if(Physics.Raycast(transform.position,-transform.up,out nesne,20.0f))
        {
            if (nesne.collider.gameObject.tag == "topBlock")
            {
                Debug.Log("En üstteki block" + nesne.collider.gameObject.name);
                dist = Vector3.Distance(this.transform.position, nesne.collider.gameObject.transform.position);
            }
        }
    }
}
