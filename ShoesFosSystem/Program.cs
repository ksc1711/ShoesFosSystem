using ShoesFosSystem.Data;
using ShoesFosSystem.Forms;
using ShoesFosSystem.Services;

namespace ShoesFosSystem;

/// <summary>
/// 애플리케이션 진입점
/// </summary>
static class Program
{
    /// <summary>
    /// 애플리케이션의 주 진입점
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // DB 파일 존재 확인 후 스키마 생성 및 초기 데이터
        DatabaseHelper.EnsureSchema();
        DatabaseHelper.SeedInitialShoeTypesIfEmpty();

        // 앱 시작 시 자동 백업 (DB 파일이 있을 때만)
        try
        {
            BackupService.RunBackup();
        }
        catch
        {
            // 백업 실패해도 앱은 실행
        }

        // 기본 폰트 크기 (60세 이상 사용자 고려)
        Application.SetDefaultFont(new Font("맑은 고딕", 12F));

        Application.Run(new MainForm());

        // 앱 종료 시 자동 백업
        try
        {
            BackupService.RunBackup();
        }
        catch
        {
            // 무시
        }
    }
}
