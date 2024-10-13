using System.Collections;
using UnityEngine;

public class SpawnOnStartIfNotTrigger : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject wall;
    public GameObject otherObject;
    public float checkRadius = 1f; 
    public LayerMask triggerLayers; 
    [SerializeField]
    private int dens = 10;
    private void Start()
    {
        int r = Random.Range(0, 100);
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, triggerLayers);

        bool isTriggerDetected = false;

        Quaternion otherObjectRotation = otherObject.transform.rotation;
        foreach (Collider col in colliders)
        {
            if (col.isTrigger)
            {
                isTriggerDetected = true;
                break;
            }
        }

        
        if (!isTriggerDetected) 
        {
            if ( r<=dens){
            Instantiate(objectToSpawn, transform.position, otherObjectRotation);
            
            }
        //}
        //StartCoroutine(aspetta());
        //if (!isTriggerDetected)
        //{ 
         //   if (r>dens){
         else{
            Instantiate(wall, transform.position, otherObjectRotation);
            }
        }
        
    }

    private IEnumerator aspetta()
    {
        yield return new WaitForSeconds(1);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
