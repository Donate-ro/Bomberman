using UnityEngine;

public class Rotation : MonoBehaviour
{
    public int speedOfRotation = 50;
    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * speedOfRotation, 0, Space.World);
    }
}