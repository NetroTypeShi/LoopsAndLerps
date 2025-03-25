using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeEdges : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] float timeBetweenSpawns = 0.05f;
    [SerializeField] int startValue = 0;
    [SerializeField] int amount = 5;
    [SerializeField] GameObject prefab;
    List<GameObject> spawnedObjects = new List<GameObject>();

    void Start()
    {
        StartCoroutine(InstantiateCubeEdges());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    IEnumerator InstantiateCubeEdges()
    {
        int start = startValue;
        int end = startValue + amount - 1;
        HashSet<Vector3> spawnedPositions = new HashSet<Vector3>();

        for (int x = start; x <= end; x++)
        {
            for (int y = start; y <= end; y++)
            {
                if (x == start || x == end || y == start || y == end)
                {
                    Vector3 posFront = new Vector3(x, y, end);
                    if (spawnedPositions.Add(posFront))
                    {
                        GameObject go = Instantiate(prefab, posFront, Quaternion.identity);
                        spawnedObjects.Add(go);
                        yield return new WaitForSeconds(timeBetweenSpawns);
                    }
                    Vector3 posBack = new Vector3(x, y, start);
                    if (spawnedPositions.Add(posBack))
                    {
                        GameObject go = Instantiate(prefab, posBack, Quaternion.identity);
                        spawnedObjects.Add(go);
                        yield return new WaitForSeconds(timeBetweenSpawns);
                    }
                }
            }
        }

        for (int x = start; x <= end; x++)
        {
            for (int z = start; z <= end; z++)
            {
                if (x == start || x == end || z == start || z == end)
                {
                    Vector3 posTop = new Vector3(x, end, z);
                    if (spawnedPositions.Add(posTop))
                    {
                        GameObject go = Instantiate(prefab, posTop, Quaternion.identity);
                        spawnedObjects.Add(go);
                        yield return new WaitForSeconds(timeBetweenSpawns);
                    }
                    Vector3 posBottom = new Vector3(x, start, z);
                    if (spawnedPositions.Add(posBottom))
                    {
                        GameObject go = Instantiate(prefab, posBottom, Quaternion.identity);
                        spawnedObjects.Add(go);
                        yield return new WaitForSeconds(timeBetweenSpawns);
                    }
                }
            }
        }

        for (int y = start; y <= end; y++)
        {
            for (int z = start; z <= end; z++)
            {
                if (y == start || y == end || z == start || z == end)
                {
                    Vector3 posLeft = new Vector3(start, y, z);
                    if (spawnedPositions.Add(posLeft))
                    {
                        GameObject go = Instantiate(prefab, posLeft, Quaternion.identity);
                        spawnedObjects.Add(go);
                        yield return new WaitForSeconds(timeBetweenSpawns);
                    }
                    Vector3 posRight = new Vector3(end, y, z);
                    if (spawnedPositions.Add(posRight))
                    {
                        GameObject go = Instantiate(prefab, posRight, Quaternion.identity);
                        spawnedObjects.Add(go);
                        yield return new WaitForSeconds(timeBetweenSpawns);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            Destroy(spawnedObjects[i]);
            yield return new WaitForSeconds(0.05f);
        }

        spawnedObjects.Clear();

        StartCoroutine(InstantiateCubeEdges());
    }
}



