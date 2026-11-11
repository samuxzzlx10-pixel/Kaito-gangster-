using UnityEngine;

public enum PoliceAlertState
{
    Clear,
    Chase,
    Search,
    Cooldown
}

public sealed class PoliceAlertSystem : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private int alertLevel;
    [SerializeField, Min(0.1f)] private float searchDuration = 8f;
    [SerializeField, Min(0.1f)] private float alertFalloffInterval = 4f;

    public int AlertLevel => alertLevel;
    public int MaxAlertLevel => 5;
    public PoliceAlertState State { get; private set; }

    private bool policeCanSeePlayer;
    private float searchTimer;
    private float cooldownTimer;

    private void Update()
    {
        if (alertLevel <= 0)
        {
            alertLevel = 0;
            State = PoliceAlertState.Clear;
            return;
        }

        if (policeCanSeePlayer)
        {
            State = PoliceAlertState.Chase;
            searchTimer = searchDuration;
            return;
        }

        if (State == PoliceAlertState.Chase)
        {
            State = PoliceAlertState.Search;
            searchTimer = searchDuration;
        }

        if (State == PoliceAlertState.Search)
        {
            searchTimer -= Time.deltaTime;
            if (searchTimer <= 0f)
            {
                State = PoliceAlertState.Cooldown;
                cooldownTimer = 0f;
            }
            return;
        }

        if (State == PoliceAlertState.Cooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= alertFalloffInterval)
            {
                cooldownTimer = 0f;
                alertLevel = Mathf.Max(0, alertLevel - 1);
                if (alertLevel == 0)
                    State = PoliceAlertState.Clear;
            }
        }
    }

    public void RaiseAlert(int amount = 1)
    {
        alertLevel = Mathf.Clamp(alertLevel + amount, 0, MaxAlertLevel);
        policeCanSeePlayer = true;
        State = alertLevel > 0 ? PoliceAlertState.Chase : PoliceAlertState.Clear;
    }

    public void SetPoliceCanSeePlayer(bool canSeePlayer)
    {
        policeCanSeePlayer = canSeePlayer;
        if (canSeePlayer)
        {
            State = PoliceAlertState.Chase;
            searchTimer = searchDuration;
        }
        else if (State == PoliceAlertState.Chase)
        {
            State = PoliceAlertState.Search;
            searchTimer = searchDuration;
        }
    }
}
