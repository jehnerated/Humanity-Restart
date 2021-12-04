using UnityEngine;
using UnityEngine.UI;

public class PanelUpdator : MonoBehaviour
{
    public UnitInfo referanceObject;

    public Text maxHealth;
    public Text currenttHealth;

    private bool isActive = false;

    public ClickEvents clickEvents;

    public void setActive(bool status)
    {
        isActive = status;
    }

    void Update()
    {
        if(isActive)
        {
            if (referanceObject)
            {
                maxHealth.text = referanceObject.unitMaxHit.ToString();
                currenttHealth.text = referanceObject.unitLife.ToString();
            }
            else
            {
                clickEvents.HidePanels();
            }
        }
    }
}
