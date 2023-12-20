using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObject : MonoBehaviour, IInteraction
{
    public HintData hintData;
    bool isRolling;
    public float rotationSpeed = 5;
    public GameObject Player;
    public GameObject Cube;
    bool mathed;

    Bounds bound;
    Vector3 left, right, up, down;

    void Start()
    {
        bound = GetComponent<BoxCollider>().bounds;
      
        right = new Vector3(bound.size.x / 2, -bound.size.y / 2, 0);
        up = new Vector3(0, -bound.size.y / 2, bound.size.z / 2);
      
    }
    private void Update()
    {
       
    }
    public string GetInteractPrompt()
    {
        return string.Format("{0}", hintData.displayName);
    }
    public void OnInteract()
    {
       
        if (Input.GetKey(KeyCode.R) && !isRolling)
        {
            StartCoroutine(Roll(up));
        }
      
        else if (Input.GetKey(KeyCode.T) && !isRolling)
        {
            StartCoroutine(Roll(right));
        }
    
    }


    IEnumerator Roll(Vector3 positionToRotation)
    {
        isRolling = true;
        float angle = 0;
        Vector3 point = transform.position + positionToRotation;
        Vector3 axis = Vector3.Cross(Vector3.up, positionToRotation).normalized;

        while (angle < 90f)
        {
            float angleSpeed = Time.deltaTime + rotationSpeed;
            transform.RotateAround(point, axis, angleSpeed);
            angle += angleSpeed;
            yield return null;
        }

        transform.RotateAround(point, axis, 90 - angle);
        isRolling = false;
    }
}
