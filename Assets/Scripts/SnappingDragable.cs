using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingDragable : Dragable
{

	public Transform SnapSpot{
		get; set;
	}
	private LayerMask layerMask;

	void Start(){
		// TODO: Maybe this should be editable from the inspector?
		layerMask = LayerMask.GetMask("SnapSpot");
	}

	// At end of drag, will try to snap to a spot
    protected override void endDrag(Vector3 atPos)
    {
		base.endDrag(atPos);
		// TODO: This might not want to be the only spot we raycast from...
        RaycastHit2D hit = Physics2D.Raycast(atPos, Vector2.zero, Mathf.Infinity, layerMask);
		if(hit.collider != null){
			//Debug.Log("Snapping to " + hit.collider.gameObject.name);
			SnapSpot = hit.collider.transform;
		}
		if(SnapSpot != null){
			transform.position = SnapSpot.position;
		}
    }

}
