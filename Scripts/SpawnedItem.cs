using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRTK;

public class SpawnedItem : VRTK_InteractableObject {

    private GameObject _spawner;

    public Transform LeftSnapHandle;
    public Transform RightSnapHandle;

    public GameObject spawner {
        get { return _spawner; }
        set { _spawner = _spawner ? _spawner : value; }
    }

    private void AddMechanicsScript() {
        var mechanics = gameObject.AddComponent<VRTK.GrabAttachMechanics.VRTK_ChildOfControllerGrabAttach>();
        mechanics.rightSnapHandle = RightSnapHandle;
        mechanics.leftSnapHandle = LeftSnapHandle;
        grabAttachMechanicScript = mechanics;
    }

    protected override void Awake()
    {
        this.isGrabbable = true;
        this.isUsable = true;
        this.validDrop = ValidDropTypes.No_Drop;
        this.useOverrideButton = VRTK_ControllerEvents.ButtonAlias.Trigger_Press;
        AddMechanicsScript();
    }

    private VRTK_InteractGrab GetGrabber(GameObject go) {
        return go.GetComponent<VRTK_InteractGrab>();
    }

    private GameObject GetGrabbed(VRTK_InteractGrab grabber) {
        return grabber.GetGrabbedObject();
    }

    public void PerformRelease(GameObject controller)
    {
        if (_spawner == null)
            Debug.Log("_spanwer is null, somehow");
        transform.position = _spawner.transform.position;
        gameObject.SetActive(false);
        controller.GetComponent<VRTK_InteractGrab>().ForceRelease();
        controller.GetComponent<VRTK_InteractUse>().ForceStopUsing();
        //controller.GetComponent<VRTK_InteractTouch>().ForceStopTouching();
        Debug.Log("Held item released!");
    }

    public override void OnInteractableObjectUsed(InteractableObjectEventArgs e)
    {
        Debug.Log("Held item used!");
        var grabber = GetGrabber(e.interactingObject);
        var grabbed = GetGrabbed(grabber);
        if (grabbed = gameObject) {
            Debug.Log("Being released");
            PerformRelease(e.interactingObject);
        }
    }
}
