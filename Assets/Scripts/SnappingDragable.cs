using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface ISnapHandler {
	// Called when the snap spot is snapped to; returns the position the
	// 		object should snap at
	public void HandleSnappedTo(SnappingDragable dragable);
}


public class SnappingDragable : Dragable
{

	public Vector3? SnapPos{
		get; set;
	}
	private LayerMask layerMask;
	ContactFilter2D filter; 

	void Start(){
		// TODO: Maybe this should be editable from the inspector?
		layerMask = LayerMask.GetMask("SnapSpot");
		filter = new ContactFilter2D();
		filter.SetLayerMask(layerMask);
	}

	// At end of drag, will try to snap to a spot
    protected override void endDrag(Vector3 atPos)
    {
		base.endDrag(atPos);
		Collider2D hitCollider = getHitCollider(atPos);
		// TODO: This might not want to be the only spot we raycast from...
			
		if(hitCollider != null){
			//Debug.Log("Snapping to " + hit.collider.gameObject.name);
			ISnapHandler[] handlers = hitCollider.GetComponents<ISnapHandler>();
			foreach(ISnapHandler handler in handlers){
				handler.HandleSnappedTo(this);
			}
			SnapPos = hitCollider.transform.position;
		}
		if(SnapPos != null){
			transform.position = (Vector3)SnapPos;
		}
    }

	// Returns the collider that this should be considered to have been dragged to
	private Collider2D getHitCollider(Vector3 atPos){
        RaycastHit2D mouseHit = Physics2D.Raycast(atPos, Vector2.zero, Mathf.Infinity, layerMask);
		if(mouseHit.collider != null){
			return mouseHit.collider;
		}

		Collider2D[] overlappedColliders = new Collider2D[1];
		if(GetComponent<Collider2D>().OverlapCollider(filter, overlappedColliders) > 0){
			return overlappedColliders[0];
		}

		return null;
	}

}
