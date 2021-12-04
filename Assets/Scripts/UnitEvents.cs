using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitEvents : MonoBehaviour
{
    public cameraScripts camScript;

    public Vector3 unitLocation;

    public GameObject UPC50Prefab;
    public GameObject messageBox;

    public ClickEvents clickEvent;

    public Text message;

    public Animator messageAni;

    public void UnitMoveClick()
    {
        camScript.selectedUnit.GetComponentInChildren<Unit>().actionType = "None";
        camScript.selectedUnit.GetComponentInChildren<UnitInfo>().stopAttack();
        camScript.unitAction = "None";
        camScript.MoveUnit = true;
    }

    public void BuildUPC50()
    {
        if (!camScript.selectedUnit.GetComponentInChildren<UnitInfo>().getBuildingStatus())
        {
            camScript.selectedUnit.GetComponentInChildren<UnitInfo>().setBuildingStatus(true);
            StartCoroutine(delayedBuildUPC50(UPC50Prefab.GetComponentInChildren<UnitInfo>().unitBuildTime));
        }
        else
        {
            displayMessage("Unit is busy, please wait");
        }
    }

    IEnumerator delayedBuildUPC50(float delayTime)
    {
        Vector3[] borderPoints = SharedFunctions.findBorderPoint(camScript.selectedUnit, 0.5f);
        int buildCost = UPC50Prefab.GetComponent<UnitInfo>().unitCost;

        Debug.Log("Running");

        yield return new WaitForSeconds(delayTime);

        if (Player.playerResources >= buildCost)
        {
            foreach (Vector3 point in borderPoints)
            {
                if (!Physics.CheckSphere(point + (Vector3.up * 0.6f), 0.3f))
                {
                    GameObject newObject = Instantiate(UPC50Prefab, point, Quaternion.identity);
                    Player.subtractResource(buildCost);
                    camScript.selectedUnit.GetComponentInChildren<UnitInfo>().setBuildingStatus(false);
                    StatsManager.AddPlayerUnit(ref newObject);
                    yield break;
                }
            }
        }
        else
        {
            displayMessage("Insufficient Resources, Mine More");
            camScript.selectedUnit.GetComponentInChildren<UnitInfo>().setBuildingStatus(false);
            yield break;
        }
        displayMessage("No Room, Move Something");
        camScript.selectedUnit.GetComponentInChildren<UnitInfo>().setBuildingStatus(false);
    }

    public void UPD50Mining()
    {
        camScript.unitAction = "Mining";
    }

    public void attackUnit()
    {
        camScript.unitAction = "Attack";
        clickEvent.lockSelect = true;
    }

    public void displayMessage(string messageString)
    {
        message.text = messageString;
        messageAni.SetTrigger("Trigger");
    }
}
