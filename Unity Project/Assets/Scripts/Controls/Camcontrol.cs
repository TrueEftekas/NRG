using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camcontrol : MonoBehaviour
{
    public float zoomSpeed = 1;
    public float moveSpeed = 0.1f;
    public Camera selectedCamera;
    public float MINSCALE = 2.0F;
    public float MAXSCALE = 5.0F;
    public float minPinchSpeed = 5.0F;
    public float varianceInDistances = 5.0F;
    private float touchDelta = 0.0F;
    private Vector2 prevDist = new Vector2(0, 0);
    private Vector2 curDist = new Vector2(0, 0);
    private float speedTouch0 = 0.0F;
    private float speedTouch1 = 0.0F;
    public MasterScript master;
    GameObject targetRoom;
    bool goIn;
    bool goOut;
    bool inside;
    Vector3 magnitude;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //zoom code
     //   if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) //if 2 fingers are moving on screen
     //{
     //       curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
     //       prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
     //       touchDelta = curDist.magnitude - prevDist.magnitude;
     //       speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
     //       speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
     //       if ((touchDelta + varianceInDistances <= 1) && (speedTouch0 >  minPinchSpeed) && (speedTouch1 > minPinchSpeed))
     //    {
     //           selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + (1 * zoomSpeed), 15, 90);
     //       }
     //       if ((touchDelta + varianceInDistances > 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
     //    {
     //           selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView - (1 * zoomSpeed), 15, 70);
     //       }
     //   }
     //   //move code
     //   if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && (Input.GetTouch(0).deltaPosition.magnitude/Input.GetTouch(0).deltaTime) > minPinchSpeed) //
     //   {
     //       Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
     //       selectedCamera.transform.Translate(-touchDeltaPosition.x * moveSpeed * (selectedCamera.fieldOfView / 60), -touchDeltaPosition.y * moveSpeed *(selectedCamera.fieldOfView/60), 0);
     //       selectedCamera.transform.position = new Vector3(
     //           Mathf.Clamp(selectedCamera.transform.position.x, -15, 27), //min and max X
     //           Mathf.Clamp(selectedCamera.transform.position.y, -5, 5), // min and max Y
     //           selectedCamera.transform.position.z);
     //   }
        //tap code
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition); //create a ray that detects objects with colliders
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit)) //if I hit
            {
                if (raycastHit.collider.CompareTag("Room") && !goIn && !inside &&!goOut) {
                    goIn = true;
                    targetRoom = raycastHit.collider.gameObject;
                    magnitude = targetRoom.transform.position - selectedCamera.transform.position;
                }
                if (raycastHit.collider.CompareTag("Interactable")) // if it is tagged as interactable
                {
                    GameObject offed = GameObject.FindGameObjectWithTag("BroButton"); // then find the active button that opens up the answer, if it exists
                    if (offed != null) //if it does
                    {
                        offed.SetActive(false); // turn it off
                    }
                    raycastHit.collider.transform.Find("Button").gameObject.SetActive(true); //Set the child button of the object in question to active
                    master.inQuestion = raycastHit.collider.gameObject; //set the object in question in the master script appropriately
                }
               else if (raycastHit.collider.CompareTag("BroButton")) // if tagged as answer button
                {
                    master.openAnswer();// open the answer panel
                    GameObject offed = GameObject.FindGameObjectWithTag("BroButton"); //deactivate the button that open the answer panel
                    if (offed != null)
                    {
                        offed.SetActive(false);
                    }
                }  
            }
            else //if I did not tap anything with a collider
            {
                GameObject offed = GameObject.FindGameObjectWithTag("BroButton"); //deactivate the active button, if any
                if (offed != null)
                {
                    offed.SetActive(false);
                }
            }
           
        }
        if (goIn)
        {
            timer += Time.deltaTime;
            transform.Translate(new Vector3(magnitude.x * Time.deltaTime * 2.7f, magnitude.y * Time.deltaTime * 2.7f, 0));
            selectedCamera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(selectedCamera.GetComponent<Camera>().fieldOfView - (28 * Time.deltaTime * 2.7f), 32, 60);
            if (timer >= (1.0/2.7f))
            {
                goIn = false;
                targetRoom.GetComponent<Collider>().enabled = false;
                timer = 0;
                inside = true;
                GameObject.Find("Canvas").transform.Find("BackButton").gameObject.SetActive(true);
                transform.position = new Vector3(targetRoom.transform.position.x, targetRoom.transform.position.y, transform.position.z);
            }
        }
        if (goOut)
        {
            timer += Time.deltaTime;
            transform.Translate(new Vector3(magnitude.x * Time.deltaTime *2.7f, magnitude.y * Time.deltaTime *2.7f, 0));
            selectedCamera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(selectedCamera.GetComponent<Camera>().fieldOfView + (28 * Time.deltaTime *2.7f), 32, 60);
            if (timer >= (1.0 / 2.7f))
            {
                goOut = false;
                targetRoom.GetComponent<Collider>().enabled = true;
                timer = 0;
                transform.position = new Vector3(0.5f, 2, -10);
            }
        }
    }
    public void outButton()
    {
        goOut = true;
        magnitude = new Vector3(0.5f,2,-10) - selectedCamera.transform.position;
        inside = false;
        GameObject.Find("Canvas").transform.Find("BackButton").gameObject.SetActive(false);
    }

}
