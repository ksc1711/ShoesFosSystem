using ShoesFosSystem.Data;
using ShoesFosSystem.Forms;

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

        // DB 스키마 생성 및 초기 데이터
        DatabaseHelper.EnsureSchema();
        DatabaseHelper.SeedInitialShoeTypesIfEmpty();

        // 기본 폰트 크기 (60세 이상 사용자 고려)
        Application.SetDefaultFont(new Font("맑은 고딕", 12F));

        Application.Run(new MainForm());
    }
}
