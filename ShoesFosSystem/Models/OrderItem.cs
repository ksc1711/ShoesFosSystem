namespace ShoesFosSystem.Models;

/// <summary>
/// 주문 품목(신발 1켤레 라인) 엔티티
/// </summary>
public class OrderItem
{
    /// <summary>PK</summary>
    public int Id { get; set; }

    /// <summary>주문 ID</summary>
    public int OrderId { get; set; }

    /// <summary>신발종류 ID</summary>
    public int ShoeTypeId { get; set; }

    /// <summary>수량</summary>
    public int Qty { get; set; } = 1;

    /// <summary>단가(주문 시점 스냅샷)</summary>
    public int UnitPrice { get; set; }

    /// <summary>추가금</summary>
    public int ExtraPrice { get; set; }

    /// <summary>품목 메모</summary>
    public string? Memo { get; set; }

    /// <summary>라인 합계</summary>
    public int LineTotal { get; set; }

    /// <summary>신발종류 이름(조회용, DB 컬럼 아님)</summary>
    public string? ShoeTypeName { get; set; }
}
