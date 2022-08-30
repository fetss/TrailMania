using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private float acceleration;

    private CarInputHandler input;

    private Car car;

    private Rigidbody2D rid;

    private void Awake()
    {
        car = GetComponent<Car>();
        input = GetComponent<CarInputHandler>();
        rid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        car = GetComponent<Car>();
        input = GetComponent<CarInputHandler>();
        rid = GetComponent<Rigidbody2D>();
        //rid.centerOfMass = new Vector2(rid.centerOfMass.x, -0.25f);
    }

    void FixedUpdate()
    {
        //if (GameManager.GetGameMode() == GameMode.Single)
        //{
        //UpdateMovement(new CarNetworking.MoveData(input.LeftTouch, input.RightTouch));
        //}
    }

    public void UpdateMovement(CarNetworking.MoveData moveData)
    {

        if (!car.ownerPlayer.isRacing) return;

        if (car.wheelBack.Grounded && car.wheelFront.Grounded)
        {
            rid.gravityScale = 0.2f;
        }
        else
        {
            rid.gravityScale = 1;
        }

        if (moveData.RightTouch && moveData.LeftTouch)
        {
            //rid.velocity = new Vector2(rid.velocity.x * (1 - Time.fixedDeltaTime * 1f), rid.velocity.y);
            rid.angularVelocity *= 1 - Time.fixedDeltaTime * 5f;
        }
        else if (moveData.RightTouch)
        {
            if (car.wheelBack.Grounded)
            {
                Vector2 p = Vector2.Perpendicular(car.wheelBack.ContactPosition - car.wheelBack.transform.position);
                //p *= acceleration / (0.66f + System.Convert.ToInt32(car.wheelFront.Grounded)) * Time.fixedDeltaTime;
                rid.AddForceAtPosition(p.normalized * acceleration * rid.mass / (0.66f + System.Convert.ToInt32(car.wheelFront.Grounded)), car.wheelBack.transform.position, ForceMode2D.Force);
                //rid.velocity += p;
                //rid.angularVelocity += Vector2.Dot(p / rid.inertia, new Vector2(car.wheelBack.ContactPosition.x, car.wheelBack.ContactPosition.y) - rid.worldCenterOfMass);
                //rid.AddForceAtPosition(p.normalized * acceleration * rid.mass, car.wheelBack.ContactPosition, ForceMode2D.Force);
            }

            if (car.wheelFront.Grounded)
            {
                Vector2 p = Vector2.Perpendicular(car.wheelFront.ContactPosition - car.wheelFront.transform.position);
                //p *= acceleration / (0.66f + System.Convert.ToInt32(car.wheelBack.Grounded)) * Time.fixedDeltaTime;
                rid.AddForceAtPosition(p.normalized * acceleration * rid.mass / (0.66f + System.Convert.ToInt32(car.wheelBack.Grounded)), car.wheelFront.transform.position, ForceMode2D.Force);
                //rid.velocity += p;
                //rid.angularVelocity += Vector2.Dot(p / rid.inertia, new Vector2(car.wheelFront.ContactPosition.x, car.wheelFront.ContactPosition.y) - rid.worldCenterOfMass);
                //rid.AddForceAtPosition(p.normalized * acceleration * rid.mass, car.wheelFront.ContactPosition, ForceMode2D.Force);
            }
        }

        else if (moveData.LeftTouch)
        {
            if (car.wheelBack.Grounded)
            {
                Vector2 p = Vector2.Perpendicular(car.wheelBack.ContactPosition - car.wheelBack.transform.position);
                //p *= acceleration / (0.66f + System.Convert.ToInt32(car.wheelFront.Grounded)) * Time.fixedDeltaTime;
                rid.AddForceAtPosition(-p.normalized * acceleration * rid.mass / (0.66f + System.Convert.ToInt32(car.wheelFront.Grounded)), car.wheelBack.transform.position, ForceMode2D.Force);
                //rid.velocity += p;
                //rid.angularVelocity += Vector2.Dot(p / rid.inertia, new Vector2(car.wheelBack.ContactPosition.x, car.wheelBack.ContactPosition.y) - rid.worldCenterOfMass);
                //rid.AddForceAtPosition(-p.normalized * acceleration * rid.mass, car.wheelBack.ContactPosition, ForceMode2D.Force);
            }

            if (car.wheelFront.Grounded)
            {
                Vector2 p = Vector2.Perpendicular(car.wheelFront.ContactPosition - car.wheelFront.transform.position);
                //p *= acceleration / (0.66f + System.Convert.ToInt32(car.wheelBack.Grounded)) * Time.fixedDeltaTime;
                rid.AddForceAtPosition(-p.normalized * acceleration * rid.mass / (0.66f + System.Convert.ToInt32(car.wheelBack.Grounded)), car.wheelFront.transform.position, ForceMode2D.Force);
                //rid.velocity += p;
                //rid.angularVelocity += Vector2.Dot(p / rid.inertia, new Vector2(car.wheelFront.ContactPosition.x, car.wheelFront.ContactPosition.y) - rid.worldCenterOfMass);
                //rid.AddForceAtPosition(-p.normalized * acceleration * rid.mass, car.wheelFront.ContactPosition, ForceMode2D.Force);
            }
        }
    }

    public CarNetworking.ReconcileData GetReconcileData()
    {
        return new CarNetworking.ReconcileData(transform.position, rid.velocity, transform.rotation, rid.angularVelocity, rid.gravityScale);
    }

    public void ReconcileData(CarNetworking.ReconcileData rd)
    {
        transform.position = rd.Position;
        rid.velocity = rd.Velocity;
        transform.rotation = rd.Rotation;
        rid.angularVelocity = rd.AngularVelocity;
        rid.gravityScale = rd.GravityScale;
    }
}
