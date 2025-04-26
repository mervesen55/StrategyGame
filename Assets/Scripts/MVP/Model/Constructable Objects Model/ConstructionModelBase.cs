using UnityEngine;

public abstract class ConstructionModelBase<TData> where TData : ScriptableObject
{
    public TData Data { get; protected set; }

    public virtual void Init(TData data)
    {
        Data = data;
    }
    public int CurrentHealth { get; protected set; }

    public void IncreaseHealth(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
    }

    public bool IsDestroyed => CurrentHealth <= 0;

    public Vector2Int SpawnStartGrid { get; set; }

}
