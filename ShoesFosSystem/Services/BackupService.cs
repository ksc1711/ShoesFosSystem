using ShoesFosSystem.Data;

namespace ShoesFosSystem.Services;

/// <summary>
/// DB 자동 백업 및 백업 폴더 관리
/// </summary>
public static class BackupService
{
    private const int MaxBackupCount = 30;

    /// <summary>
    /// 백업 폴더 경로 (실행 파일 기준 backup)
    /// </summary>
    public static string GetBackupFolder()
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(dir, "backup");
    }

    /// <summary>
    /// 지금 시점 백업 파일명 pos_yyyyMMdd_HHmm.db
    /// </summary>
    public static string GetBackupFileName()
    {
        return $"pos_{DateTime.Now:yyyyMMdd_HHmm}.db";
    }

    /// <summary>
    /// DB 파일 복사하여 backup 폴더에 저장, 30개 초과 시 오래된 것 삭제
    /// </summary>
    public static string? RunBackup()
    {
        var dbPath = DatabaseHelper.GetDbPath();
        if (!File.Exists(dbPath)) return null;

        var backupDir = GetBackupFolder();
        Directory.CreateDirectory(backupDir);
        var destPath = Path.Combine(backupDir, GetBackupFileName());
        File.Copy(dbPath, destPath, overwrite: true);

        var files = Directory.GetFiles(backupDir, "pos_*.db")
            .Select(f => new FileInfo(f))
            .OrderBy(fi => fi.CreationTime)
            .ToList();
        while (files.Count > MaxBackupCount)
        {
            files[0].Delete();
            files.RemoveAt(0);
        }
        return destPath;
    }

    /// <summary>
    /// 백업 폴더 탐색기로 열기
    /// </summary>
    public static void OpenBackupFolder()
    {
        var folder = GetBackupFolder();
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        System.Diagnostics.Process.Start("explorer.exe", folder);
    }
}
