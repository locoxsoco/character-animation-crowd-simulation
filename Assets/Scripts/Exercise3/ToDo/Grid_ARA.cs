using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PathFinding;

public class Grid_ARA : ARA<GridCell, CellConnection, GridConnections, Grid, GridHeuristic>
{
	// Class that implements the A* pathfinding algorithm	
	// over a Grid graph, componsed of GridCells and CellConnections
	// using GridHeuristic as the Heuristic function.

	// NOTHING TO DO HERE

	public Grid_ARA(int maxNodes, float maxTime, int maxDepth) : base(maxNodes, maxTime, maxDepth)
	{

	}
};