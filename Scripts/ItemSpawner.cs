using UnityEngine;
using VRTK;

public class ItemSpawner : VRTK_InteractableObject
{
    public GameObject prefab;
    private GameObject item;

    private void Start()
    {
        // create the prefab instance
        item = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
        // add the SpawnedItem component
        var spawned = item.AddComponent<SpawnedItem>();
        // deactivate the prefab instance
        item.SetActive(false);
    }

    private VRTK_InteractGrab GetGrabber(GameObject go) {
        // return the grab component
        return go.GetComponent<VRTK_InteractGrab>();
    }

    private GameObject GetGrabbed(VRTK_InteractGrab grabber) {
        // return what is currently grabbed
        return grabber.GetGrabbedObject();
    }

    private void PerformGrab(GameObject controller) {
        // enable the managed item
        item.SetActive(true);
        item.GetComponent<SpawnedItem>().spawner = gameObject;
        // force the controller to stop touching the spawner
        controller.GetComponent<VRTK_InteractTouch>().ForceStopTouching();
        // make it touch the managed item instead
        controller.GetComponent<VRTK_InteractTouch>().ForceTouch(item);
        // then attempt to grab the managed item
        controller.GetComponent<VRTK_InteractGrab>().AttemptGrab();
    }

    public override void OnInteractableObjectUsed(InteractableObjectEventArgs e)
    {
        Debug.Log("Spawner was used!");
        base.OnInteractableObjectUsed(e);
        PerformGrab(e.interactingObject);
    }

}