using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    //Stores the trigger on the player that detects the ball
    private SphereCollider ballCollider;
    //Stores the collider of the hit ball
    private List<Collider> ballCollisionList = new List<Collider>();
    private Collider actualBallCollider;
    // Start is called before the first frame update
    void Start()
    {
        ballCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Press grab button to grab ball
        if(Input.GetButtonDown("Fire2"))
        {
            //List not empty
            if (ballCollisionList.Count > 0)
            {
                //Grab ball
                actualBallCollider = ballCollisionList[0];
                actualBallCollider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                actualBallCollider.gameObject.transform.parent.gameObject.transform.parent = gameObject.transform;
                Debug.Log("Cacat");
            }
            Debug.Log("Pisat");
        }
        //Release button to let go
        if (Input.GetButtonUp("Fire2"))
        {
            if (actualBallCollider!=null)
            {
                actualBallCollider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                actualBallCollider.gameObject.transform.parent.gameObject.transform.parent = null;
                //Impart player velocity to the ball
                actualBallCollider.gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<CharacterController>().velocity;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ball")
        {
            ballCollisionList.Add(collider);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "ball")
        {
             //Grab ball
              //  ballCollisionList[0].gameObject.GetComponent<Rigidbody>().isKinematic = false;
              //  ballCollisionList[0].gameObject.transform.parent.gameObject.transform.parent = null;
            ballCollisionList.Remove(collider);
        }
    }
}
