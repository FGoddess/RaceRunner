using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport _connectedTeleport;
    [SerializeField] private Transform _placeToTeleport;

    public Transform PlaceToTeleport => _placeToTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover player) || other.TryGetComponent(out AIMover bot))
        {
            var temp = other.GetComponent<CharacterController>();
            temp.enabled = false;
            other.transform.position = new Vector3(_connectedTeleport.PlaceToTeleport.position.x, _connectedTeleport.PlaceToTeleport.position.y, other.transform.position.z);
            temp.enabled = true;
        }
    }
}
