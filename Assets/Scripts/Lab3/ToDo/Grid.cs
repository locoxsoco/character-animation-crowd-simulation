using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PathFinding;

public class Grid : FiniteGraph<GridCell, CellConnection, GridConnections>
{
	// Class that represent the finite graph corresponding to a grid of cells
	// There is a known set of nodes (GridCells), 
	// and a known set of connections (CellConnections) between those nodes (GridConnections)
	
	// Example Data 
	
	protected float xMin;
	protected float xMax;
	protected float zMin;
	protected float zMax;
	
	protected float gridHeight;
	
	protected float sizeOfCell;
	
	protected int numCells;
	protected int numRows;
	protected int numColumns;
	
	
	// Example Constructor function declaration
	public Grid(float minX, float maxX, float minZ, float maxZ, float cellSize, float height = 0) : base()
	{
		numRows = Mathf.CeilToInt( (maxZ - minZ) / cellSize);
		numColumns = Mathf.CeilToInt( (maxX - minX) / cellSize);
		float obstacleProbability = Random.Range(0.0f, 1.0f);
		bool obstacle = obstacleProbability < 0.2;
		for(int i=0; i<numRows;i++)
			for (int j = 0; i < numColumns; j++)
			{
				nodes.Add(new GridCell(i*numColumns+j,minX*j,minX*(j+1),minZ*i,minZ*(i+1),obstacle));
			}
		
		GridConnections gridConnections = new GridConnections();
		connections.Add(gridConnections);
		// horizontal connections
		for(int i=0; i<numRows;i++)
			for (int j = 0; j < numColumns-1; j++)
			{
				gridConnections.Add(new CellConnection(nodes[i*numColumns+j],nodes[i*numColumns+(j+1)]));
				gridConnections.Add(new CellConnection(nodes[i*numColumns+(j+1)],nodes[i*numColumns+j]));
			}
		// vertical connections
		for(int i=0; i<numRows-1;i++)
			for (int j = 0; j < numColumns; j++)
			{
				gridConnections.Add(new CellConnection(nodes[i*numColumns+j],nodes[(i+1)*numColumns+j]));
				gridConnections.Add(new CellConnection(nodes[(i+1)*numColumns+j],nodes[i*numColumns+j]));
			}
		
	}
	
	// You have basically to fill the base fields "nodes" and "connections", 
	// i.e. create your list of GridCells (with random obstacles) 
	// and then create the corresponding GridConnections for each one of them
	// based on where the obstacles are and the valid movements allowed between GridCells. 
	
	
	// TO IMPLEMENT
		
	
	
};
