using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private bool _isFound = false;
    [SerializeField] private bool _isBooking = false;
    [SerializeField] private bool _isReadyToTake = false;
    [SerializeField] private bool _isInPool = false;

    public bool IsFound => _isFound;
    public bool IsBooking => _isBooking;
    public bool IsReadyToTaken => _isReadyToTake;
    public bool IsInPool => _isInPool;

    public void Found()
    {
        _isFound = true;
    }

    public void Booking()
    {
        _isBooking = true;
    }

    public void ToTake()
    {
        _isReadyToTake = true;
    }

    public void TakeToPool()
    {
        _isInPool = true;

        gameObject.SetActive(false);
    }
}
