using Microsoft.Data.Sqlite;
using ShoesFosSystem.Models;

namespace ShoesFosSystem.Data;

/// <summary>
/// 신발 종류(가격표) CRUD
/// </summary>
public static class ShoeTypeRepository
{
    /// <summary>
    /// 활성 포함 전체 목록 (sort_order, name 순)
    /// </summary>
    public static List<ShoeType> GetAll(bool activeOnly = false)
    {
        var list = new List<ShoeType>();
        using var conn = DatabaseHelper.CreateConnection();
        var sql = activeOnly
            ? "SELECT id, name, base_price, is_active, sort_order FROM shoe_types WHERE is_active = 1 ORDER BY sort_order, name"
            : "SELECT id, name, base_price, is_active, sort_order FROM shoe_types ORDER BY sort_order, name";
        using var cmd = new SqliteCommand(sql, conn);
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            list.Add(new ShoeType
            {
                Id = r.GetInt32(0),
                Name = r.GetString(1),
                BasePrice = r.GetInt32(2),
                IsActive = r.GetInt32(3),
                SortOrder = r.GetInt32(4)
            });
        }
        return list;
    }

    /// <summary>
    /// ID로 조회
    /// </summary>
    public static ShoeType? GetById(int id)
    {
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand("SELECT id, name, base_price, is_active, sort_order FROM shoe_types WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;
        return new ShoeType
        {
            Id = r.GetInt32(0),
            Name = r.GetString(1),
            BasePrice = r.GetInt32(2),
            IsActive = r.GetInt32(3),
            SortOrder = r.GetInt32(4)
        };
    }

    /// <summary>
    /// 추가 후 ID 반환
    /// </summary>
    public static int Insert(ShoeType entity)
    {
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand(
            "INSERT INTO shoe_types (name, base_price, is_active, sort_order) VALUES (@name, @basePrice, @isActive, @sortOrder); SELECT last_insert_rowid();", conn);
        cmd.Parameters.AddWithValue("@name", entity.Name);
        cmd.Parameters.AddWithValue("@basePrice", entity.BasePrice);
        cmd.Parameters.AddWithValue("@isActive", entity.IsActive);
        cmd.Parameters.AddWithValue("@sortOrder", entity.SortOrder);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    /// <summary>
    /// 수정
    /// </summary>
    public static void Update(ShoeType entity)
    {
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand(
            "UPDATE shoe_types SET name=@name, base_price=@basePrice, is_active=@isActive, sort_order=@sortOrder WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("@id", entity.Id);
        cmd.Parameters.AddWithValue("@name", entity.Name);
        cmd.Parameters.AddWithValue("@basePrice", entity.BasePrice);
        cmd.Parameters.AddWithValue("@isActive", entity.IsActive);
        cmd.Parameters.AddWithValue("@sortOrder", entity.SortOrder);
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// 비활성화(판매중지)
    /// </summary>
    public static void SetInactive(int id)
    {
        using var conn = DatabaseHelper.CreateConnection();
        using var cmd = new SqliteCommand("UPDATE shoe_types SET is_active = 0 WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
