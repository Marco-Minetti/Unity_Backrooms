using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class grid : MonoBehaviour
{
    public GameObject blockGameObject;
    public GameObject objectToSpawn;
    //variabili posizione
    /*[SerializeField]
    private int lun = 10;
    [SerializeField]
    private int lun = 10;*/
    [SerializeField]
    private int offSet = 1;
    
    private List<Vector3> posizioni = new List<Vector3>();
    [SerializeField]
    private int giu=40;
    [SerializeField]
    private int su=50;
    [SerializeField]
    private int dx=90;
    [SerializeField]
    private int sx=100;
    [SerializeField]
    private GameObject otherObject;
    void Start()
    {
        int lun = LunghezzaManager.Instance.lunghezza;
        
        for (int x = 0; x < lun; x++)
        {
            for (int z = 0; z < lun; z++)
            {
                Vector3 pos = new Vector3(x * offSet, 0, z * offSet);

                GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;

                posizioni.Add(block.transform.position);

                block.transform.SetParent(this.transform);
            }
        }
        Spawner(posizioni,lun);
    }
    /* void Spawner()
     {
         Vector3 newPos = Vector3.zero; // (0,0,0)
         bool Vx = true;
         bool V_x = true;
         bool Vz = true;
         bool V_z = true;
         for (int i = 0; i < 10; i++)
         {
             Vector3 posVecchia = newPos;
             int percentuale = Random.Range(0, 100);
             do
             {
                 if (percentuale <= 25 && Vx == true)
                 {
                     newPos.x++;
                     Vx = false;
                 }
                 if (percentuale <= 50 && percentuale > 25 && V_x == true)
                 {
                     newPos.x--;
                     V_x = false;
                 }
                 if (percentuale <= 75 && percentuale > 50 && Vz == true)
                 {
                     newPos.z++;
                     Vz = false;
                 }
                 if (percentuale <= 100 && percentuale > 75 && V_z == true)
                 {
                     newPos.z--;
                     V_z = false;
                 }
             } while (newPos.Equals(posVecchia));
             GameObject toPlaceObj = Instantiate(objectToSpawn, newPos, Quaternion.identity);
         }
     }*/

    void Spawner(List<Vector3> posizioni,int lun)
    {    
        
        int loop=0;
        int n =2*lun;
        int j = 0;
        List<Vector3> usato = new List<Vector3>();
        Vector3 newPos = posizioni[j];
        for (int i = 0; i < n; i++)
        {
            int jVecchio = j;
            bool rot = true;
            Vector3 posVecchia = newPos;
            
                int percentuale = Random.Range(0, 100);
                int l= Random.Range(3, 5);
                if (percentuale <= giu && j < (lun * lun) - (lun*l))
                {
                    rot=false;
                    for (int z=0; z<l ; z++){
                        
                        newPos = new Vector3(posizioni[j + lun].x, 0,posizioni[j + lun].z);
                        j = j + lun; //indice dopo creazione vettore
                        if (crea(usato,newPos,posVecchia,j,jVecchio,rot)){
                            break;
                        }
                        
                    }
                }
                else if (percentuale <= su && percentuale > giu && j > lun*l)
                {
                    rot=false;
                    for (int z=0; z<l; z++){

                    newPos = new Vector3(posizioni[j - lun].x, 0, posizioni[j - lun].z);
                    j = j - lun;
                    if (crea(usato,newPos,posVecchia,j,jVecchio,rot)){
                            break;
                        }
                    }
                }
                else if (percentuale <= dx  && percentuale > su && j < (j - (j%lun) + lun - l))
                {
                    for (int z=0; z<l ; z++){
                    newPos = new Vector3(posizioni[j + 1].x, 0, posizioni[j+1].z);
                    j = j + 1;
                    if (crea(usato,newPos,posVecchia,j,jVecchio,rot)){
                            break;
                        }
                    }
                }
                else if (percentuale <= sx && percentuale > dx && j > (j - (j%lun) + l))
                {
                    for (int z=0; z<l; z++){
                    newPos = new Vector3(posizioni[j - 1].x, 0, posizioni[j - 1].z);
                    j = j - 1;
                    if (crea(usato,newPos,posVecchia,j,jVecchio,rot)){
                            break;
                        }
                    }
                }
                else {
                     loop++;
                    if(loop % 2 == 0) {
                        i--;
                    }
                }
                

                
                
            
                
        }
    }

    void Scorre(List<Vector3> usato, Vector3 newPos, Vector3 posVecchia, int j,int jVecchio )
    {
        for (int lu = 0; lu < usato.Count; lu++)
        {
            Vector3 el = usato[lu];
            if (!newPos.Equals(el))
                usato.Add(newPos);
            else
            {
                newPos = posVecchia;
                j = jVecchio;
            }
        }
    }
    bool crea(List<Vector3> usato, Vector3 newPos,Vector3 posVecchia, int j, int jVecchio,bool rot)
    {
        Scorre(usato, newPos, posVecchia,j,jVecchio);//controllo
            if (newPos.Equals(posVecchia))
        {
            return true; 
            
        }else{

            GameObject toPlaceObj = Instantiate(objectToSpawn, newPos, Quaternion.identity);
            toPlaceObj.transform.SetParent(this.transform);
            if (!rot){
            toPlaceObj.transform.rotation = Quaternion.Euler(0, 90, 0);
            otherObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
            otherObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        return false;

    }
}
