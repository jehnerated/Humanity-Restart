using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    //Public Variables
    public string unitType;
    public int unitCost;
    public int unitBuildTime;
    public int unitMaxHit;
    public int unitLife;
    public int unitAttackRange;
    public int unitAttackDamage;
    public int unitVisionRange;
    public float attackRate;

    // Private Variables
    private bool building = false;
    private bool attacking = false;
    private bool targetHit = false;
    private bool attackCycle = false;
    private Unit unitScript;
    private GameObject targetUnit;
    private Animator animator;


    private void Start()
    {
        unitScript = gameObject.GetComponent<Unit>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void setBuildingStatus(bool status)
    {
        building = status;
    }

    public bool getBuildingStatus()
    {
        return building;
    }

    public void setAttack(GameObject target)
    {
        targetUnit = target;
        attacking = true;
    }

    public void stopAttack()
    {
        attacking = false;
        animator.SetInteger("AniChoice", 0);
    }

    public void hasBeenHit(int damage)
    {
        unitLife -= damage;
    }

    IEnumerator attackDelay(float delayTime)
    {
        attackCycle = true;

        yield return new WaitForSeconds(delayTime);

        if (targetUnit)
        {
            gameObject.transform.rotation = Quaternion.LookRotation(targetUnit.transform.position - gameObject.transform.position);
            targetUnit.GetComponent<UnitInfo>().hasBeenHit(unitAttackDamage);
        }

        attackCycle = false;

        yield break;
    }

    private void Update()
    {
        if(unitLife <= 0)
        {
            Destroy(gameObject);
        }

        if (attacking)
        {
            if (targetUnit)
            {
                Collider[] overlapCols = Physics.OverlapSphere(gameObject.transform.position, unitAttackRange);

                foreach(Collider col in overlapCols)
                {
                    if(col.gameObject == targetUnit)
                    {
                        animator.SetInteger("AniChoice", 3);
                        targetHit = true;
                    }
                }

                if(targetHit)
                {
                    if(!attackCycle) StartCoroutine(attackDelay(attackRate));
                    targetHit = false;
                }
                else
                {
                }


                if (targetUnit.tag != "Building")
                {
                    unitScript.target = targetUnit.transform.position;
                }
                else
                {
                    unitScript.target = SharedFunctions.findClosestPoint(SharedFunctions.findBorderPoint(targetUnit, 0.5f), gameObject.transform.position);
                }
            }
            else
            {
                animator.SetInteger("AniChoice", 0);
                attacking = false;
            }
        }
    }
}
