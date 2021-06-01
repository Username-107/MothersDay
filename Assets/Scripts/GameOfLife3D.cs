using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GameOfLife3D : GameOfLife {

    public GameObject selectionIndicator;
    public float cameraDistancePlacement = 10.0f;
    private Vector3 CELL_PIVOT_OFFSET = new Vector3(0.5f, 0.5f, 0.5f);
    private GameObject[,,] grid;
    private int width = 24;
    private int height = 24;
    private int depth = 24;

    protected override void Awake() {
        grid = new GameObject[width, height, depth];
        selectionIndicator = Instantiate(selectionIndicator);
        selectionIndicator.transform.SetParent(gameBoard);
    }

    private void Update() {
        if (!gamePaused) {
            elapsedTime += Time.deltaTime;
            int ticksPassed = Mathf.FloorToInt(elapsedTime / tickInterval);
            elapsedTime -= ticksPassed * tickInterval;
            for (int i = 0; i < ticksPassed; i++) {
                Tick();
            }
        } else {
            Vector3 pos = gameCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistancePlacement));
            Vector3Int boardPos = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
            selectionIndicator.transform.position = boardPos;
            if (boardPos.x >= 0 && boardPos.x <= width && boardPos.y >= 0 && boardPos.y <= height && boardPos.z >= 0 && boardPos.z <= depth) {
                selectionIndicator.SetActive(true);
            } else {
                selectionIndicator.SetActive(false);
            }
            bool mouseClicked = Input.GetMouseButtonDown(0);
            if (mouseClicked) {
                ChangeCell(boardPos);
            }
        }
    }

    public override void Tick() {
        OnGenerationChanged(++generation);
        List<Vector3Int> toBringToLife = new List<Vector3Int>();
        List<Vector3Int> toBeKilled = new List<Vector3Int>();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                for (int k = 0; k < depth; k++) {
                    bool cellIsAlive = grid[i, j, k] != null;
                    int numNeighbours = GetNumNeighbours(i, j, k);
                    if (cellIsAlive) {
                        if (numNeighbours < 7 || numNeighbours > 12) {
                            toBeKilled.Add(new Vector3Int(i, j, k));
                        }
                    } else {
                        if (numNeighbours >= 10 && numNeighbours <= 12) {
                            toBringToLife.Add(new Vector3Int(i, j, k));
                        }
                    }
                }
            }
        }
        foreach (Vector3Int cell in toBringToLife) {
            CreateCell(cell);
        }
        foreach (Vector3Int cell in toBeKilled) {
            DestroyCell(cell);
        }
    }

    public override void Restart() {
        OnPauseStateChanged(gamePaused = true);
        foreach (GameObject cell in grid) {
            if (cell != null) {
                Destroy(cell);
            }
        }
        Array.Clear(grid, 0, grid.Length);
        OnGenerationChanged(generation = 0);
    }

    private int GetNumNeighbours(int x, int y, int z) {
        int numNeighbours = 0;

        int minXRange = x > 0 ? -1 : 0;
        int maxXRange = x < width - 1 ? 1 : 0;
        int minYRange = y > 0 ? -1 : 0;
        int maxYRange = y < height - 1 ? 1 : 0;
        int minZRange = z > 0 ? -1 : 0;
        int maxZRange = z < depth - 1 ? 1 : 0;

        for (int i = minXRange; i <= maxXRange; i++) {
            for (int j = minYRange; j <= maxYRange; j++) {
                for (int k = minZRange; k <= maxZRange; k++) {
                    if (i == 0 && j == 0 && k == 0) { // Don't count ourselves
                        continue;
                    }
                    bool neighbourIsAlive = grid[x + i, y + j, z + k] != null;
                    numNeighbours += neighbourIsAlive ? 1 : 0;
                }
            }
        }
        return numNeighbours;
    }

    private void ChangeCell(Vector3Int cellPos) {
        try {
            GameObject cell = grid[cellPos.x, cellPos.y, cellPos.z];
            if (cell == null) {
                CreateCell(cellPos);
            } else {
                DestroyCell(cellPos);
            }
        }
        catch {
            return;
        }
    }

    private void CreateCell(Vector3Int cellPos) {
        GameObject newCell = Instantiate(cellPrefab);
        newCell.transform.SetParent(gameBoard);
        newCell.transform.position = cellPos;
        grid[cellPos.x, cellPos.y, cellPos.z] = newCell;
    }

    private void DestroyCell(Vector3Int cellPos) {
        GameObject deadCell = grid[cellPos.x, cellPos.y, cellPos.z];
        if (deadCell != null) {
            Destroy(deadCell);
        }
        grid[cellPos.x, cellPos.y, cellPos.z] = null;
    }

}
