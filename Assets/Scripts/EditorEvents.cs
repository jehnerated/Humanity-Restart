using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorEvents : MonoBehaviour
{
    //Menus
    public GameObject Step1Menu;
    public GameObject Step2Menu;
    public GameObject Step3Menu;
    public GameObject Step4Menu;
    public GameObject Step5Menu;

    //Terrain Models
    public GameObject groundPlane;
    public GameObject cliff;
    public GameObject insideCorner;
    public GameObject outsideCorner;

    //Envirment Models
    public GameObject Resource;

    //Player Building Models
    public GameObject CommandCenter;
    public GameObject Barrack;
    public GameObject Refinary;
    public GameObject Farm;
    public GameObject Training;

    //Player Unit Models
    public GameObject UPC50;

    //Enemy Building Models
    public GameObject TownHall;

    //Enemy Unit Models
    public GameObject Warrior;

    //Object Collections for use in save file
    GameObject[,] mapObjectArray;
    GameObject[] resourceObjectArray;
    GameObject[] playerBuildingObjectArray;
    GameObject[] playerUnitObjectArray;

    public int gridX = 128;
    public int gridY = 128;

    public float brushSize = 1f;

    int clickX;
    int clickY;

    public string saveFileText;

    public Material dirtMaterial;
    public Material grassMaterial;
    public Material waterMaterial;

    string tool;

    string[,] MapArray;
    string[,] MapNameArray;

    void Start()
    {
        int startSize = PlayerPrefs.GetInt("EditorNewSize");
        JehnerateScene(startSize, startSize);
        DrawMap();
        tool = "Raise";
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit clickHit;

            if (Physics.Raycast(clickRay, out clickHit, 50000.0f))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    string clickedName = clickHit.transform.name;
                    string[] clickLocation = clickedName.Split(',');
                    try
                    {
                        clickX = int.Parse(clickLocation[0]);
                    }
                    catch
                    {
                        clickX = (int)clickHit.transform.position.x;
                    }

                    try
                    {
                        clickY = int.Parse(clickLocation[1]);
                    }
                    catch
                    {
                        clickY = (int)clickHit.transform.position.y;
                    }

                    if (tool == "Raise")
                    {
                        MapArray[clickX, clickY] = "Second";
                        if (brushSize > 1)
                        {
                            if (clickX < gridX - 1) MapArray[clickX + 1, clickY] = "Second";
                            if (clickX < gridX - 1 && clickY < gridY - 1) MapArray[clickX + 1, clickY + 1] = "Second";
                            if (clickX < gridX - 1 && clickY > 1) MapArray[clickX + 1, clickY - 1] = "Second";
                            if (clickX > 1) MapArray[clickX - 1, clickY] = "Second";
                            if (clickX > 1 && clickY > 1) MapArray[clickX - 1, clickY - 1] = "Second";
                            if (clickX > 1 && clickY < gridY - 1) MapArray[clickX - 1, clickY + 1] = "Second";
                            if (clickY > 1) MapArray[clickX, clickY - 1] = "Second";
                            if (clickY < gridY - 1) MapArray[clickX, clickY + 1] = "Second";
                        }
                        if (brushSize > 2)
                        {
                            MapArray[clickX + 2, clickY] = "Second";
                            MapArray[clickX - 2, clickY] = "Second";
                            MapArray[clickX, clickY + 2] = "Second";
                            MapArray[clickX, clickY - 2] = "Second";
                        }
                        if (brushSize > 3)
                        {
                            MapArray[clickX + 3, clickY] = "Second";
                            MapArray[clickX - 3, clickY] = "Second";
                            MapArray[clickX, clickY + 3] = "Second";
                            MapArray[clickX, clickY - 3] = "Second";
                            MapArray[clickX - 2, clickY + 1] = "Second";
                            MapArray[clickX - 1, clickY + 2] = "Second";
                            MapArray[clickX + 1, clickY + 2] = "Second";
                            MapArray[clickX + 2, clickY + 1] = "Second";
                            MapArray[clickX - 2, clickY - 1] = "Second";
                            MapArray[clickX - 1, clickY - 2] = "Second";
                            MapArray[clickX + 1, clickY - 2] = "Second";
                            MapArray[clickX + 2, clickY - 1] = "Second";
                        }
                    }
                    if (tool == "Lower")
                    {
                        MapArray[clickX, clickY] = "Lower";
                        if (brushSize > 1)
                        {
                            if (clickX < gridX - 1) MapArray[clickX + 1, clickY] = "Lower";
                            if (clickX < gridX - 1 && clickY < gridY - 1) MapArray[clickX + 1, clickY + 1] = "Lower";
                            if (clickX < gridX - 1 && clickY > 1) MapArray[clickX + 1, clickY - 1] = "Lower";
                            if (clickX > 1) MapArray[clickX - 1, clickY] = "Lower";
                            if (clickX > 1 && clickY > 1) MapArray[clickX - 1, clickY - 1] = "Lower";
                            if (clickX > 1 && clickY < gridY - 1) MapArray[clickX - 1, clickY + 1] = "Lower";
                            if (clickY > 1) MapArray[clickX, clickY - 1] = "Lower";
                            if (clickY < gridY - 1) MapArray[clickX, clickY + 1] = "Lower";
                        }
                        if (brushSize > 2)
                        {
                            MapArray[clickX + 2, clickY] = "Lower";
                            MapArray[clickX - 2, clickY] = "Lower";
                            MapArray[clickX, clickY + 2] = "Lower";
                            MapArray[clickX, clickY - 2] = "Lower";
                        }
                        if (brushSize > 3)
                        {
                            MapArray[clickX + 3, clickY] = "Lower";
                            MapArray[clickX - 3, clickY] = "Lower";
                            MapArray[clickX, clickY + 3] = "Lower";
                            MapArray[clickX, clickY - 3] = "Lower";
                            MapArray[clickX - 2, clickY + 1] = "Lower";
                            MapArray[clickX - 1, clickY + 2] = "Lower";
                            MapArray[clickX + 1, clickY + 2] = "Lower";
                            MapArray[clickX + 2, clickY + 1] = "Lower";
                            MapArray[clickX - 2, clickY - 1] = "Lower";
                            MapArray[clickX - 1, clickY - 2] = "Lower";
                            MapArray[clickX + 1, clickY - 2] = "Lower";
                            MapArray[clickX + 2, clickY - 1] = "Lower";
                        }
                    }
                    if (tool == "Level")
                    {
                        MapArray[clickX, clickY] = "Base";
                        if (brushSize > 1)
                        {
                            if (clickX < gridX - 1) MapArray[clickX + 1, clickY] = "Base";
                            if (clickX < gridX - 1 && clickY < gridY - 1) MapArray[clickX + 1, clickY + 1] = "Base";
                            if (clickX < gridX - 1 && clickY > 1) MapArray[clickX + 1, clickY - 1] = "Base";
                            if (clickX > 1) MapArray[clickX - 1, clickY] = "Base";
                            if (clickX > 1 && clickY > 1) MapArray[clickX - 1, clickY - 1] = "Base";
                            if (clickX > 1 && clickY < gridY - 1) MapArray[clickX - 1, clickY + 1] = "Base";
                            if (clickY > 1) MapArray[clickX, clickY - 1] = "Base";
                            if (clickY < gridY - 1) MapArray[clickX, clickY + 1] = "Base";
                        }
                        if (brushSize > 2)
                        {
                            MapArray[clickX + 2, clickY] = "Base";
                            MapArray[clickX - 2, clickY] = "Base";
                            MapArray[clickX, clickY + 2] = "Base";
                            MapArray[clickX, clickY - 2] = "Base";
                        }
                        if (brushSize > 3)
                        {
                            MapArray[clickX + 3, clickY] = "Base";
                            MapArray[clickX - 3, clickY] = "Base";
                            MapArray[clickX, clickY + 3] = "Base";
                            MapArray[clickX, clickY - 3] = "Base";
                            MapArray[clickX - 2, clickY + 1] = "Base";
                            MapArray[clickX - 1, clickY + 2] = "Base";
                            MapArray[clickX + 1, clickY + 2] = "Base";
                            MapArray[clickX + 2, clickY + 1] = "Base";
                            MapArray[clickX - 2, clickY - 1] = "Base";
                            MapArray[clickX - 1, clickY - 2] = "Base";
                            MapArray[clickX + 1, clickY - 2] = "Base";
                            MapArray[clickX + 2, clickY - 1] = "Base";
                        }
                    }

                    if (tool == "Raise" || tool == "Lower" || tool == "Level") DrawMap();

                    if (tool == "Resource")
                    {
                        if(clickHit.transform.tag == "Ground")
                        {
                            Instantiate(Resource, clickHit.transform.position, Quaternion.identity);
                            tool = "";
                        }
                    }

                    if (tool == "UPC50")
                    {
                        if (clickHit.transform.tag == "Ground")
                        {
                            GameObject upc50 = Instantiate(UPC50, clickHit.transform.position + Vector3.down, Quaternion.identity);
                            upc50.GetComponentInChildren<Unit>().target = clickHit.transform.position + Vector3.down;
                            tool = "";
                        }
                    }
                }
            }
        }
    }

    public void JehnerateSaveFileString()
    {
        //Add Terrain
        DrawMap();
        saveFileText = gridX + "," + gridY;

        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                    saveFileText += "," + MapNameArray[x, y];
            }
        }

        //Add Enviroment Objects

        resourceObjectArray = GameObject.FindGameObjectsWithTag("Resource");

        if (resourceObjectArray.Length != 0)
        {

            saveFileText += "," + resourceObjectArray.Length;

            for (int r = 0; r < resourceObjectArray.Length; r++)
            {
                saveFileText += "," + resourceObjectArray[r].transform.tag + "/" + resourceObjectArray[r].transform.position.x + "/" + resourceObjectArray[r].transform.position.y + "/" + resourceObjectArray[r].transform.position.z;
            }
        }
        else
        {
            saveFileText += ",0";
        }

        //Add Player's Buildings

        playerBuildingObjectArray = GameObject.FindGameObjectsWithTag("Building");

        if (playerBuildingObjectArray.Length != 0)
        {

            saveFileText += "," + playerBuildingObjectArray.Length;

            for (int b = 0; b < playerBuildingObjectArray.Length; b++)
            {
                saveFileText += "," + playerBuildingObjectArray[b].transform.gameObject.GetComponent<UnitInfo>().unitType + "/" + playerBuildingObjectArray[b].transform.position.x + "/" + playerBuildingObjectArray[b].transform.position.y + "/" + playerBuildingObjectArray[b].transform.position.z;
            }
        }
        else
        {
            saveFileText += ",0";
        }

        //Add Player's Units
        playerUnitObjectArray = GameObject.FindGameObjectsWithTag("PlayerUnit");

        if (playerUnitObjectArray.Length != 0)
        {
            saveFileText += "," + playerUnitObjectArray.Length;

            for (int u = 0; u < playerUnitObjectArray.Length; u++)
            {
                saveFileText += "," + playerUnitObjectArray[u].transform.gameObject.GetComponent<UnitInfo>().unitType + "/" + playerUnitObjectArray[u].transform.position.x + "/" + playerUnitObjectArray[u].transform.position.y + "/" + playerUnitObjectArray[u].transform.position.z;
            }
        }
        else
        {
            saveFileText += ",0";
        }

        Debug.Log(saveFileText);
        PlayerPrefs.SetString("DefaultMap", saveFileText);
    }

    public void JehnerateScene(int newGridX, int newGridY)
    {
        gridX = newGridX;
        gridY = newGridY;

        MapArray = new string[newGridX + 1, newGridY + 1];
        MapNameArray = new string[newGridX + 1, newGridY + 1];
        mapObjectArray = new GameObject[newGridX + 1, newGridY + 1];

        for (int x = 0; x < newGridX + 1; x++)
        {
            for (int y = 0; y < newGridY + 1; y++)
            {
                MapArray[x, y] = "Base";
            }
        }


    }

    public void DrawMap()
    {
        foreach (GameObject go in mapObjectArray)
        {
            Destroy(go);
        }

        CreateLandScape();
    }

    public void BrushSize(float newBrushSize)
    {
        brushSize = newBrushSize;
    }

    public void RaiseClick()
    {
        tool = "Raise";
    }

    public void LevelClick()
    {
        tool = "Level";
    }

    public void LowerClick()
    {
        tool = "Lower";
    }
    public void ResourceClick()
    {
        tool = "Resource";
    }
    public void CommandClick()
    {
        tool = "CommandCenter";
        Instantiate(CommandCenter);
    }
    public void RefineryClick()
    {
        tool = "Refinary";
        Instantiate(Refinary);
    }
    public void BarrackClick()
    {
        tool = "Barrack";
        Instantiate(Barrack);
    }
    public void FarmClick()
    {
        tool = "Farm";
        Instantiate(Farm);
    }
    public void TrainingClick()
    {
        tool = "Training";
        Instantiate(Training);
    }
    public void UPC50Click()
    {
        tool = "UPC50";
    }
    public void Step2()
    {
        Step1Menu.SetActive(false);
        Step2Menu.SetActive(true);
    }

    public void Step3()
    {
        Step2Menu.SetActive(false);
        Step3Menu.SetActive(true);
    }

    public void Step4()
    {
        Step3Menu.SetActive(false);
        Step4Menu.SetActive(true);
    }

    public void Step5()
    {
        Step4Menu.SetActive(false);
        Step5Menu.SetActive(true);
    }

    public void CreateLandScape()
    {
        Vector3 spawnLocation;

        for (int y = 0; y < MapArray.GetLength(0) - 1; y++)
        {
            for (int x = 0; x < MapArray.GetLength(1) - 1; x++)
            {
                MapNameArray[x, y] = checkSquare(ref MapArray[x, y], ref MapArray[x, y + 1], ref MapArray[x + 1, y + 1], ref MapArray[x + 1, y]);
                switch (MapNameArray[x, y])
                {
                    case "Lower Dirt":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "Mid Dirt":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "High Dirt":
                        spawnLocation = new Vector3(x - (gridX / 2), 1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "North Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "East Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "South Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "West Cliff L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "North Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "East Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "South Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "West Cliff B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(cliff, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NW Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NE Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SW Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SE Outside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NW Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NE Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SW Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SE Outside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(outsideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NW Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NE Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SW Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SE Inside Corner L2B":
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NW Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 0, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "NE Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SW Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, -90, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    case "SE Inside Corner B2U":
                        spawnLocation = new Vector3(x - (gridX / 2), 0, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(insideCorner, spawnLocation, Quaternion.Euler(0, 180, 0));
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                    default:
                        spawnLocation = new Vector3(x - (gridX / 2), -1, y - (gridY / 2));
                        mapObjectArray[x, y] = Instantiate(groundPlane, spawnLocation, Quaternion.identity);
                        mapObjectArray[x, y].transform.name = x + "," + y + "," + MapArray[x, y];
                        break;
                }
            }

        }
    }

    public string checkSquare(ref string A, ref string S1, ref string S2, ref string S3)
    {
        string D1 = "Lower";
        string D2 = "Base";
        string D3 = "Second";

        if (A == D1)
        {
            if (S1 == D1)
            {
                if (S2 == D1)
                {
                    if (S3 == D1) return "Lower Dirt";
                    if (S3 == D3) S3 = D2;
                    if (S3 == D2) return "NW Outside Corner L2B";
                }
                if (S2 == D3)
                {
                    S2 = D2;
                }
                if (S2 == D2)
                {
                    if (S3 == D1) return "SW Outside Corner L2B";
                    if (S3 == D3) S3 = D2;
                    if (S3 == D2) return "East Cliff L2B";
                }
            }
            if (S1 == D3)
            {
                S1 = D2;
            }
            if (S1 == D2)
            {
                if (S2 == D1)
                {
                    if (S3 == D1) return "SE Outside Corner L2B";
                    if (S3 == D3) S3 = D2;
                    if (S3 == D2)
                    {
                        S2 = D2;
                        return "NE Inside Corner L2B";
                    }
                }
                if (S2 == D3)
                {
                    S2 = D2;
                }
                if (S2 == D2)
                {
                    if (S3 == D1) return "North Cliff L2B";
                    if (S3 == D3) S3 = D2;
                    if (S3 == D2) return "NE Inside Corner L2B";
                }
            }
        }
        if (A == D2)
        {
            if (S1 == D1)
            {
                if (S2 == D1)
                {
                    if (S3 == D1) return "NE Outside Corner L2B";
                    if (S3 == D3) S3 = D2;
                    if (S3 == D2) return "South Cliff L2B";
                }
                if (S2 == D3)
                {
                    S2 = D2;
                }
                if (S2 == D2)
                {
                    if (S3 == D1)
                    {
                        S2 = D1;
                        return "NE Outside Corner L2B";
                    }
                    if (S3 == D2) return "SE Inside Corner L2B";
                    if (S3 == D3) S1 = D2;
                }
            }
            if (S1 == D3)
            {
                if (S2 == D1)
                {
                    S2 = D2;
                }
                if (S2 == D3)
                {
                    if (S3 == D1) S2 = D2;
                    if (S3 == D2) return "North Cliff B2U";
                    if (S3 == D3) return "NE Inside Corner B2U";
                }
                if (S2 == D2)
                {
                    if (S3 == D1) S1 = D2;
                    if (S3 == D2) return "SE Outside Corner B2U";
                    if (S3 == D3)
                    {
                        S2 = D3;
                        return "NE Inside Corner B2U";
                    }
                }
            }
            if (S1 == D2)
            {
                if (S2 == D1)
                {
                    if (S3 == D1) return "West Cliff L2B";
                    if (S3 == D3) S2 = D2;
                    if (S3 == D2) return "SW Inside Corner L2B";
                }
                if (S2 == D3)
                {
                    if (S3 == D1) S2 = D2;
                    if (S3 == D2) return "SW Outside Corner B2U";
                    if (S3 == D3) return "East Cliff B2U";
                }
                if (S2 == D2)
                {
                    if (S3 == D1) return "NW Inside Corner L2B";
                    if (S3 == D2) return "Mid Dirt";
                    if (S3 == D3) return "NW Outside Corner B2U";
                }
            }
        }
        if (A == D3)
        {
            if (S1 == D1)
            {
                S1 = D2;
            }
            if (S1 == D2)
            {
                if (S2 == D1) S2 = D2;
                if (S2 == D2)
                {
                    if (S3 == D2) return "NE Outside Corner B2U";
                    if (S3 == D3) return "South Cliff B2U";
                }
                if (S2 == D3)
                {
                    if (S3 == D2)
                    {
                        S2 = D2;
                        return "NE Outside Corner B2U";
                    }
                    if (S3 == D3) return "SE Inside Corner B2U";
                }
            }
            if (S1 == D3)
            {
                if (S2 == D1) S2 = D2;
                if (S2 == D2)
                {
                    if (S3 == D2) return "West Cliff B2U";
                    if (S3 == D3) return "SW Inside Corner B2U";
                }
                if (S2 == D3)
                {
                    if (S3 == D2) return "NW Inside Corner B2U";
                    if (S3 == D3) return "High Dirt";
                }
            }
        }
        return "None";
    }
}