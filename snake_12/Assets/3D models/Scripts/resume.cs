using System.Collections;
using UnityEngine;

namespace Assets._3D_models.Scripts
{
    public class resume : MonoBehaviour
    {
        public InfoBox infoBox;
        public GameObject Pbutton;
       // public GameObject Playerbutton;

        // Use this for initialization
        void Start()
        {
         //   Time.timeScale = 1;
         //   Debug.Log("Start");

        }

        // Update is called once per frame
        void Update()
        {
            if (Time.timeScale == 1)
            {
                Pbutton.gameObject.SetActive(true);
                infoBox.gameObject.SetActive(true);
            }
            else
            {
                Pbutton.gameObject.SetActive(false);
                infoBox.gameObject.SetActive(false);
               // Playerbutton.gameObject.SetActive(false);

            }
            
        }
    }
}