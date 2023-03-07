using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform pointsParent;
    [SerializeField] private Transform pointPrefab;
    [SerializeField] private Transform[] basePoints;
    [SerializeField] private Transform lastPoint;
    [SerializeField] private Transform pickedPoint;
    [SerializeField] private int amount;

    private bool isDone = false;


    [ContextMenu("InstantiatePoints")]
    public void InstantiatePoints()
    {
        if (isDone) return;

        Transform pointTransform;

        for (int i = 0; i < amount; i++)
        {
            if (pointsParent.childCount == 0) // First time
            {

                pointTransform = Instantiate(pointPrefab, RandomSpawnPoint(), Quaternion.identity);
                pointTransform.parent = pointsParent;
                lastPoint = pointTransform;
                continue;
            }

            int rand = Random.Range(0, amount);
            int randIndex = (i + rand) % 3;
            pickedPoint = basePoints[randIndex];

            pointTransform = Instantiate(pointPrefab, CalculateHalfPoint(lastPoint.position, pickedPoint.position), Quaternion.identity);
            pointTransform.parent = pointsParent;
            lastPoint = pointTransform;

        }

        isDone = true;
    }

    [ContextMenu("ClearPoints")]
    public void ClearPoints()
    {
        if (isDone)
        {
            while (pointsParent.childCount > 0)
            {
                foreach (Transform child in pointsParent)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }
        
        isDone = false;
    }

    private Vector3 RandomSpawnPoint()
    {
        int randX = Random.Range(0, 400);
        int randY = Random.Range(0, 400);

        if (randX <= 200)
        {
            if (randY > 2 * randX)
                randY = 2 * randX;
        }
        if (randX > 200)
        {
            if (randY > (550 - randX) / 2)
                randY = (550 - randX) / 2;
        }

        Vector3 randSpawnPoint = new Vector3(randX, 0f, randY);
        Debug.Log(randSpawnPoint);
        return randSpawnPoint;
    }

    private Vector3 CalculateHalfPoint(Vector3 startPoint, Vector3 endPoint)
    {
        return new Vector3(Mathf.Abs((startPoint.x + endPoint.x) / 2), 0f, Mathf.Abs((startPoint.z + endPoint.z) / 2));
    }

    public int GetAmount()
    {
        return amount;
    }

    public void SetAmount(int value)
    {
        amount = value;
    }
}
