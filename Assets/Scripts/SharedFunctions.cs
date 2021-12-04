using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedFunctions
{   
    public static Vector3[] findBorderPoint(GameObject inputObject, float padding)
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

        return searchPoints;
    }

    public static float calculateDistance(Vector3 pointA, Vector3 pointB)
    {
        return Mathf.Sqrt(Mathf.Pow((pointB.x - pointA.x), 2) + Mathf.Pow((pointB.y - pointA.y), 2));
    }

    public static Vector3 findClosestPoint(Vector3[] searchPoints, Vector3 originPoint)
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
