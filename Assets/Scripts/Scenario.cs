using UnityEngine;

[CreateAssetMenu(menuName = "New Scenario")]

public class Scenario : ScriptableObject
{
    public Vector3[] Wall;
    public int Score;

    public void OnEnable()
    {
        // Forcer la réinitialisation du score au démarrage
        // TODO: voir autre solution
        Score = 0;
    }

}
