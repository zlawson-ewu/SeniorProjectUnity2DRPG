using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = Player_Movement.Instance.transform;
        }
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
