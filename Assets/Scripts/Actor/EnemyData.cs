using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Objects/Enemy Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public float Health;
    public float PointValue;
}
