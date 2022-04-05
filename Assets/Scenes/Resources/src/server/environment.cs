public class environment
{
    public double[,] map;
    public int SIZE = 100;

    bit[,] rap;
    double dx = 10;

    public class bit
    {
        public double O;
        public double[] H ;
        public bit()
        {
            H = new double[6];
        }
    }

    public environment()
    {
        map = new double[SIZE, SIZE];
        rap = new bit[SIZE, SIZE];
        for (int i = 1; i < SIZE - 1; i++)
        {
            for (int j = 1; j < SIZE - 1; j++)
            {
                map[i, j] = 0;
                rap[i, j] = new bit();
            }
        }
        map[20, 20] = 600;
        map[50, 50] = 600;
    }
    public void update()
    {
        a();
        b();
    }
    void a()
    {
        for (int i = 1; i < SIZE - 1; i++)
        {
            for (int j = 1; j < SIZE - 1; j++)
            {
                rap[i, j].O = map[i + 1, j] + map[i, j + 1] + map[i, j - 1] + map[i - 1, j] + map[i + 1, j - 1] + map[i + 1, j + 1] - (6 * map[i, j]);
                rap[i, j].O /= dx * dx;
            }
        }
    }
    void b()
    {
        for (int i = 1; i < SIZE - 1; i++)
        {
            for (int j = 1; j < SIZE - 1; j++)
            {
                map[i, j] += rap[i, j].O;
            }
        }

    }
}



