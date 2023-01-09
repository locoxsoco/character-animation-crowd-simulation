using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PathFinding{

	public class ARA<TNode,TConnection,TNodeConnection,TGraph,THeuristic> : PathFinder<TNode,TConnection,TNodeConnection,TGraph,THeuristic>
	where TNode : Node
	where TConnection : Connection<TNode>
	where TNodeConnection : NodeConnections<TNode,TConnection>
	where TGraph : Graph<TNode,TConnection,TNodeConnection>
	where THeuristic : Heuristic<TNode>
	{
	// Class that implements the A* pathfinding algorithm
	// You have to implement the findpath function.
	// You can add whatever you need.
				
		protected List<TNode> visitedNodes; // list of visited nodes
		private int _maxNodes, _maxDepth;
		private float _maxTime;

		protected NodeRecord currentBest; // current best node found
		
		protected enum NodeRecordCategory{ OPEN, CLOSED, UNVISITED };
				
		protected class NodeRecord{	
		// You can use (or not) this structure to keep track of the information that we need for each node
			
			public NodeRecord(){}
			
			public TNode node; 
			public NodeRecord connection;	// connection traversed to reach this node 
			public float costSoFar; // cost accumulated to reach this node
			public float estimatedTotalCost; // estimated total cost to reach the goal from this node
			public NodeRecordCategory category; // category of the node: open, closed or unvisited
			public int depth; // depth in the search graph
		};

		public	ARA(int maxNodes, float maxTime, int maxDepth):base()
		{
			_maxNodes = maxNodes;
			_maxTime = maxTime;
			_maxDepth = maxDepth;
			visitedNodes = new List<TNode> ();
			
		}

		public virtual List<TNode> getVisitedNodes(){
			return visitedNodes;
		}

		List<TNode> reconstructPath(ref Dictionary<TNode, TNode> _cameFrom, TNode current, ref List<TNode> path)
		{
			path.Add(current);
			while (_cameFrom.ContainsKey(current))
			{
				current = _cameFrom[current];
				path.Insert(0,current);
			}
			return path;
		}

		void improvePath(ref HashSet<TNode> _openSet,
			ref HashSet<TNode> _closedSet,
			ref HashSet<TNode> _inconsSet, 
			ref Dictionary<TNode, float> _fScore, 
			ref Dictionary<TNode, float> _gScore,
			ref THeuristic heuristic,
			ref TNode end,
			ref TGraph graph,
			ref Dictionary<TNode, TNode> _cameFrom,
			float epsilon
			)
		{
			while (_openSet.Count != 0)
			{
				TNode current = null;
				float current_score_value = float.MaxValue;
				foreach (TNode node in _openSet)
				{
					if (_fScore.ContainsKey(node) && _fScore[node] < current_score_value)
					{
						current = node;
						current_score_value = _fScore[node];
					}
				}

				if (_fScore.ContainsKey(end) && _fScore[end] <= current_score_value)
				{
					break;
				}

				_openSet.Remove(current);
				_closedSet.Add(current);
				
				TNodeConnection current_connections = graph.getConnections(current);
				foreach (TConnection connection in current_connections.connections)
				{
					float tentative_gScore = _gScore[current] + connection.cost;
					if (!_gScore.ContainsKey(connection.toNode) || (_gScore.ContainsKey(connection.toNode) && tentative_gScore < _gScore[connection.toNode]))
					{
						_cameFrom[connection.toNode] = current;
						_gScore[connection.toNode] = tentative_gScore;
						_fScore[connection.toNode] = tentative_gScore + heuristic.estimateCost(connection.toNode) * epsilon;
						if (!_closedSet.Contains(connection.toNode))
						{
							_openSet.Add(connection.toNode);
						}
						else
						{
							_inconsSet.Add(connection.toNode);
						}
					}
				}
			}
		}
		
		public override List<TNode> findpath(TGraph graph, TNode start, TNode end, THeuristic heuristic, ref int found)
		{
			float epsilon = 2.5f;
			int counter_epsilon = 0; 
			List<TNode> path = new List<TNode>();
			
			// TO IMPLEMENT
			HashSet<TNode> _openSet = new HashSet<TNode>();
			HashSet<TNode> _closedSet = new HashSet<TNode>();
			HashSet<TNode> _inconsSet = new HashSet<TNode>();
			_openSet.Add(start);

			Dictionary<TNode, TNode> _cameFrom = new Dictionary<TNode, TNode>();
			
			Dictionary<TNode, float> _gScore = new Dictionary<TNode, float>();
			_gScore[start] = 0.0f;
			Dictionary<TNode, float> _fScore = new Dictionary<TNode, float>();
			_fScore[start] = heuristic.estimateCost(start);
			
			improvePath(ref _openSet, ref _closedSet, ref _inconsSet, ref _fScore, ref _gScore, ref heuristic, ref end, ref graph, ref _cameFrom, epsilon);

			TNode min_open_incons_node = null;
			float min_open_incons_node_score_value = float.MaxValue;
			foreach (TNode node in _openSet)
			{
				if (_gScore.ContainsKey(node) && _gScore[node]+heuristic.estimateCost(node) < min_open_incons_node_score_value)
				{
					min_open_incons_node = node;
					min_open_incons_node_score_value = _gScore[node]+heuristic.estimateCost(node);
				}
			}
			foreach (TNode node in _inconsSet)
			{
				if (_gScore.ContainsKey(node) && _gScore[node]+heuristic.estimateCost(node) < min_open_incons_node_score_value)
				{
					min_open_incons_node = node;
					min_open_incons_node_score_value = _gScore[node]+heuristic.estimateCost(node);
				}
			}

			float next_epsilon = Mathf.Min(epsilon, _gScore[end] / min_open_incons_node_score_value);

			// Just three times for this exercise
			while (next_epsilon > 1 && counter_epsilon < 3)
			{
				counter_epsilon++;
				epsilon = next_epsilon;
				while (_inconsSet.Count > 0)
				{
					TNode node = _inconsSet.First();
					_inconsSet.Remove(node);
					_openSet.Add(node);
				}
				foreach (var node in _openSet)
				{
					_fScore[node] = _gScore[node] + heuristic.estimateCost(node) * epsilon;
				}
				_closedSet.Clear();
				improvePath(ref _openSet, ref _closedSet, ref _inconsSet, ref _fScore, ref _gScore, ref heuristic, ref end, ref graph, ref _cameFrom, epsilon);
				next_epsilon = Mathf.Min(epsilon, _gScore[end] / min_open_incons_node_score_value);
			}
			
			Debug.Log(epsilon);
			path = reconstructPath(ref _cameFrom, end,ref path);
			found = path.Count;
			return path;
		}

	};

}