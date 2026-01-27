using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnCave : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    float originalPlayerZ; // Menyimpan posisi Z pemain sebelum teleportasi

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originalPlayerZ = player.transform.position.z; // Simpan posisi Z pemain saat Awake
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                Vector3 destinationPosition = destination.transform.position;
                destinationPosition.z = originalPlayerZ; // Tetapkan posisi Z pemain sesuai yang disimpan sebelumnya
                player.transform.position = destinationPosition;
            }
        }
    }
}
