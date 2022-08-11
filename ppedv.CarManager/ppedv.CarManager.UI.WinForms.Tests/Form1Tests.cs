using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using FluentAssertions;
using System.Reflection;

namespace ppedv.CarManager.UI.WinForms.Tests
{
    public class Form1Tests
    {


        [Fact]
        public void Form1_click_loadbutton_should_show_some_rows_in_datagrid()
        {
            var path = Assembly.GetAssembly(typeof(Form1)).Location;
            path = path.Replace(".dll", ".exe");
            //var path = @"C:\Users\Fred\source\repos\Testing_2022\ppedv.CarManager\ppedv.CarManager.UI.WinForms\bin\Debug\net6.0-windows\ppedv.CarManager.UI.WinForms.exe";

            using var app = Application.Launch(path);
            using var automation = new UIA3Automation();
            var win = app.GetMainWindow(automation);
            var btn = win.FindFirstDescendant(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Button)).AsButton();
            btn.Click();
            Thread.Sleep(2000);
            var grid = win.FindFirstDescendant(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.DataGrid)).AsDataGridView();
            grid.Rows.Count().Should().BeGreaterThan(1);

            app.Close();
        }
    }
}