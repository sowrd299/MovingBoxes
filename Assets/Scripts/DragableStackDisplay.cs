using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableStackDisplay : MonoBehaviour, ISnapHandler
{
	[SerializeField]
	Vector3 step = new Vector3(0, -0.3f, -0.1f);

	private Stack<SnappingDragable> dragables; 


    void Start()
    {
		dragables = new Stack<SnappingDragable>();	
    }

	public void HandleSnappedTo(SnappingDragable dragable){
		dragable.SnapPos = transform.position + step * dragables.Count;
		dragables.Push(dragable);
	}

}
