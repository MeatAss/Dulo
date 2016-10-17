namespace Dulo.GameObjects.Labyrinth
{
    public class Indexes
    {
        public int i { get; set; }
        public int j { get; set; }

        public Indexes(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public override string ToString()
        {
            return "i:" + i.ToString() + "j:" + j.ToString();
        }
    }
}
