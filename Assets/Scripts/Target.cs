using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private GameManager manager;

  /*  [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private float interpolationAmount = 0; // The value that goes in between 0 and 1
    [SerializeField] private float speed;*/

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("fooditem"))
        {
            // Will destroy the food item
            Destroy(collision.gameObject);

            // Change the position
            MoveToRandomPosition();

            // Add a point
            manager.AddPoint();
        }
    }

    private void MoveToRandomPosition()
    {
        var x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        var y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        var z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        transform.position = new Vector3(x, y, z);
    }
    
    void Update()
    {
        /*if(interpolationAmount > 1)
        {
            interpolationAmount = 0;
        }
        interpolationAmount += speed;*/

       // transform.position = Vector3.Lerp(startPoint.position, endPoint.position, interpolationAmount);
    }
}
