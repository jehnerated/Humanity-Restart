using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStructure : MonoBehaviour
{
    public GameObject BarrackPreview;
    public GameObject TrainingPreview;
    public GameObject FarmPreview;
    public GameObject RefineryPreview;
    public GameObject CommandPreview;

    public GameObject Barracks;
    public GameObject Training;
    public GameObject Farm;
    public GameObject Refinery;
    public GameObject Command;


    public void BuildBarracks()
    {
        if (Player.playerResources >= Barracks.GetComponent<UnitInfo>().unitCost)
        {
            Instantiate(BarrackPreview);
        }
    }
    public void BuildTraining()
    {
        if (Player.playerResources >= Training.GetComponent<UnitInfo>().unitCost)
        {
            Instantiate(TrainingPreview);
        }
    }
    public void BuildFarm()
    {
        if (Player.playerResources >= Farm.GetComponent<UnitInfo>().unitCost)
        {
            Instantiate(FarmPreview);
        }
    }
    public void BuildRefinery()
    {
        if (Player.playerResources >= Refinery.GetComponent<UnitInfo>().unitCost)
        {
            Instantiate(RefineryPreview);
        }
    }
    public void BuildCommand()
    {
        if (Player.playerResources >= Command.GetComponent<UnitInfo>().unitCost)
        {
            Instantiate(CommandPreview);
        }
    }
}
