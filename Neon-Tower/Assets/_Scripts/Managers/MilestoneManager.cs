using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MilestoneManager : MonoBehaviour
{
    
    [SerializeField]
    private Transform _milestonePlatformParent; //Should be located at Environment
    [SerializeField]
    private GameObject _milestonePlatformPrefab;
    [SerializeField]
    private List<int> _openMilestoneMarks;
    [SerializeField]
    private List<int> _completeMilestoneMarks; // Has no use jet, might me used in Scoreboard later.
    private bool _goalReached = false;
    public static MilestoneManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void CheckMilestoneReached(int CurrentHeight)
    {
        if (_openMilestoneMarks.Count <= 0 && _goalReached)
        {
            return;
        }
        else if (_openMilestoneMarks.Count <= 0 && !_goalReached)
        {
            ReachedGoal();
            return;
        }

        int smallestOpenMilestone = _openMilestoneMarks.Min();
        if (CurrentHeight > smallestOpenMilestone)
        {
            MileStoneReached(CurrentHeight, smallestOpenMilestone);
        }
    }

    private void ReachedGoal()
    {
        _goalReached = true;
        ScoreScreenManager.Instance.ShowScoreScreen();
    }

    private void MileStoneReached(int CurrentHeight, int smallestOpenMilestone)
    {
        _completeMilestoneMarks.Add(smallestOpenMilestone);
        _openMilestoneMarks.Remove(smallestOpenMilestone);
        CompleteMileStoneAt(CurrentHeight);
        FreezeAllObjectsBelow(CurrentHeight);
        MoveDestructionZoneToMilestone(CurrentHeight);
        WindManager.Instance.IncreaseWindInsensity();
    }

    private void MoveDestructionZoneToMilestone(int CurrentHeight)
    {
        int offset = 2;
        GameObject destructionZone = GameObject.FindWithTag("DestructionZone");
        Vector3 destructionZonePosition = destructionZone.transform.position;
        destructionZone.transform.position = new(destructionZonePosition.x, CurrentHeight-offset ,destructionZonePosition.z);
    }

    private void FreezeAllObjectsBelow(int currentHeight)
    {
        PlacedBuilding[] buildings = FindObjectsOfType<PlacedBuilding>();

        foreach (var building in buildings)
        {
            if (!building.IsFalling)
            {
                building.Freeze();
            }
        }
    }

    private void CompleteMileStoneAt(float currentHeight)
    {
        Quaternion rotation = Quaternion.identity;
        Vector3 position = new(0, currentHeight, 0);
        Instantiate(_milestonePlatformPrefab, position, rotation, _milestonePlatformParent);
        SFXManager.Instance.PlayMilestone();
    }
}
