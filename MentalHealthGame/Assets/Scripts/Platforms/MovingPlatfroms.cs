using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfroms : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _platformSpeed = 2f;
    private int _currentWaypointIndex = 0;



    private void Update()
    {
        if (Vector2.Distance(_waypoints[_currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].transform.position, Time.deltaTime * _platformSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger.");
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.activeSelf)
            {
                collision.gameObject.transform.SetParent(null);
            }
        }
    }
}
