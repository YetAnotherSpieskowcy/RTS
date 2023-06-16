using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMarkers : MonoBehaviour
{
    public GameObject marker;
    public float timeScale = 0.2f;
    private List<GameObject> markers = new List<GameObject>();
    private List<Transform> targets = new List<Transform>();

    private IEnumerator Animate()
    {
        float progress = 0;
        while (progress < 1.0)
        {
            progress += Time.deltaTime * timeScale;
            marker.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Progress", progress);
            yield return new WaitForEndOfFrame();
        }
        foreach (GameObject m in markers)
        {
            m.SetActive(false);
        }
    }

    void CollectTargets()
    {
        targets.Clear();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Mark"))
        {
            targets.Add(go.transform);
        }
    }

    void ShowAllMarkers()
    {
        while (markers.Count < targets.Count)
        {
            markers.Add(Instantiate(marker));
        }
        List<GameObject>.Enumerator iMarkers = markers.GetEnumerator();
        iMarkers.MoveNext();
        foreach (Transform target in targets)
        {
            iMarkers.Current.transform.position = target.position;
            iMarkers.Current.SetActive(true);
            iMarkers.MoveNext();
        }
        StartCoroutine(Animate());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CollectTargets();
            ShowAllMarkers();
        }
    }
}
