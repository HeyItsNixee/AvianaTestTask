using System.Collections.Generic;
using UnityEngine;

public class RaceManager : Singleton<RaceManager>
{
    [SerializeField] private GameObject UI_Object;
    [SerializeField] private GhostCar ghostCarPrefab;
    private GhostCar spawnedGhostCar;

    [SerializeField] private Transform playerStartTransform;

    private List<(float, (Vector3, Quaternion))> capturedTransformsAndTime;
    private int currentPosID = -1;
    private int raceNumber = 0;

    private float internalRaceTimerForGhost = 0f;
    private float internalRaceTimer = 0f;
    private float captureTime = 1f;
    private float captureTimer = 0f;

    private bool raceStarted = false;
    public bool RaceStarted => raceStarted;
    public int RaceNumber => raceNumber;
    public float RaceTime => internalRaceTimer;
    private void Start()
    {
        capturedTransformsAndTime = new List<(float, (Vector3, Quaternion))>();
        captureTimer = 0f;
        spawnedGhostCar = null;
    }

    private void Update()
    {
        if (!raceStarted)
            return;

        if (raceNumber == 0)
        {
            if (captureTimer >= captureTime)
            {
                capturedTransformsAndTime.Add((internalRaceTimerForGhost, (Player.Instance.PlayerTransform.position, Player.Instance.PlayerTransform.rotation)));
                captureTimer = 0f;
            }
            else
                captureTimer += Time.deltaTime;
        }

        internalRaceTimerForGhost += Time.deltaTime;
        internalRaceTimer += Time.deltaTime;
    }

    public void StartTheRace()
    {
        UI_Object.SetActive(false);

        //Player.Instance.SetTransform(playerStartTransform);

        raceStarted = true;
        captureTimer = 0f;
        internalRaceTimerForGhost = 0f;

        if (raceNumber != 0)
        {
            if (spawnedGhostCar != null)
                return;

            spawnedGhostCar = Instantiate(ghostCarPrefab);
            spawnedGhostCar.SetGhostCarTransform(playerStartTransform);
            spawnedGhostCar.SetNewDestination(capturedTransformsAndTime[0].Item1, capturedTransformsAndTime[0].Item2);
        }
    }

    public void NextLap()
    {
        //TBD
    }

    public void StopTheRace()
    {
        UI_Object.SetActive(true);
        Player.Instance.SetTransform(playerStartTransform);
        raceStarted = false;
        captureTimer = 0f;

        UI_TimerAndRace.Instance.UpdateRaceNumberText();
        UI_TimerAndRace.Instance.UpdateRaceTimerText();

        if (spawnedGhostCar != null)
        {
            Destroy(spawnedGhostCar.gameObject);
            spawnedGhostCar = null;
        }
    }

    public void IncreaseRaceNumber()
    {
        raceNumber++;
    }

    public (float, (Vector3, Quaternion)) CurrentDestinationAndTime()
    {
        currentPosID++;
        if (currentPosID >= capturedTransformsAndTime.Count)
        {
            currentPosID = 0;
            spawnedGhostCar.ResetInternalTimer();
            return (0f, (playerStartTransform.position, playerStartTransform.rotation));
        }

        return capturedTransformsAndTime[currentPosID];
    }
}
