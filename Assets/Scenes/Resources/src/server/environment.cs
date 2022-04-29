public class environment
{
    public double[,] map;
    public int SIZE = 100;

    bit[,] rap;
    public double[,] vector1;
    double dx = 0.01;

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
        vector1 = new double[SIZE, SIZE];
        rap = new bit[SIZE, SIZE];
        for (int i = 1; i < SIZE - 1; i++)
        {
            for (int j = 1; j < SIZE - 1; j++)
            {
                map[i, j] = 0;
                vector1[i, j] = 0;
                rap[i, j] = new bit();
            }
        }
        map[21, 21] = 6000;
        map[60, 60] = 6000;
        map[40, 60] = 6000;
        for (int i=0;i<10;i++)
        {
            a();
            b();
        }
    }
    public void update()
    {
        wave();
        waveb();
    }
    void a()
    {
        for (int i = 2; i < SIZE - 2; i++)
        {
            for (int j = 2; j < SIZE - 2; j++)
            {
                if (j%2!=0)
                {
                    rap[i, j].O = map[i + 1, j] + map[i, j + 1] + map[i, j - 1] + map[i - 1, j] + map[i + 1, j + 1] + map[i + 1, j - 1] - (6 * map[i, j]);
                }
                else
                {
                    rap[i, j].O = map[i + 1, j] + map[i, j + 1] + map[i, j - 1] + map[i - 1, j] + map[i - 1, j + 1] + map[i - 1, j - 1] - (6 * map[i, j]);
                }
                
                rap[i, j].O /= 0.01 * 0.01;
            }
        }
    }
    void wave()
    {
        for (int i = 2; i < SIZE - 2; i++)
        {
            for (int j = 2; j < SIZE - 2; j++)
            {
                if (j % 2 != 0)
                {
                    rap[i, j].O = map[i + 1, j] + map[i, j + 1] + map[i, j - 1] + map[i - 1, j] + map[i + 1, j + 1] + map[i + 1, j - 1];
                }
                else
                {
                    rap[i, j].O = map[i + 1, j] + map[i, j + 1] + map[i, j - 1] + map[i - 1, j] + map[i - 1, j + 1] + map[i - 1, j - 1];
                }

                rap[i, j].O /= dx;
                vector1[i, j] += rap[i, j].O;
            }
        }
    }
    void b()
    {
        for (int i = 1; i < SIZE - 1; i++)
        {
            for (int j = 1; j < SIZE - 1; j++)
            {
                map[i, j] += rap[i, j].O * dx;
            }
        }

    }
    void waveb()
    {
        for (int i = 1; i < SIZE - 1; i++)
        {
            for (int j = 1; j < SIZE - 1; j++)
            {
                map[i, j] += vector1[i, j]*5;
            }
        }

    }
}



