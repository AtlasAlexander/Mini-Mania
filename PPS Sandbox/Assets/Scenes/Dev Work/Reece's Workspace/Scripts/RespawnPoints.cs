using UnityEngine;

public class RespawnPoints : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawnpoint;
    [SerializeField] protected Transform player;
    [SerializeField] protected Transform cube1;
    [SerializeField] protected Transform cube2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetPlayerSpawnpoint()
    {
        Debug.Log("Spawn Triggered!");
        
            //playerSpawnpoint.position = player.position;
            //player.position = playerSpawnpoint.position;
        
        player.position = playerSpawnpoint.position;
    }

    public void SetCubePosition()
    {
        cube1.position = playerSpawnpoint.position;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    
    //}
}
