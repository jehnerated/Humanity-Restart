using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preview : MonoBehaviour
{
    RaycastHit hit;
    public GameObject prefab;
    public GameObject PathFinder;

    private Collider[] overLapCheck;
    private bool overlapping = false;

    public Material activePlacement;
    public Material negitivePlacement;


    void Start()
    {
        PathFinder = GameObject.Find("Pathfinder");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 50000.0f, ~(1 << 8)))
        {
            hit.point.Set(1, 1,1);
            transform.position = hit.point;
        }
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, ~(1 << 8)))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {

                if(!checkCollision())
                {
                    gameObject.GetComponentInChildren<MeshRenderer>().material = activePlacement;
                }
                else
                {
                    gameObject.GetComponentInChildren<MeshRenderer>().material = negitivePlacement;
                }

                transform.position = hit.point;

            }
        }

        float rot = Input.GetAxis("Mouse ScrollWheel");


        if (rot != 0)
        {
            if (rot > 0) transform.Rotate(new Vector3(0,  90, 0));
            if (rot < 0) transform.Rotate(new Vector3(0, -90, 0));
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (!checkCollision())
            {
                Player.subtractResource(prefab.GetComponent<UnitInfo>().unitCost);
                Instantiate(prefab, transform.position, transform.rotation);
                PathFinder.GetComponent<Grid>().CreateGrid();
                Destroy(gameObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }

    }

    public bool checkCollision()
    {
        overlapping = false;
        overLapCheck = Physics.OverlapSphere(hit.point, 1f);
        foreach (Collider test in overLapCheck)
        {
            if (test.gameObject.tag == "Cliff" || test.gameObject.tag == "Building")
            {
                overlapping = true;
            }
        }
        return overlapping;
    }
}
