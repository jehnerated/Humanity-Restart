using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInfo : MonoBehaviour
{
    int start = 500;
    int remaining;

    bool isbeingMined;

    private void Start()
    {
        this.remaining = this.start;
    }

    private void Update()
    {
        if(this.remaining == 0)
        {
            Destroy(gameObject);
        }
    }

    public void removeResources(int amount)
    {
        this.remaining -= amount;
    }

    public int getResources()
    {
        return remaining;
    }

    public bool getMiningStatus()
    {
        return this.isbeingMined;
    }
    public void setMiningStatus(bool status)
    {
        this.isbeingMined = status;
    }
}
