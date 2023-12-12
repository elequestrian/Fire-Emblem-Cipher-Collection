using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SakuraStudios.FECipherCollection
{
    public class NavPanelController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenMainMenuScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
