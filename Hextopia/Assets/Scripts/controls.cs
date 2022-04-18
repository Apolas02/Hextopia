using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class controls : MonoBehaviour
    {
        public float speed = 5;
        public GameObject MainCamera;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            input();
        }

        void input()
        {
            /*if (Input.GetKey(KeyCode.Mouse1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    clickableObjects[0] = hit.collider.tag;
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                transform.eulerAngles += speed * new Vector3(x: 0, y: Input.GetAxis("Mouse X"), z: 0);
                MainCamera.transform.RotateAround(gameObject.transform.position,
                    new Vector3(Mathf.Clamp(Input.GetAxis("Mouse Y"), -90, 90),
                                 y: 0,
                                 Mathf.Clamp(Input.GetAxis("Mouse Y"), 0, 0)), -1);
            */
        }
    }
}

