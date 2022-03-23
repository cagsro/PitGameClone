using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 defaultScale=new Vector3(1f,1f,1f);
    public Vector3 maxScale = new Vector3(1.25f,0.5f,1.25f);
    public float lerp=0.01f;


    public float jumpStake=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("SpaceDown");
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, maxScale, lerp);
            StartCoroutine(StakeJump());
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(StakeJump());
            if (jumpStake <= 1)
            {
                Debug.Log("SpaceUp" + jumpStake);
                this.GetComponent<Rigidbody>().AddForce(0, 100 * jumpStake, 0);
            }
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, defaultScale, lerp * 20);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("JumpAAA");
            this.GetComponent<Rigidbody>().AddForce(0, 100, 0);
        }

    }
    public IEnumerator StakeJump()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Coroutine");
        jumpStake++;
    }
}
