public class GeneralShape
{
    public int number { get; set; }
    public int id { get; set; }
    public float x { get; set; }
    public float y { get; set; }

    public float angle { get; set; }

    public GeneralShape(int number, int id, float x, float y, float angle)
    {
        this.number = number;
        this.id = id;
        this.x = x;
        this.y = y;
        this.angle = angle;
    }

}