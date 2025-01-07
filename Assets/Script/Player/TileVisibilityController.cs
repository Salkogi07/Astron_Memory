using UnityEngine;
using UnityEngine.Tilemaps;

public class TileVisibilityController : MonoBehaviour
{
    public Tilemap tilemap;            // 타일맵
    public Transform targetObject;    // 타일을 기준으로 볼 오브젝트
    public float visibilityRange = 5f; // 보이는 범위 (월드 단위)

    void Update()
    {
        // 중심 타일 위치를 계산
        Vector3Int targetTilePos = tilemap.WorldToCell(targetObject.position);

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // 타일의 월드 좌표를 계산
            Vector3 worldPos = tilemap.GetCellCenterWorld(pos);

            // 거리 계산
            float distance = Vector3.Distance(worldPos, targetObject.position);

            if (distance <= visibilityRange)
            {
                // 범위 안의 타일은 보이도록 설정
                tilemap.SetTileFlags(pos, TileFlags.None);
                tilemap.SetColor(pos, Color.white);
            }
            else
            {
                // 범위 밖의 타일은 투명하게 설정
                tilemap.SetTileFlags(pos, TileFlags.None);
                tilemap.SetColor(pos, new Color(1, 1, 1, 0)); // 투명화
            }
        }
    }
}
