using UnityEngine;

public class Car
{
    private float speed;
    public float Speed
    {
        get { return speed; }
    }

    private float accel;
    public float Accel
    {
        get { return accel; }
    }

    private string name;
    public string Name
    {
        set { name = value; }
        get { return name; }
    }

    private string brand;
    public string Brand
    {
        set { brand = value; }
        get { return brand; }
    }

    private int price;
    public int Price
    {
        set { price = value; }
        get { return price; }
    }

    private int wheelCount;

    public Car()
    {
        speed = 0.0f;
        accel = 0.0f;
        name = "";
        brand = "";
        price = 0;
        wheelCount = 0;
    }

    public Car(float speed, float accel, string name, string brand, int price, int wheelCount)
    {
        this.speed = speed;
        this.accel = accel;
        this.name = name;
        this.brand = brand;
        this.price = price;
        this.wheelCount = wheelCount;
    }
}
