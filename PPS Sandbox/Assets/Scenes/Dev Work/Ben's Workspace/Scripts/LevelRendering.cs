using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRendering : MonoBehaviour
{
    [SerializeField] List<GameObject> Rooms;

    int currentRoom = 0;

    // Start is called before the first frame update
    void Start()
    {
        RoomChange();
    }
    
    void RoomChange()
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            if(i == currentRoom)
                Rooms[i].gameObject.SetActive(true);
            else if(i == currentRoom - 1)
                Rooms[i].gameObject.SetActive(true);
            else if (i == currentRoom + 1)
                Rooms[i].gameObject.SetActive(true);
            else
                Rooms[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Rooms[currentRoom + 1])
        {
            print("ROOM CHANGE");
            currentRoom++;
            RoomChange();
        }
        else if (other.gameObject == Rooms[currentRoom - 1])
        {
            currentRoom--;
            RoomChange();
        }
    }
}
