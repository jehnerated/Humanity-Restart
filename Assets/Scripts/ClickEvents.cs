using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvents : MonoBehaviour
{
    private UnitInfo clickedUnitInfo;
    private string clickedType;
    public cameraScripts camScripts;

    //Panels
    public GameObject housingPanel;
    public GameObject commandPanel;
    public GameObject farmPanel;
    public GameObject refineryPanel;
    public GameObject trainingPanel;
    public GameObject buildingPanel;
    public GameObject upd50Panel;

    //Panel Updators
    public PanelUpdator commandPanelUpdate;
    public PanelUpdator housePanelUpdate;
    public PanelUpdator trainPanelUpdate;
    public PanelUpdator refineryPanelUpdate;
    public PanelUpdator farmPanelUpdate;
    public PanelUpdator upc50PanelUpdate;

    public bool lockSelect = false;
    public void HidePanels()
    {
        housingPanel.SetActive(false);
        commandPanel.SetActive(false);
        farmPanel.SetActive(false);
        refineryPanel.SetActive(false);
        trainingPanel.SetActive(false);
        buildingPanel.SetActive(false);
        upd50Panel.SetActive(false);
        commandPanelUpdate.setActive(false);
        housePanelUpdate.setActive(false);
        trainPanelUpdate.setActive(false);
        refineryPanelUpdate.setActive(false);
        farmPanelUpdate.setActive(false);
        upc50PanelUpdate.setActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit clickHit;

            if (Physics.Raycast(clickRay, out clickHit, 50000.0f))
            {
                // What happens if you click a player building or unit
                if (clickHit.transform.tag == "Building" || clickHit.transform.tag == "PlayerUnit")
                {
                    if (!lockSelect)
                    {
                        clickedUnitInfo = clickHit.transform.gameObject.GetComponent<UnitInfo>();
                        clickedType = clickedUnitInfo.unitType;
                        HidePanels();
                        switch (clickedType)
                        {
                            case "housing":
                                housingPanel.SetActive(true);
                                housePanelUpdate.setActive(true);
                                housePanelUpdate.referanceObject = clickHit.transform.gameObject.GetComponent<UnitInfo>();
                                break;
                            case "command":
                                commandPanel.SetActive(true);
                                commandPanelUpdate.setActive(true);
                                commandPanelUpdate.referanceObject = clickHit.transform.gameObject.GetComponent<UnitInfo>();
                                break;
                            case "farm":
                                farmPanel.SetActive(true);
                                farmPanelUpdate.setActive(true);
                                farmPanelUpdate.referanceObject = clickHit.transform.gameObject.GetComponent<UnitInfo>();
                                break;
                            case "refinery":
                                refineryPanel.SetActive(true);
                                refineryPanelUpdate.setActive(true);
                                refineryPanelUpdate.referanceObject = clickHit.transform.gameObject.GetComponent<UnitInfo>();
                                break;
                            case "training":
                                trainingPanel.SetActive(true);
                                trainPanelUpdate.setActive(true);
                                trainPanelUpdate.referanceObject = clickHit.transform.gameObject.GetComponent<UnitInfo>();
                                break;
                            case "UPD50":
                                upd50Panel.SetActive(true);
                                upc50PanelUpdate.setActive(true);
                                upc50PanelUpdate.referanceObject = clickHit.transform.gameObject.GetComponent<UnitInfo>();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        HidePanels();
                    }
                    if (clickHit.transform.tag == "Ground" || clickHit.transform.tag == "Cliff" || clickHit.transform.tag == "Resource")
                    {
                        HidePanels();
                    }
                }
            }
        }
    }
}
