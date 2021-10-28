using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dragable : MonoBehaviour
{

	[SerializeField]
	private float dragCoefficient = 1;
	private bool dragging = false;
	// the offset between the position of the object
	//		and the point by which it is being dragged
	private Vector3 dragOffset; 

	private Vector3 cachedVelocity; // the velocity of the object before being dragged


	public readonly UnityEvent DragEnded = new UnityEvent();


    void FixedUpdate()
    {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(Input.GetButtonDown("Fire1") && isTouchingPos(mousePos)){
			startDrag(mousePos);
		}else if(dragging){
			if(!Input.GetButton("Fire1")){
				endDrag(mousePos);
			}else{
				dragTowards(mousePos);
			}
		}
    }


	// Returns if the given world pos is over the object
	protected bool isTouchingPos(Vector3 atPos){
		RaycastHit2D hit = Physics2D.Raycast(atPos, Vector2.zero);
		return hit.collider == GetComponent<Collider2D>();
	}

	// Begins a drag from the given world point
	protected virtual void startDrag(Vector3 atPos){
		dragging = true;
		dragOffset = transform.position - atPos;
		cachedVelocity = GetComponent<Rigidbody2D>().velocity;
	}

	// Drags towards the given world point
	protected virtual void dragTowards(Vector3 targetPos){
		Vector3 diff = targetPos - transform.position + dragOffset;
		// TODO: I feel like there is actually a more efficient way to calculate this...
		GetComponent<Rigidbody2D>().velocity = diff.normalized * diff.sqrMagnitude * dragCoefficient;
	}

	// ends the drag with the object at the given world point
	protected virtual void endDrag(Vector3 atPos){
		// handle the case where the player moved the mouse too fast for "pritty drag"
		Vector3 pos = atPos + dragOffset;
		if(!isTouchingPos(pos)){ 
			transform.position = pos;
		}
		GetComponent<Rigidbody2D>().velocity = cachedVelocity;
		dragging = false;
		//DragEnded.Invoke();
	}

}
