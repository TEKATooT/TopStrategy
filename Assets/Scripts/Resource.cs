using UnityEngine;

public class Resource : MonoBehaviour
{
    private bool _isFound = false;
    private bool _isBooking = false;
    private bool _isReadyToTake = false;
    private bool _isInPool = false;

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
