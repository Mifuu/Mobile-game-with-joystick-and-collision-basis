using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundCollider
{
    //Circ and Circ collision
    public static bool Collision(CircBound c1, CircBound c2) {
        //find distance
        float dist = Mathf.Sqrt(Mathf.Pow(c2.transform.position.x-c1.transform.position.x,2) + Mathf.Pow(c2.transform.position.y-c1.transform.position.y,2));
        //check
        if (dist <= c1.radius + c2.radius) {
            return true;
        }
        return false;
    }

    //Circ and Rect collision
    public static bool Collision(RectBound r1, CircBound c1) {
        return Collision(c1, r1);
    }
    
    public static bool Collision(CircBound c1, RectBound r1) {
        float c1X = c1.transform.position.x;
        float c1Y = c1.transform.position.y;
        float r1X = r1.transform.position.x;
        float r1Y = r1.transform.position.y;

        //if applyRotation is true, then bound will be calculate with the rotation of the gameObject
        float r1Angle = (r1.applyRotation)? r1.gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad : 0;
        if (r1Angle != 0f) {
            float c1Dist = CMath.Distance(r1X, r1Y, c1X, c1Y);
            float c1IniAngleFromR1 = CMath.Angle(r1.transform.position, c1.transform.position);
            float c1FinAngleFromR1 = c1IniAngleFromR1 - r1Angle;//the final angle is c1IniAngleFromR1-r1Angle
            //replace coordinate values with new value
            c1X = r1X + c1Dist*Mathf.Cos(c1FinAngleFromR1);
            c1Y = r1Y + c1Dist*Mathf.Sin(c1FinAngleFromR1);
        }

        float c1DistX = Mathf.Abs(c1X - r1X);
        float c1DistY = Mathf.Abs(c1Y - r1Y);

        //https://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection
        //far out case
        if (c1DistX > (r1.extents.x + c1.radius)) {return false;}
        if (c1DistY > (r1.extents.y + c1.radius)) {return false;}
        //far in case
        if (c1DistX <= (r1.extents.x)) {return true;}
        if (c1DistY <= (r1.extents.y)) {return true;}
        //corner
        float cornerDistSqr = Mathf.Pow(c1DistX-r1.extents.x, 2) + Mathf.Pow(c1DistY-r1.extents.y, 2);
        return (cornerDistSqr <= Mathf.Pow(c1.radius,2));
    }
}
