using UnityEngine;

public class ForceField : Buff
{
    void Update()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}