
public class SensorContinuous : Sensor
{
    //maybe add settings to sense not only every frame, but every second, third, fourth, fifth and so on - to increase performance.

    // Update is called once per frame
    void Update()
    {
        Sense();
    }
}
