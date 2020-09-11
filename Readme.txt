C# 的列印圖片(CS_Print_Image)

資料來源:
http://kenyanzi.blogspot.com/2011/05/c.html
https://docs.microsoft.com/zh-tw/dotnet/api/system.windows.forms.printdialog?view=netcore-3.1
http://blog.udn.com/pgferic/7457104

step0. 引用 System.Drawing.Printing;

step1. 新增printDocument
    pdc001
step2. 點擊printDocument兩下開啟printDocument_PrintPage
    private void pdc001_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)

step3. 編輯printDocument_PrintPage