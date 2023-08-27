using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    private List<GameObject> objectsToDestroy;
    private int totalObjectsToDelete;

    private void Start()
    {
        // Initialize the list of objects to be potentially deleted.
        objectsToDestroy = new List<GameObject>(GameObject.FindGameObjectsWithTag("StandardWeed"));

        // Randomize the total number of objects to delete, but limit to half the original count.
        totalObjectsToDelete = Random.Range(1, (objectsToDestroy.Count / 2) + 1);

        StartCoroutine(DeleteRandomly());
    }

    private IEnumerator DeleteRandomly()
    {
        // Initial delay before starting the deletion.
        yield return new WaitForSeconds(Random.Range(20f, 120f));

        int deletedObjectsCount = 0;

        while (deletedObjectsCount < totalObjectsToDelete)
        {
            if (objectsToDestroy.Count == 0)
                break;

            // Decide how many objects to delete, 1 or 2.
            int deleteCount = Random.Range(1, 3);
            for (int i = 0; i < deleteCount; i++)
            {
                // If we've reached our random deletion limit, stop.
                if (deletedObjectsCount >= totalObjectsToDelete)
                    break;

                // Choose a random object to delete.
                GameObject objectToDelete = objectsToDestroy[Random.Range(0, objectsToDestroy.Count)];

                Destroy(objectToDelete);
                objectsToDestroy.Remove(objectToDelete);
                deletedObjectsCount++;
            }

            // Wait for a random time before the next deletion.
            yield return new WaitForSeconds(Random.Range(20f, 120f));
        }
    }
}
