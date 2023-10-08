using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public GameObject PlayerPrefab;
    public Rigidbody PlayerRigidbody;
    public float JumpForce;
    public float WalkForce; 
    public float RunningForce;

    //public GameObject BulletPrefab;
 
}


