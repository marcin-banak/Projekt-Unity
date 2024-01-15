using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> chunkList;
    [SerializeField] private Transform mazeParent;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemy;

    // Maze values.
    int height = 7;
    int width = 7;
    int chunkSize = 17;
    private int chunkAmount = 0;

    private Enemy enemyMotion;
    private FOV enemyFOV;

    void Start() {
        chunkAmount = chunkList.Count;
        enemyFOV = enemy.GetComponent<FOV>();
        enemyMotion = enemy.GetComponent<Enemy>();
        //dla latarki potrzebowałam mieć prefab gracza w scenie, więc dodałam ręcznie na te pozycje startowa
        //Instantiate(playerPrefab, new Vector3((width - 1) * chunkSize / 2, 4, (height - 1) * chunkSize / 2), Quaternion.identity);
        // CreateMaze();
    }

    void Update() {
        if (enemyFOV.canSeePlayer) {
            enemyMotion.followTime = 7f;
        }
    }

    void CreateMaze() {
        CreateFrame();
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                // Calculate the position for each chunk based on grid coordinates and chunk size
                Vector3 spawnPosition = new Vector3(x * chunkSize, 0f, y * chunkSize);

                GameObject newChunk;
                if (x == (int)(width / 2) && y == (int)(height / 2)) {
                    newChunk = Instantiate(chunkList[0], spawnPosition, Quaternion.identity);
                    Instantiate(playerPrefab, spawnPosition + new Vector3(0, 2, 0), Quaternion.identity);
                }
                else {
                    newChunk = Instantiate(chunkList[Random.Range(1, chunkAmount)], spawnPosition, Quaternion.Euler(0, Random.Range(0, 3) * 90, 0));
                }
                newChunk.transform.parent = mazeParent.transform;
            }
        }
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<Transform>().transform.parent = mazeParent.transform;
        cube.transform.position = new Vector3(chunkSize * (width - 1) / 2, 0, chunkSize * (height - 1) / 2);
        cube.transform.localScale = new Vector3(width * chunkSize, 3f, height * chunkSize);
    }

    void CreateFrame() {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = mazeParent.transform;
        cube.transform.position = new Vector3(-(chunkSize), 2f, ((height - 1) * chunkSize) / 2);
        cube.transform.localScale = new Vector3(.5f, 3f, height * chunkSize);

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = mazeParent.transform;
        cube.transform.position = new Vector3((width - 0.5f) * chunkSize - 1, 2f, ((height - 1) * chunkSize) / 2);
        cube.transform.localScale = new Vector3(.5f, 3f, height * chunkSize);

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = mazeParent.transform;
        cube.transform.position = new Vector3(((width - 1) * chunkSize) / 2, 2f, (height - 0.5f) * chunkSize - 1);
        cube.transform.localScale = new Vector3(width * chunkSize / 2, 3f, .5f);

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = mazeParent.transform;
        cube.transform.position = new Vector3(((width - 1) * chunkSize) / 2, 2f, -chunkSize / 2 + 1);
        cube.transform.localScale = new Vector3(width * chunkSize / 2, 3f, .5f);
    }
}
