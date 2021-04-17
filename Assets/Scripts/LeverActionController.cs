using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActionController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToInteract;
    private List<Color> startColor = new List<Color>();
    
    private void Start()
    {

        foreach (GameObject obj in objectsToInteract)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            startColor.Add(objRenderer.material.color);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeverTriggerZone"))
        {
            Debug.Log("LeverActive");

           foreach(GameObject obj in objectsToInteract)
            {
                Renderer objRenderer = obj.GetComponent<Renderer>();
                objRenderer.material.color = Color.green;
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LeverTriggerZone"))
        {
            Debug.Log("LeverDeactive");

           for (int i = 0; i < objectsToInteract.Length; i++)
            {
                Renderer objRenderer = objectsToInteract[i].GetComponent<Renderer>();
                objRenderer.material.color = startColor[i];
                
            }
        }
    }


}
