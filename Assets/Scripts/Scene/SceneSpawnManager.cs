using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    public static string lastScene = "";

    public Transform spawnStart;            // Default spawn (optional)
    public Transform spawnFromHouse;
    public Transform spawnFromForest;
    public Transform spawnFromCave;
    public Transform spawnFromCaveIntersection;
    public Transform spawnFromCaveKey2;
    public Transform spawnFromCaveKey3;
    public Transform spawnFromBoss;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        switch (lastScene)
        {
            case "HouseScene":
                if (spawnFromHouse != null)
                    player.transform.position = spawnFromHouse.position;
                break;

            case "Forest Scene":
                if (spawnFromForest != null)
                    player.transform.position = spawnFromForest.position;
                break;

            case "Cave":
                if (spawnFromCave != null)
                    player.transform.position = spawnFromCave.position;
                break;

            case "CaveIntersection":
                if (spawnFromCaveIntersection != null)
                    player.transform.position = spawnFromCaveIntersection.position;
                break;

            case "CaveKey2":
                if (spawnFromCaveKey2 != null)
                    player.transform.position = spawnFromCaveKey2.position;
                break;

            case "CaveKey3":
                if (spawnFromCaveKey3 != null)
                    player.transform.position = spawnFromCaveKey3.position;
                break;

            case "BossRoom":
                if (spawnFromBoss != null)
                    player.transform.position = spawnFromBoss.position;
                break;    

            default:
                if (spawnStart != null)
                    player.transform.position = spawnStart.position;
                break;
        }
    }
}
