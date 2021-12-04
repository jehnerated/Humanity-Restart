
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class cameraScripts : MonoBehaviour
{
    public int Boundary = 75;
    public int speed = 15;
    int theScreenWidth;
    int theScreenHeight;

    //Terrain Pieces
    public GameObject DirtPreFab;
    public GameObject CliffPreFab;
    public GameObject SmallRampPreFab;
    public GameObject LargeRampPreFab;
    public GameObject CliffInsideCornerPreFab;
    public GameObject CliffOutsideCornerPreFab;
    public GameObject groundPlane;
    public GameObject cliff;
    public GameObject insideCorner;
    public GameObject outsideCorner;

    //Enviroment
    public GameObject ResourcePrefab;

    //Buildings
    public GameObject CommandPrefab;
    public GameObject BarracksPrefab;
    public GameObject TrainingPrefab;
    public GameObject FarmPrefab;
    public GameObject RefinaryPrefab;

    //Units
    public GameObject Upc50;

    //Pathfinder
    public Grid pathFinder;

    //Public Text Asset
    public TextAsset defaultMapFile;

    UnitInfo selectedUnitInfo;
    Unit selectedUnitAcript;
    public ClickEvents clickEvent;


    public GameObject selectedUnit;
    GameObject addedPiece;

    public Text resourceDisplay;

    GameObject[,] mapObjectArray;

    int randNum;

    int gridX;
    int gridY;
    int resourceCount;
    int buildingCount;
    int unitCount;

    float xPos;
    float yPos;
    float zPos;

    string[] addPiece;

    public bool MoveUnit = false;
    public string unitAction = "None";
    public string[] mapLoad;

    Vector3 newPosition;
    Vector3 gotoPoint;

    void Awake()
    {
        Player.playerResources = 5000;
        resourceDisplay.text = Player.PlayerResources();

        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;

        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;

        Vector3 tileLocation = new Vector3(0, 0, 0);

        newPosition = transform.position;

        if(PlayerPrefs.GetString("DefaultMap").Length < 3)
        {
            mapLoad = defaultMapFile.text.Split(',');
        }
        else
        {
            mapLoad = PlayerPrefs.GetString("DefaultMap").Split(',');
        }

        int MapCounter = 2;

        gridX = int.Parse(mapLoad[0]);
        gridY = int.Parse(mapLoad[1]);

        resourceCount = int.Parse(mapLoad[(gridX * gridY) + 2]);
        buildingCount = int.Parse(mapLoad[(gridX * gridY) + resourceCount + 3]);
        unitCount = int.Parse(mapLoad[(gridX * gridY) + buildingCount + resourceCount + 4]);

        mapObjectArray = new GameObject[gridX, gridY];

        Vector3 spawnLocation;


        // Add Terrain
        for (int y = 0; y < gridX; y++)
        {
            for (int x = 0; x < gridY; x++)
            {

                switch (mapLoad[MapCounter])
                {
                    case "Lower Dirt":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "Mid Dirt":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "High Dirt":
                        spawnLocation = new Vector3(x - (gridX / 2), 1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "North Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "East Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "South Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "West Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "North Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "East Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "South Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "West Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NW Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NE Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SW Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SE Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NW Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NE Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SW Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SE Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NW Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NE Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SW Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SE Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NW Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "NE Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SW Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    case "SE Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                    default:
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + mapLoad[MapCounter];
                        break;
                }
                MapCounter++;
            }
        }
        MapCounter++;

        // Add Enviroment Units
        for(int r = 0; r < resourceCount; r++)
        {
            addPiece = mapLoad[MapCounter].Split('/');
            spawnLocation.x = float.Parse(addPiece[1]);
            spawnLocation.y = float.Parse(addPiece[2]);
            spawnLocation.z = float.Parse(addPiece[3]);
            Instantiate(ResourcePrefab, spawnLocation, Quaternion.identity);
            MapCounter++;
        }
        MapCounter++;

        // Add Player Buildings
        for (int b = 0; b < buildingCount; b++)
        {
            addPiece = mapLoad[MapCounter].Split('/');
            spawnLocation.x = float.Parse(addPiece[1]);
            spawnLocation.y = float.Parse(addPiece[2]);
            spawnLocation.z = float.Parse(addPiece[3]);
            switch(addPiece[0])
            {
                case "command":
                    Instantiate(CommandPrefab, spawnLocation, Quaternion.identity);
                    break;
                case "housing":
                    Instantiate(BarracksPrefab, spawnLocation, Quaternion.identity);
                    break;
                case "refinery":
                    Instantiate(RefinaryPrefab, spawnLocation, Quaternion.identity);
                    break;
                case "farm":
                    Instantiate(FarmPrefab, spawnLocation, Quaternion.identity);
                    break;
                case "training":
                    Instantiate(TrainingPrefab, spawnLocation, Quaternion.identity);
                    break;
            }
            MapCounter++;
        }
        MapCounter++;

        // Add Player Units
        for (int u= 0; u < unitCount; u++)
        {
            addPiece = mapLoad[MapCounter].Split('/');
            spawnLocation.x = float.Parse(addPiece[1]);
            spawnLocation.y = float.Parse(addPiece[2]) + 0.1f;
            spawnLocation.z = float.Parse(addPiece[3]);
            addedPiece = Instantiate(Upc50, spawnLocation, Quaternion.identity);
            addedPiece.GetComponentInChildren<Unit>().target = spawnLocation;
            MapCounter++;
        }

        pathFinder.CreateGrid();
    }
    void Update()
    {

        resourceDisplay.text = Player.PlayerResources();
        zPos = transform.position.z;

        if (Input.mousePosition.x > theScreenWidth - Boundary && xPos < (gridX / 2) - 6)
        {
            xPos += (speed * Time.deltaTime) * ((Input.mousePosition.x - (theScreenWidth - Boundary)) / Boundary); // move on +X axis
        }

        if (Input.mousePosition.x < 0 + Boundary && xPos > -(gridX / 2) + 4)
        {
            xPos -= (speed * Time.deltaTime) * (1 - (Input.mousePosition.x / Boundary)); // move on -X axis
        }

        if (Input.mousePosition.y > theScreenHeight - Boundary && zPos < (gridY / 2) - 10)
        {
            zPos += (speed * Time.deltaTime) * ((Input.mousePosition.y - (theScreenHeight - Boundary)) / Boundary); // move on +Z axis
        }

        if (Input.mousePosition.y < 0 + Boundary && zPos > -(gridY / 2) - 1)
        {
            zPos -= (speed * Time.deltaTime) * (1 - (Input.mousePosition.y / Boundary)); // move on -Z axis
        }

        newPosition = Vector3.Lerp(newPosition, new Vector3(xPos, yPos, zPos), 0.375f);
        transform.position = newPosition;


        //Check for clicks
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (!(Physics.CheckSphere(hit.point, 0.45f, 10)))
                {

                    if (!MoveUnit)
                    {
                        //Attack Funtion
                        if (unitAction == "Attack")
                        {
                            string clickType = hit.transform.tag;
                            if (clickType == "Building" ||
                                clickType == "PlayerUnit" ||
                                clickType == "EnemyBuilding" ||
                                clickType == "EnemyUnit"
                                )
                            {
                                selectedUnitInfo.setAttack(hit.transform.gameObject);
                                clickEvent.lockSelect = false;
                            }
                            unitAction = "None";
                        }
                        else
                        {

                            //Select Buildings
                            if (hit.transform.tag == "Building" || hit.transform.tag == "PlayerUnit")
                            {
                                selectedUnit = hit.transform.gameObject;
                                selectedUnitInfo = selectedUnit.GetComponent<UnitInfo>();
                                selectedUnitAcript = selectedUnit.GetComponent<Unit>();
                            }

                            //Mine Resources
                            if (hit.transform.tag == "Resource" && unitAction == "Mining" && !hit.transform.GetComponent<ResourceInfo>().getMiningStatus())
                            {
                                selectedUnitAcript.targetMine = hit.transform.GetComponent<ResourceInfo>();
                                selectedUnitAcript.resourceLocation = hit.transform.position;
                                selectedUnitAcript.refinaryLocation = findBorderPoint(locatedRefinery(hit.transform.position), hit.transform.position, 0.5f);
                                selectedUnitAcript.actionType = "Mining";
                                hit.transform.GetComponent<ResourceInfo>().setMiningStatus(true);
                                selectedUnitAcript.target = hit.point;
                            }

                            if (unitAction == "Attack")
                            {
                                string clickType = hit.transform.tag;
                                if (clickType == "Building" ||
                                    clickType == "PlayerUnit" ||
                                    clickType == "EnemyBuilding" ||
                                    clickType == "EnemyUnit"
                                    )
                                {
                                    selectedUnitInfo.setAttack(hit.transform.gameObject);
                                }
                            }
                        }
                    }

                    //Moving unit
                    if (MoveUnit)
                    {
                        if (selectedUnitAcript.targetMine) selectedUnitAcript.targetMine.GetComponent<ResourceInfo>().setMiningStatus(false);
                        if (selectedUnitAcript.targetMine) selectedUnitAcript.targetMine = null;
                        selectedUnitAcript.target = hit.point;
                        MoveUnit = false;
                    }

                    unitAction = "None";
                }
            }
        }
    }

    public GameObject locatedRefinery(Vector3 resourceLocation)
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        Debug.Log(buildings.Length);
        GameObject refineryLocation = null;
        float thisDistance;
        float shortestDistance = 10000f;
        foreach(GameObject building in buildings)
        {
            if(building.GetComponent<UnitInfo>().unitType == "refinery")
            {
                thisDistance = calculateDistance(building.transform.position, resourceLocation);
                if (thisDistance < shortestDistance)
                {
                    shortestDistance = thisDistance;
                    refineryLocation = building;
                }
            }
        }

        return refineryLocation;
    }

    public float calculateDistance(Vector3 pointA, Vector3 pointB)
    {
        return Mathf.Sqrt(Mathf.Pow((pointB.x - pointA.x),2) + Mathf.Pow((pointB.y - pointA.y), 2));
    }

    public Vector3 findBorderPoint(GameObject inputObject, Vector3 originPoint, float padding)
    {
        Vector3 startPoint = inputObject.transform.position;
        float xOffset = (inputObject.transform.gameObject.GetComponent<BoxCollider>().bounds.size.x / 2) + padding;
        float yOffset = (inputObject.transform.gameObject.GetComponent<BoxCollider>().bounds.size.z / 2) + padding;
        Vector3[] searchPoints = new Vector3[12];

        searchPoints[0] = startPoint + (Vector3.left * xOffset);
        searchPoints[1] = startPoint + (Vector3.forward * yOffset);
        searchPoints[2] = startPoint - (Vector3.left * xOffset);
        searchPoints[3] = startPoint - (Vector3.forward * yOffset);
        searchPoints[4] = startPoint + ((Vector3.left * xOffset) * 0.5f) + (Vector3.forward * yOffset);
        searchPoints[5] = startPoint - ((Vector3.left * xOffset) * 0.5f) + (Vector3.forward * yOffset);
        searchPoints[6] = startPoint + ((Vector3.left * xOffset) * 0.5f) - (Vector3.forward * yOffset);
        searchPoints[7] = startPoint - ((Vector3.left * xOffset) * 0.5f) - (Vector3.forward * yOffset);
        searchPoints[8] = startPoint + (Vector3.left * xOffset) + ((Vector3.forward * yOffset) * 0.5f);
        searchPoints[9] = startPoint + (Vector3.left * xOffset) - ((Vector3.forward * yOffset) * 0.5f);
        searchPoints[10] = startPoint - (Vector3.left * xOffset) + ((Vector3.forward * yOffset) * 0.5f);
        searchPoints[11] = startPoint - (Vector3.left * xOffset) - ((Vector3.forward * yOffset) * 0.5f);

        return findClosestPoint(searchPoints, originPoint);
    }

    public Vector3 findClosestPoint(Vector3[] searchPoints, Vector3 originPoint)
    {
        Vector3 closestPoint = searchPoints[0];
        float closestDistance = calculateDistance(originPoint, closestPoint);
        float currentDistance = calculateDistance(originPoint, closestPoint);

        foreach (Vector3 point in searchPoints)
        {
            if (!Physics.CheckBox(point, Vector3.one * 0.4f, Quaternion.identity, 10))
            {
                currentDistance = calculateDistance(originPoint, point);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestPoint = point;
                }
            }
        }

        return closestPoint;
    }
}
