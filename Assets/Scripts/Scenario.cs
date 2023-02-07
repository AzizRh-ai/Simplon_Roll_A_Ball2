using UnityEngine;

[CreateAssetMenu(menuName = "New Scenario")]

public class Scenario : ScriptableObject
{
    public Vector3[] Wall;
    public int Score;
}
