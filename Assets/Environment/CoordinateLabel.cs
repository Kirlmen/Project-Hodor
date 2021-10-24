using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour
{

    Waypoint waypoint;
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    private void Awake()
    {
        waypoint = GetComponentInParent<Waypoint>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) //editor mode
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels() //toggle coordinate labels.
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }

    }

    private void SetLabelColor()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else { label.color = blockedColor; }
    }

    void DisplayCoordinates()
    {

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y;

    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
