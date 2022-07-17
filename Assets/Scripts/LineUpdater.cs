using UnityEngine;

public class LineUpdater : MonoBehaviour {
   public LineRenderer line;
   public Transform start, end;

   private void Update() {
      line.positionCount = 2;
      line.SetPosition(0,start.position );
      line.SetPosition(1,end.position );
   }
}
