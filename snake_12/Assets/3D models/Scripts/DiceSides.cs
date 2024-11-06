using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSides : MonoBehaviour
{
   bool onGround;

   public int sideValue;

   void OnTriggerStay(Collider col)
   {
       if(col.CompareTag("Ground"))
       {
           onGround = true;
       }
   }

   void OnTriggerExit(Collider col)
   {
       if (col.CompareTag("Ground"))
       {
           onGround = false;
       }
   }
   public bool OnGround()
   {
       return onGround;
   }
}
