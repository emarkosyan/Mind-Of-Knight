using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject layoutRoom;

    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction { up, right, down, left};
    public Direction selectedDirections;
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

        selectedDirections = (Direction)Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveGenerationPoint()
    {

    }
}
