using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport _connectedTeleport;
    [SerializeField] private Transform _placeToTeleport;

    [SerializeField] private bool _needRotation;

    public Transform PlaceToTeleport => _placeToTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover player) || other.TryGetComponent(out AIMover bot))
        {
            var ñontroller = other.GetComponent<CharacterController>();
            ñontroller.enabled = false;

            if (_needRotation)
            {
                other.transform.eulerAngles = new Vector3(other.transform.eulerAngles.x, -other.transform.eulerAngles.y, other.transform.eulerAngles.z);
            }

            other.transform.position = new Vector3(_connectedTeleport.PlaceToTeleport.position.x, _connectedTeleport.PlaceToTeleport.position.y, other.transform.position.z);

            ñontroller.enabled = true;
        }
    }
}
