using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> panels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < panels.Count; i++) {
            panels[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel(int index)
    {
        if (!panels[index].activeSelf) {
            panels[index].SetActive(true);
        }
    }

    public void ClosePanel(int index)
    {
        if (panels[index].activeSelf) {
            panels[index].SetActive(false);
        }
    }
}
