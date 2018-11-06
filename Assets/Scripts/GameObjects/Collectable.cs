using UnityEngine;

public class Collectable : MonoBehaviour
{ 
	void Update () {
        transform.Rotate(new Vector3(0, 0, 50) * Time.deltaTime);
    }
}
