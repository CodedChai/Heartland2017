using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    public List<Node> path;
    public Transform seeker, target;
    public Vector3 nextPosition;
    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        if(seeker != null && target != null && Vector2.Distance(seeker.position, target.position) > Vector2.kEpsilon)
        {
            FindPath(seeker.position, target.position);
        }
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            // Need an initial node
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            // Reached Destination
            if (currentNode == targetNode)
            {
                if(startNode == targetNode)
                {
                    return;
                }
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbor in grid.GetNeighbors(currentNode))
            {
                if(!neighbor.walkable || closedSet.Contains(neighbor)){
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    } else
                    {
                        openSet.UpdateItem(neighbor);
                    }
                }
            }
        }
    }


    void RetracePath(Node startNode, Node endNode)
    {
        path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        nextPosition = path[0].worldPosition;

        grid.path = path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int disX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int disY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if(disX > disY)
        {
            return 14 * disY + 10 * (disX - disY);
        } else
        {
            return 14 * disX + 10 * (disY - disX);
        }

    }
}
