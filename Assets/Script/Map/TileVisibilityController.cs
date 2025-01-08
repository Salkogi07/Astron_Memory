using UnityEngine;
using UnityEngine.Tilemaps;

public class TileVisibilityController : MonoBehaviour
{
    public Tilemap[] tilemaps;        // ���� Ÿ�ϸ�
    public Transform targetObject;   // Ÿ���� �������� �� ������Ʈ
    public float visibilityRange = 5f; // ���̴� ���� (���� ����)

    void Update()
    {
        foreach (var tilemap in tilemaps)
        {
            // �߽� Ÿ�� ��ġ�� ���
            Vector3Int targetTilePos = tilemap.WorldToCell(targetObject.position);

            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                // Ÿ���� ���� ��ǥ�� ���
                Vector3 worldPos = tilemap.GetCellCenterWorld(pos);

                // �Ÿ� ���
                float distance = Vector3.Distance(worldPos, targetObject.position);

                if (distance <= visibilityRange)
                {
                    // ���� ���� Ÿ���� ���̵��� ����
                    tilemap.SetTileFlags(pos, TileFlags.None);
                    tilemap.SetColor(pos, Color.white);
                }
                else
                {
                    // ���� ���� Ÿ���� �����ϰ� ����
                    tilemap.SetTileFlags(pos, TileFlags.None);
                    tilemap.SetColor(pos, new Color(1, 1, 1, 0)); // ����ȭ
                }
            }
        }
    }
}
