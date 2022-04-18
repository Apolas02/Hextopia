using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// on blueprint objects
public class blueprint_script : MonoBehaviour
{
    RaycastHit hit;
    public GameObject Prefab;
    public GameObject terrain;
    GameObject builtObject;
    public GameObject UI;
    public UICanvasScript UIScript;

    

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindWithTag("UI");
        UIScript = UI.GetComponent<UICanvasScript>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, (0 << 8)))
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50.0f))
        {
            transform.position = hit.point;
        }

        if (Input.GetKey(KeyCode.LeftBracket))
        {
            transform.Rotate(0.0f, 0.3f, 0.0f);
        }

        if (Input.GetKey(KeyCode.RightBracket))
        {
            transform.Rotate(0.0f, -0.3f, 0.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            builtObject = Instantiate(Prefab, transform.position, transform.rotation);
            UIScript.builtObjectList.Add(builtObject);
            builtObject.tag = Prefab.name;
            Destroy(gameObject);
        }


        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Mouse1))
        {
            Destroy(gameObject);
        }
    }
}
