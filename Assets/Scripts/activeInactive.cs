using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeInactive : MonoBehaviour
{
   public List<GameObject>obj=new List<GameObject>();

   public void togglepanel(int val){
    for(int j=0;j<obj.Count;j++)obj[j].SetActive(false);
    obj[val].SetActive(true);
   }
}
