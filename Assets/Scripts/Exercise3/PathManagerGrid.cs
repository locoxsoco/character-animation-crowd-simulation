using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathManagerGrid : MonoBehaviour
{
    public Vector3 goalPos;
    public GridCell goal, start;
    public float distanceThreshold;
    public Grid grid;
    public List<GridCell> waypoints;
    public bool ARASearch;
    private Grid_A_Star _grid_a_star;
    private Grid_ARA _grid_ara;
    private GridHeuristic _grid_heuristic;
    private int found;
    
    // Start is called before the first frame update
    void Start()
    {
        if (grid != null)
        {
            int randomNodeId = Random.Range(0, grid.nodes.Count-1);
        
            while (grid.nodes[randomNodeId].Occupied || randomNodeId == start.id)
            {
                randomNodeId = Random.Range(0, grid.nodes.Count-1);
            }
            Vector3 randomGoal = grid.nodes[randomNodeId].Center;
            goal = grid.nodes[randomNodeId];
            goalPos = randomGoal;
            distanceThreshold = 2.0f;
            _grid_a_star = new Grid_A_Star(10,10,10);
            _grid_ara = new Grid_ARA(10,10,10);
            _grid_heuristic = new GridHeuristic(goal);
            found = 0;

            if (ARASearch)
            {
                waypoints = _grid_ara.findpath(grid,start,goal, _grid_heuristic,ref found);
            }
            else
            {
                waypoints = _grid_a_star.findpath(grid,start,goal, _grid_heuristic,ref found);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grid != null)
        {
            Vector3 directionGoal = goalPos - transform.position;
            Vector3 directionCurrWaypoint = waypoints[1].Center - transform.position;
            if (directionGoal.magnitude < distanceThreshold)
            {
                GridCell lastGoal = goal;
                int randomNodeId = Random.Range(0, grid.nodes.Count-1);
                while (grid.nodes[randomNodeId].Occupied || lastGoal.id == randomNodeId)
                {
                    randomNodeId = Random.Range(0, grid.nodes.Count-1);
                }
                Vector3 randomGoal = grid.nodes[randomNodeId].Center;
                goalPos = randomGoal;
                goal = grid.nodes[randomNodeId];
                _grid_heuristic = new GridHeuristic(goal);
                found = 0;
                if (ARASearch)
                {
                    waypoints = _grid_ara.findpath(grid,lastGoal,goal, _grid_heuristic,ref found);
                }
                else
                {
                    waypoints = _grid_a_star.findpath(grid,lastGoal,goal, _grid_heuristic,ref found);
                }
            } else if (directionCurrWaypoint.magnitude < distanceThreshold)
            {
                if (ARASearch)
                {
                    waypoints = _grid_ara.findpath(grid,waypoints[1],goal, _grid_heuristic,ref found);
                }
                else
                {
                    waypoints = _grid_a_star.findpath(grid,waypoints[1],goal, _grid_heuristic,ref found);
                }
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        if (waypoints != null && waypoints.Count > 1)
        {
            for (int i = 0; i < waypoints.Count-1; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(waypoints[i].Center,waypoints[i+1].Center);
            }
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(goalPos,0.5f);
    }
}
