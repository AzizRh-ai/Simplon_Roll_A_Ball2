using UnityEngine;

[CreateAssetMenu(menuName = "New Scenario")]

public class Scenario : ScriptableObject
{
    public Vector3[] Wall;
    public int Score;

    public void OnEnable()
    {
        // Forcer la r�initialisation du score au d�marrage
        // TODO: voir autre solution
        Score = 0;
    }

}
