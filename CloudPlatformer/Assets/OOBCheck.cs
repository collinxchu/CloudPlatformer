using UnityEngine;

public class OOBRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
            Debug.Log("Player sent back to respawn point");
        }
    }
  
}
