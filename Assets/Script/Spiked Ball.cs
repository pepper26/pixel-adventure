using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    public Transform pendulum;

    [SerializeField] private float RotTime;
    [SerializeField] private float RotAngle;
    [SerializeField] private float RotSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pendulum)
        {
            RotTime += Time.deltaTime;
            pendulum.rotation = Quaternion.Euler(0, 0, RotAngle * Mathf.Sin(RotTime * RotSpeed));
        }
    }

}
