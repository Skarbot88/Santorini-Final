using UnityEngine;

public class PlayerMove : TacticsMove
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving)
        { 
            FindSelectableTiles();
            CheckMouse();                       
        }
        else
        {
            Move();
            //FindSelectableTiles();
            //CheckMouse();
            //PlaceTile();
        }
    }
    void CheckMouse()
    {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit; 
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Tile")
                    {
                        Tile t = hit.collider.GetComponent<Tile>();
                        if (t.selectable)
                        {
                            //todo: move target 
                            MoveToTile(t);
                        }
                    else if (t.current)
                    {
                        Update();
                    }
                }
                }
            }
    }
}
