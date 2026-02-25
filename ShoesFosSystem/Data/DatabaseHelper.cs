using Microsoft.Data.Sqlite;

namespace ShoesFosSystem.Data;

/// <summary>
/// SQLite DB 연결 및 스키마 초기화
/// </summary>
public static class DatabaseHelper
{
    private static string? _connectionString;

    /// <summary>
    /// DB 파일 경로 (실행 파일과 같은 폴더의 pos.db)
    /// </summary>
    public static string GetDbPath()
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(dir, "pos.db");
    }

    /// <summary>
    /// 연결 문자열 반환
    /// </summary>
    public static string GetConnectionString()
    {
        if (_connectionString != null) return _connectionString;
        var path = GetDbPath();
        _connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = path,
            Mode = SqliteOpenMode.ReadWriteCreate
        }.ToString();
        return _connectionString;
    }

    /// <summary>
    /// 새 연결 생성
    /// </summary>
    public static SqliteConnection CreateConnection()
    {
        var conn = new SqliteConnection(GetConnectionString());
        conn.Open();
        return conn;
    }

    /// <summary>
    /// DB 존재 여부
    /// </summary>
    public static bool DatabaseExists()
    {
        return File.Exists(GetDbPath());
    }

    /// <summary>
    /// 스키마 생성 및 WAL 모드 설정
    /// </summary>
    public static void EnsureSchema()
    {
        using var conn = CreateConnection();
        conn.ExecuteNonQuery("PRAGMA foreign_keys = ON;");
        conn.ExecuteNonQuery("PRAGMA journal_mode = WAL;");

        const string schema = """
            CREATE TABLE IF NOT EXISTS shoe_types (
              id INTEGER PRIMARY KEY AUTOINCREMENT,
              name TEXT NOT NULL UNIQUE,
              base_price INTEGER NOT NULL,
              is_active INTEGER NOT NULL DEFAULT 1,
              sort_order INTEGER DEFAULT 0
            );

            CREATE TABLE IF NOT EXISTS daily_counters (
              biz_date TEXT PRIMARY KEY,
              last_seq INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS orders (
              id INTEGER PRIMARY KEY AUTOINCREMENT,
              biz_date TEXT NOT NULL,
              daily_seq INTEGER NOT NULL,
              customer_name TEXT,
              customer_phone TEXT,
              status TEXT NOT NULL DEFAULT 'RECEIVED',
              memo TEXT,
              total_amount INTEGER NOT NULL DEFAULT 0,
              created_at TEXT NOT NULL,
              updated_at TEXT NOT NULL,
              UNIQUE (biz_date, daily_seq)
            );

            CREATE TABLE IF NOT EXISTS order_items (
              id INTEGER PRIMARY KEY AUTOINCREMENT,
              order_id INTEGER NOT NULL,
              shoe_type_id INTEGER NOT NULL,
              qty INTEGER NOT NULL DEFAULT 1,
              unit_price INTEGER NOT NULL,
              extra_price INTEGER NOT NULL DEFAULT 0,
              memo TEXT,
              line_total INTEGER NOT NULL,
              FOREIGN KEY(order_id) REFERENCES orders(id) ON DELETE CASCADE,
              FOREIGN KEY(shoe_type_id) REFERENCES shoe_types(id)
            );

            CREATE INDEX IF NOT EXISTS idx_orders_biz_date ON orders(biz_date);
            CREATE INDEX IF NOT EXISTS idx_items_order_id ON order_items(order_id);
            """;

        foreach (var stmt in schema.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var s = stmt.Trim();
            if (string.IsNullOrEmpty(s)) continue;
            conn.ExecuteNonQuery(s + ";");
        }
    }

    /// <summary>
    /// shoe_types가 비어 있으면 초기 데이터 삽입
    /// </summary>
    public static void SeedInitialShoeTypesIfEmpty()
    {
        using var conn = CreateConnection();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM shoe_types";
        var count = Convert.ToInt32(cmd.ExecuteScalar());
        if (count > 0) return;

        var inserts = new[]
        {
            "INSERT INTO shoe_types (name, base_price, is_active, sort_order) VALUES ('운동화', 10000, 1, 0)",
            "INSERT INTO shoe_types (name, base_price, is_active, sort_order) VALUES ('구두', 12000, 1, 1)",
            "INSERT INTO shoe_types (name, base_price, is_active, sort_order) VALUES ('부츠', 15000, 1, 2)"
        };
        foreach (var sql in inserts)
        {
            using var c = conn.CreateCommand();
            c.CommandText = sql;
            c.ExecuteNonQuery();
        }
    }
}

internal static class SqliteConnectionExtensions
{
    public static void ExecuteNonQuery(this SqliteConnection conn, string sql)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
}
