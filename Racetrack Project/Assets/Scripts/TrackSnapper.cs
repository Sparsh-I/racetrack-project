using UnityEngine;

public class TrackSnapper : MonoBehaviour
{
    // Snap points for this track piece
    public Transform frontSnapPoint; // Point at the front of this piece
    public Transform backSnapPoint;  // Point at the back of this piece
    public float snapRange = 0.5f;   // Distance within which snapping is allowed

    // Called when dragging the object
    void OnMouseDrag()
    {
        // Move the object to the mouse's position in the world
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            // Set this track piece's position to the hit point (e.g., terrain or other collider)
            transform.position = hit.point;

            // Attempt to snap to other pieces
            SnapToNearestPiece();
        }
    }

    // Snap this piece to the nearest compatible track piece
    void SnapToNearestPiece()
    {
        // Get all track pieces in the scene
        TrackSnapper[] otherPieces = FindObjectsOfType<TrackSnapper>();

        foreach (TrackSnapper piece in otherPieces)
        {
            // Skip checking against itself
            if (piece == this) continue;

            // Check if the front snap point can connect to another piece's back snap point
            float distanceToBack = Vector3.Distance(frontSnapPoint.position, piece.backSnapPoint.position);
            if (distanceToBack < snapRange)
            {
                SnapToPiece(piece.backSnapPoint, frontSnapPoint);
                return; // Stop further checks once snapped
            }

            // Check if the back snap point can connect to another piece's front snap point
            float distanceToFront = Vector3.Distance(backSnapPoint.position, piece.frontSnapPoint.position);
            if (distanceToFront < snapRange)
            {
                SnapToPiece(piece.frontSnapPoint, backSnapPoint);
                return; // Stop further checks once snapped
            }
        }
    }

    // Snap this piece to another piece's snap point
    void SnapToPiece(Transform targetSnapPoint, Transform ownSnapPoint)
    {
        // Calculate the offset between this piece's snap point and its position
        Vector3 offset = ownSnapPoint.position - transform.position;

        // Adjust this piece's position so its snap point aligns with the target's snap point
        transform.position = targetSnapPoint.position - offset;

        // Rotate this piece to align with the target snap point
        transform.rotation = targetSnapPoint.rotation;
    }
}
