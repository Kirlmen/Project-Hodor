using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;
    Enemy enemy;
    Bank bank;
    private void Start()
    {
        bank = FindObjectOfType<Bank>();
        enemy = GetComponent<Enemy>();

    }

    // Create and Enable at hierarchy
    void OnEnable()
    {
        FindPath();
        ReturnStartPosition();
        StartCoroutine(FollowPath());

    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform) //foreach one find the waypoint componen on that object and then add that list of path.

        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null) //guarding. if it's not a waypoint, don't add it to the path.
            {
                path.Add(waypoint);
            }

        }
    }

    void ReturnStartPosition()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {

        gameObject.SetActive(false);
        enemy.StealGold();
        if (bank.CurrentBalance < 0) { ReloadScene(); }
    }


    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f) //while we are not at end position.
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); //going from point A to B
                yield return new WaitForEndOfFrame();

            }
        }
        FinishPath();
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }



}
