using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //tile being standed on 
    public bool walkable = true; 
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public List<Tile> adjacencyList = new List<Tile>();
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public void Reset()
    {
        adjacencyList.Clear();
      
        current = false;
        target = false;
        selectable = false;
    
        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbours(float jumpHeight)
    {
        Reset();
        if (transform.position.y >= 2)
        {
            jumpHeight = 4;
        }
        CheckTiles(Vector3.forward,  jumpHeight);
        CheckTiles(Vector3.forward + (Vector3.right), jumpHeight);
        CheckTiles(Vector3.forward + (Vector3.left), jumpHeight);
        CheckTiles(-Vector3.forward ,  jumpHeight);
        CheckTiles(-Vector3.forward + (Vector3.right), jumpHeight);
        CheckTiles(-Vector3.forward + (Vector3.left), jumpHeight);
        CheckTiles(Vector3.left, jumpHeight);
        CheckTiles(Vector3.right, jumpHeight); 
    
    }

    public void CheckTiles(Vector3 direction, float jumpHeight)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1+jumpHeight)/2.0f,0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    adjacencyList.Add(tile);
                }
            }
        }

    }
}
