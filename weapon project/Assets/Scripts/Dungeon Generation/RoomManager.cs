using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject startRoomPrefab;
    [SerializeField] GameObject bossRoomPrefab;
    [SerializeField] private int minRooms = 7;
    [SerializeField] private int maxRooms = 12;

    [SerializeField] int roomWidth = 20;
    [SerializeField] int roomHeight = 12;

    [SerializeField] int gridSizeX = 16;
    [SerializeField] int gridSizeY = 16;

    private List<GameObject> roomObjects = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>(); 

    private int[,] roomGrid;

    private int roomCount;
    private bool generationComplete = false;


    private void Start() 
    {
        roomGrid = new int[gridSizeX, gridSizeY]; // Mapeia a grid para dar track aos quartos criados.
        roomQueue = new Queue<Vector2Int>(); // Uma lista para comportamento FIFO

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2); // Arranja as coordenadas do centro da grid,
        StartRoomGenerationFromRoom(initialRoomIndex); // para criar o start room e a geração procedural dos restantes quartos a partir do centro.
    }

    private void Update() {
        if (roomQueue.Count > 0 && roomCount < maxRooms && !generationComplete)
        {
            Vector2Int roomIndex = roomQueue.Dequeue(); // Vai buscar as primeiras coordenadas da lista, remove-o da lista
            int gridX = roomIndex.x;
            int gridY = roomIndex.y;

            // e tenta criar uma nova sala em 4 direções.
            TryGenerateRoom(new Vector2Int(gridX, gridY - 1)); // Tenta criar para cima,
            TryGenerateRoom(new Vector2Int(gridX + 1, gridY)); // para a direita,
            TryGenerateRoom(new Vector2Int(gridX, gridY + 1)); // para baixo,
            TryGenerateRoom(new Vector2Int(gridX - 1, gridY)); // para a esquerda.
        }
        else if(roomCount < minRooms) // "Geração completa" com um número inferior de salas que o mínimo desejado.
        {
            Debug.Log("Rooms didn't reach minimum amount. Retrying.");
            RegenerateRooms();
        }
        else if (!generationComplete) // 
        {
            Debug.Log($"Generation completed, {roomCount} rooms generated.");
            generationComplete = true;

            if(!GenerateBossRoom()){
                Debug.Log("Wasn't able to add boss room. Retrying.");

            }
        } 
    }

    private void StartRoomGenerationFromRoom(Vector2Int roomIndex) 
    { // Cria o primeiro quarto do nível.
        roomQueue.Enqueue(roomIndex); // Coloca as coordenadas no início da fila para a posição da próxima criação se basear nela.
        int x = roomIndex.x;
        int y = roomIndex.y;

        AddRoomToLevel(roomIndex, true);
    }

    private void AddRoomToLevel(Vector2Int roomIndex, bool isStartRoom = false, bool isBossRoom = false)
    {
        roomGrid[roomIndex.x, roomIndex.y] = 1; // Declara no mapa a existência desta sala.

        if (isBossRoom) 
        {
            var bossRoom = Instantiate(bossRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity); // Instanceia o primeiro quarto no centro sem alterações de rotação.
            bossRoom.name = $"Room-Boss";
            bossRoom.GetComponent<Room>().RoomIndex = roomIndex; // Guarda as coordenadas de mapa do quarto no quarto.
            roomObjects.Add(bossRoom); // Guarda a referência deste GameObject numa lista.  
            OpenDoors(bossRoom, roomIndex.x, roomIndex.y);
        }
        else if (isStartRoom)
        {
            var startRoom = Instantiate(startRoomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity); // Instanceia o primeiro quarto no centro sem alterações de rotação.
            roomCount++;
            startRoom.name = $"Room-Spawn";
            startRoom.GetComponent<Room>().RoomIndex = roomIndex; // Guarda as coordenadas de mapa do quarto no quarto.
            roomObjects.Add(startRoom); // Guarda a referência deste GameObject numa lista. 
            OpenDoors(startRoom, roomIndex.x, roomIndex.y);  
        }
        else
        {
            var regularRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity); // Instanceia o primeiro quarto no centro sem alterações de rotação.
            roomCount++;
            regularRoom.name = $"Room-{roomCount}";
            regularRoom.GetComponent<Room>().RoomIndex = roomIndex; // Guarda as coordenadas de mapa do quarto no quarto.
            roomObjects.Add(regularRoom); // Guarda a referência deste GameObject numa lista.  
            OpenDoors(regularRoom, roomIndex.x, roomIndex.y); 
        }
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (CountAdjacentRooms(roomIndex) > 1 || // Não permite mais que três quartos ligados
        roomGrid[x, y] != 0 || // Não permite room overlapping
        roomCount >= maxRooms ||
        UnityEngine.Random.value < 0.5f && roomIndex != Vector2.zero) // Uma maneira de tornar a criação mais "aleatória"
            return false;

        roomQueue.Enqueue(roomIndex); // Coloca as coordenadas no início da fila para a posição da próxima criação se basear nela.

        /*
        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity); // Instanceia o primeiro quarto no centro sem alterações de rotação.
        newRoom.GetComponent<Room>().RoomIndex = roomIndex; // Guarda as coordenadas de mapa do quarto no quarto.
        newRoom.name = $"Room-{roomCount}";
        roomObjects.Add(newRoom); // Guarda a referência deste GameObject numa lista.*/

        AddRoomToLevel(roomIndex);

        return true;
    }

    private bool GenerateBossRoom()
    {
        Vector2Int lastRoomPos = roomObjects[roomObjects.Count - 1].GetComponent<Room>().RoomIndex;
        
        if (lastRoomPos.x > 0 && roomGrid[lastRoomPos.x - 1, lastRoomPos.y] != 0) 
        {
            Debug.Log("Last Room has room to the left!");
            if (CountAdjacentRooms(new Vector2Int(lastRoomPos.x + 1, lastRoomPos.y)) <= 1) 
            {
                Debug.Log("Generating boss room to the right of the last room!");
                AddRoomToLevel(new Vector2Int(lastRoomPos.x + 1, lastRoomPos.y), false, true);
                return true;
            }
        }

        if (lastRoomPos.x < gridSizeX - 1 && roomGrid[lastRoomPos.x + 1, lastRoomPos.y] != 0) 
        {  
            Debug.Log("Last Room has room to the right!");
            if (CountAdjacentRooms(new Vector2Int(lastRoomPos.x - 1, lastRoomPos.y)) <= 1) 
            {
                Debug.Log("Generating boss room to the left of the last room!");
                AddRoomToLevel(new Vector2Int(lastRoomPos.x - 1, lastRoomPos.y), false, true);
                return true;
            }
        }

        if (lastRoomPos.y > 0 && roomGrid[lastRoomPos.x, lastRoomPos.y - 1] != 0) 
        {
            Debug.Log("Last Room has room to the down!");
            if (CountAdjacentRooms(new Vector2Int(lastRoomPos.x, lastRoomPos.y + 1)) <= 1) 
            {
                Debug.Log("Generating boss room to the up of the last room!");
                AddRoomToLevel(new Vector2Int(lastRoomPos.x, lastRoomPos.y + 1), false, true);
                return true;
            }
        }

        if (lastRoomPos.y < gridSizeY - 1 && roomGrid[lastRoomPos.x, lastRoomPos.y + 1] != 0) 
        {
            Debug.Log("Last Room has room to the up!");
            if (CountAdjacentRooms(new Vector2Int(lastRoomPos.x, lastRoomPos.y - 1)) <= 1) 
            {
                Debug.Log("Generating boss room to the downs of the last room!");
                AddRoomToLevel(new Vector2Int(lastRoomPos.x, lastRoomPos.y - 1), false, true);
                return true;
            }
        }

        return false;
    }

    void OpenDoors(GameObject room, int x, int y) 
    {   // Mostra as portas para ligar as salas adjacentes
        Room newRoomScript = room.GetComponent<Room>(); // Referência à sala criada

        Room upperRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));

        // Ativa as portas dos quartos adjacentes
        if (x > 0 && roomGrid[x - 1, y] != 0) 
        {
            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);

            // Guarda os destinos das portas nas portas
            newRoomScript.leftDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(leftRoomScript.RoomIndex);
            leftRoomScript.rightDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(newRoomScript.RoomIndex);
        }
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0)
        {
            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);

            // Guarda os destinos das portas nas portas
            newRoomScript.rightDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(rightRoomScript.RoomIndex);
            rightRoomScript.leftDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(newRoomScript.RoomIndex);
        }
        if (y > 0 && roomGrid[x, y - 1] != 0) 
        {
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);

            // Guarda os destinos das portas nas portas
            newRoomScript.bottomDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(bottomRoomScript.RoomIndex);
            bottomRoomScript.topDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(newRoomScript.RoomIndex);
        }
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) 
        {
            newRoomScript.OpenDoor(Vector2Int.up);
            upperRoomScript.OpenDoor(Vector2Int.down);

            // Guarda os destinos das portas nas portas
            newRoomScript.topDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(upperRoomScript.RoomIndex);
            upperRoomScript.bottomDoor.GetComponent<Door>().myDestinationPos = GetPositionFromGridIndex(newRoomScript.RoomIndex);
        }
    }

    Room GetRoomScriptAt(Vector2Int index) 
    {   // Devolve as referências das salas adjacentes
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);

        if (roomObject != null) 
            return roomObject.GetComponent<Room>();
        
        return null;
    }

    private void RegenerateRooms()
    {   // Algo correu mal, apagar tudo e começar de novo!
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int [gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        generationComplete = false;

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private int CountAdjacentRooms(Vector2Int roomIndex) 
    {   // Conta os quartos em redor das coordenadas do parametro.
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (x > 0 && roomGrid[x - 1, y] != 0) count++; // Um quarto à esquerda
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++; // Um quarto à direita
        if (y > 0 && roomGrid[x, y - 1] != 0) count++; // Um quarto acima
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++; //Um quarto abaixo

        return count;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex) 
    { // Traduz as coordenadas do mapa para posições para os quartos. 
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;

        return new Vector3(roomWidth * (gridX - gridSizeX / 2), 
            roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos() 
    { // Desenha uma grid no editor para visualizar melhor o mapeamento feito com GetPositionFromIndex(...)
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++) 
        {
            for (int y = 0; y < gridSizeY; y++) 
            {
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }

}
