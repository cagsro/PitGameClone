using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakedBody : MonoBehaviour
{
    public Vector3 defaultScale = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 maxScale = new Vector3(2f, 0.5f, 2f);
    public float lerp = 0.02f;
    public bool holdButton;
    public bool onGround;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        /*if (onGround)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                holdButton = false;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (!holdButton)
                {
                    //Debug.Log("SpaceDown");
                    holdButton = true;
                }
                //Debug.Log("SpaceHoldDown");
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, maxScale, lerp);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //Debug.Log("KeyUp");
                holdButton = false;
            }
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, defaultScale, lerp * 7);
        }*/
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
        if (other.transform.tag == "topBlock")
        {
            Debug.Log("HealthPAarent");
            other.GetComponent<Enemy>().health--;
        }
        if (other.transform.tag == "Road")
        {
            StartCoroutine(Wait1Second());
        }
    }
    public IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(0.2f);
        onGround = true;
    }
}
