namespace ShoesFosSystem.Models;

/// <summary>
/// 주문(접수) 엔티티
/// </summary>
public class Order
{
    /// <summary>PK</summary>
    public int Id { get; set; }

    /// <summary>업무일자 YYYY-MM-DD</summary>
    public string BizDate { get; set; } = string.Empty;

    /// <summary>당일 접수번호</summary>
    public int DailySeq { get; set; }

    /// <summary>고객명</summary>
    public string? CustomerName { get; set; }

    /// <summary>고객 전화</summary>
    public string? CustomerPhone { get; set; }

    /// <summary>상태: RECEIVED, COMPLETED, CANCELED</summary>
    public string Status { get; set; } = "RECEIVED";

    /// <summary>주문 메모</summary>
    public string? Memo { get; set; }

    /// <summary>주문 합계(원)</summary>
    public int TotalAmount { get; set; }

    /// <summary>생성일시</summary>
    public string CreatedAt { get; set; } = string.Empty;

    /// <summary>수정일시</summary>
    public string UpdatedAt { get; set; } = string.Empty;
}
