using UnityEngine;

public class GameOfLife : ProcessingLite.GP21
{
	GameCell[,] cells;
	float cellSize = 0.25f;
	int numberOfColums, numberOfRows;
	int spawnChancePercentage = 15;

    void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 4;

		numberOfColums = (int)Mathf.Floor(Width / cellSize);
		numberOfRows = (int)Mathf.Floor(Height / cellSize);

		cells = new GameCell[numberOfColums, numberOfRows];

		for (int y = 0; y < numberOfRows; y++)
		{
			for (int x = 0; x < numberOfColums; x++)
			{
				cells[x, y] = new GameCell(x * cellSize, y * cellSize, cellSize);

				if (Random.Range(0, 100) < spawnChancePercentage)
				{
					cells[x, y].alive = true;
				}
			}
		}
	}

    void Update()
    {
        Background(0);

        for (int y = 1; y < numberOfRows - 1; ++y)
        {
            for (int x = 1; x < numberOfColums - 1; ++x)
            {
                cells[x, y].last = Adjacent(x, y);
            }
        }

        for (int y = 0; y < numberOfRows; ++y)
        {
            for (int x = 0; x < numberOfColums; ++x)
            {
                if (cells[x, y].last < 2 || cells[x, y].last > 3)
                {
                    cells[x, y].alive = false;
                }

                if (cells[x, y].alive == false && cells[x, y].last == 3)
                {
                    cells[x, y].alive = true;
                }
            }
        }

        for (int y = 0; y < numberOfRows; ++y)
        {
            for (int x = 0; x < numberOfColums; ++x)
            {
                cells[x, y].Draw();
            }
        }
    }

    public int Adjacent(int x, int y)
    {
        int adjacent = 0;


        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) 
                { 
                    continue;
                }
                else if (cells[x + i, y + j].alive)
                {
                    adjacent++;
                }
            }
        }

        return adjacent;
    }
}

public class GameCell : ProcessingLite.GP21
{
	float x, y;
	float size;
    public int last;
    public bool alive = false;

	public GameCell(float x, float y, float size)
	{

		this.x = x + size / 2;
		this.y = y + size / 2;

		this.size = size / 2;
	}

	public void Draw()
	{
		if (alive)
		{
            Circle(x, y, size);
		}
    }
}