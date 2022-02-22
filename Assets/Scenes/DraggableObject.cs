using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;
    public float maxSpeed = 10;
    Vector2 mouseForce;
    Vector3 lastPosition;
    
    void Update()
    {
        //Ask the camera to convert the x,y coordintes of the screen we clicked on to
        //x,y,z coordinates in the world, so we know when the mouse is in our game.
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(selectedObject)
        {
            mouseForce = (mousePosition - lastPosition)/Time.deltaTime;
            mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
            lastPosition = mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //create an overlap circle where the mouse is.
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            //did it hit the collider we put on the green circle?
            if (targetObject)
            {
                //if so get the object it hit
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                //this code makes it so the circle does not snap to the mouse position
                //but rather it looks like the circle sticks to the mouse wherever clicked
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {   //when the mouse up let go of the object
            selectedObject = null;
        }
    }
    void FixedUpdate()
    {
        //if something was hit by the collision above and selected
        //but this fires anytime the mouse is moved and we have not let go yet
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }
}
