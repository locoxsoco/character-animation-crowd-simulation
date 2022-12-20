using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PathFinding;

public class GridCell : Node 
{
	public GridCell(int i, float minX, float maxX, float minZ, float maxZ, bool isOccupied):base(i) {
		// TO IMPLEMENT
		xMin = minX;
		xMax = maxX;
		zMin = minZ;
		zMax = maxZ;
		occupied = isOccupied;
		center = new Vector3((xMax + xMin)/2, 0, (zMax + zMin)/2);
	}
	public GridCell(GridCell n):base(n) {
		// TO IMPLEMENT
		xMin = n.xMin;
		xMax = n.xMax;
		zMin = n.zMin;
		zMax = n.zMax;
		occupied = n.occupied;
		center = n.center;
	}

	public bool Occupied => occupied;

	// Your class that represents a grid cell node derives from Node

	// You add any data needed to represent a grid cell node

	// EXAMPLE DATA
	
	protected float xMin;
	protected float xMax;
	protected float zMin;
	protected float zMax;

	protected bool occupied;

	protected Vector3 center;
	

	// You also add any constructors and methods to implement your grid cell node class

	// TO IMPLEMENT
};
