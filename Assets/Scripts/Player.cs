using UnityEngine;

[RequireComponent(typeof(Ashsvp.SimcadeVehicleController))]
public class Player : Singleton<Player>
{
    private Ashsvp.SimcadeVehicleController carController;

    public Transform PlayerTransform => transform;

    public bool IsMoving => carController.accelerationInput == 0;

    private void Start()
    {
        carController = GetComponent<Ashsvp.SimcadeVehicleController>();
    }

    public void SetTransform(Transform newTransform)
    {
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
        carController.RB.velocity = Vector3.zero;
    }
}
