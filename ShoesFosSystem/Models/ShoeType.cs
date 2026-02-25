namespace ShoesFosSystem.Models;

/// <summary>
/// 신발 종류(가격표) 엔티티
/// </summary>
public class ShoeType
{
    /// <summary>PK</summary>
    public int Id { get; set; }

    /// <summary>신발 종류 이름</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>기본 가격(원)</summary>
    public int BasePrice { get; set; }

    /// <summary>활성 여부 (1:판매중, 0:판매중지)</summary>
    public int IsActive { get; set; } = 1;

    /// <summary>표시 순서</summary>
    public int SortOrder { get; set; }
}
