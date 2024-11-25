using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject == Player.Instance.gameObject)
        {
            RaceManager.Instance.IncreaseRaceNumber();
            RaceManager.Instance.StartTheRace();

            if (RaceManager.Instance.RaceNumber == 2)
                RaceManager.Instance.StopTheRace();

            /*if (RaceManager.Instance.RaceStarted)
                RaceManager.Instance.StopTheRace();
            else
                RaceManager.Instance.StartTheRace();*/
        }
        else
            return;
    }
}
