using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    // 게임 매니저에게 보낼 신호
    public event System.Action<int> OnBlockPlaced;
    public event System.Action<int> OnPerfectBlock;
    public event System.Action OnGameOver;

    [Header("게임 설정")]
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Transform blockContainer;
    [SerializeField] private float blockMoveSpeed = 4.0f;
    [SerializeField] private float errorMargin = 0.1f;
    [SerializeField] private float stackMoveSpeed = 5.0f;

    private Block lastBlock;
    private Vector2 prevBlockPosition;
    private float currentBlockSize;
    private float moveBlockBoundary;
    private bool isGameActive = false;
    private int stackCount = 0;
    private Vector3 desiredPosition;
    private Color prevColor;
    private Color nextColor;

    private void Update()
    {
        if (!isGameActive) return;
        MoveCurrentBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, stackMoveSpeed * Time.deltaTime);
    }

    Color GetRandomColor()
    {
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }

    public void StartNewGame()
    {
        foreach (Transform child in blockContainer) Destroy(child.gameObject);
        currentBlockSize = 10f;
        moveBlockBoundary = currentBlockSize / 2 + 2.5f;
        prevBlockPosition = Vector2.down;
        stackCount = 0;
        desiredPosition = Vector3.zero;
        isGameActive = true;
        prevColor = GetRandomColor();
        nextColor = GetRandomColor();
        SpawnNewBlock();
        SpawnNewBlock();
    }

    public void StopGame()
    {
        isGameActive = false;
        if (lastBlock != null) lastBlock.gameObject.SetActive(false);
    }

    // 블럭 놓기
    public void PlaceBlock()
    {
        if (!isGameActive) return;

        Vector2 lastPos = lastBlock.transform.position;
        float overhang = lastPos.x - prevBlockPosition.x;

        if (Mathf.Abs(overhang) <= errorMargin)
        {
            lastBlock.transform.position = new Vector2(prevBlockPosition.x, lastPos.y);
            OnPerfectBlock?.Invoke(stackCount);
        }
        else
        {
            float newSize = currentBlockSize - Mathf.Abs(overhang);
            if (newSize <= 0)
            {
                OnGameOver?.Invoke();
                StopGame();
                return;
            }
            float middle = (lastPos.x + prevBlockPosition.x) / 2f;
            lastBlock.transform.position = new Vector2(middle, lastPos.y);
            lastBlock.transform.localScale = new Vector3(newSize, 1, 1);

            float rubblePosX = overhang > 0 ? middle + newSize / 2f : middle - newSize / 2f;
            CreateRubble(new Vector2(rubblePosX, lastPos.y), new Vector2(Mathf.Abs(overhang), 1));
            currentBlockSize = newSize;
            OnBlockPlaced?.Invoke(stackCount);
        }
        SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        if (lastBlock != null) prevBlockPosition = lastBlock.transform.position;
        GameObject newObj = Instantiate(blockPrefab);
        Block newBlock = newObj.GetComponent<Block>();

        newObj.transform.position = new Vector2(0, prevBlockPosition.y + 1f);
        newObj.transform.localScale = new Vector3(currentBlockSize, 1, 1);
        newObj.transform.SetParent(blockContainer);

        // 색상 로직
        float lerpT = (stackCount % 11) / 10f;
        Color applyColor = Color.Lerp(prevColor, nextColor, lerpT);
        newBlock.SetColor(applyColor);

        if (lerpT >= 1.0f)
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
        Camera.main.backgroundColor = applyColor * 0.6f;

        // 스택 이동 로직 (카운트 증가 및 목표 위치 계산)
        stackCount++;
        desiredPosition = Vector3.down * stackCount;
        lastBlock = newObj.GetComponent<Block>();
    }

    // 플레이어 입력 전 블럭이 움직이도록 하는 메서드
    private void MoveCurrentBlock()
    {
        if (lastBlock == null) return;
        float moveRange = moveBlockBoundary - (currentBlockSize / 2f);
        float positionX = Mathf.PingPong(Time.time * blockMoveSpeed, moveRange * 2) - moveRange;
        Vector3 pos = lastBlock.transform.position;
        pos.x = positionX;
        lastBlock.transform.position = pos;
    }

    // 완벽하게 클릭하지 못했을 때 Rubble 생성
    private void CreateRubble(Vector2 position, Vector2 scale)
    {
        GameObject rubbleObj = Instantiate(blockPrefab, transform);
        rubbleObj.GetComponent<Block>().SetColor(lastBlock.GetComponent<SpriteRenderer>().color);
        rubbleObj.transform.position = position;
        rubbleObj.transform.localScale = new Vector3(scale.x, scale.y, 1);
        Rigidbody2D rb = rubbleObj.AddComponent<Rigidbody2D>();
        rb.gravityScale = 3f;
        rubbleObj.tag = "Rubble"; // 생성된 조각에 "Rubble" 태그를 할당
    }
}
