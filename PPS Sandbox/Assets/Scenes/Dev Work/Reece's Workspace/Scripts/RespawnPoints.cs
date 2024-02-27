using UnityEngine;

public class RespawnPoints : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawnpoint;
    [SerializeField] protected Transform cube1Spawnpoint;
    [SerializeField] protected Transform cube2Spawnpoint;
    [SerializeField] protected Transform player;
    [SerializeField] protected Transform cube1;
    [SerializeField] protected Transform cube2;

    public void SetPlayerSpawnpoint()
    {
        Debug.Log("Spawn Triggered!");
        player.position = playerSpawnpoint.position;
    }

    public void SetCube1Position()
    {
        cube1.position = cube1Spawnpoint.position;
    }

    public void SetCube2Position()
    {
        cube2.position = cube2Spawnpoint.position;
    }
}
