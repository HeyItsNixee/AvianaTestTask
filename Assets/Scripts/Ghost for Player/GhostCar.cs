using UnityEngine;

public class GhostCar : MonoBehaviour
{
    [SerializeField] private float FixedSpeed = 10f;
    private (Vector3, Quaternion) destination;
    private float timeToArrive;

    private float internalTimer = 0f;

    private void Update()
    {
        if (RaceManager.Instance.RaceStarted)
            MoveToDestination();
        else
            internalTimer = 0f;

        if (internalTimer >= timeToArrive)
            transform.position = destination.Item1;
    }

    public void SetGhostCarTransform(Transform destination)
    {
        transform.position = destination.position;
        transform.rotation = destination.rotation;
    }

    public void SetNewDestination(float newTimeToArrive, (Vector3, Quaternion) newDestination)
    {
        destination = newDestination;
        timeToArrive = newTimeToArrive;
    }

    private void MoveToDestination()
    {
        if (destination.Item1 == null || destination.Item2 == null)
            return;

        if (transform.position == destination.Item1)
        {
            var a = RaceManager.Instance.CurrentDestinationAndTime();

            timeToArrive = a.Item1;
            destination = a.Item2;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.Item1, FixedSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, destination.Item2, FixedSpeed * Time.deltaTime);
        }

        internalTimer += Time.deltaTime;
    }

    public void ResetInternalTimer()
    {
        internalTimer = 0f;
    }
}
