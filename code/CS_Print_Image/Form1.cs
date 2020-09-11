using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
//http://kenyanzi.blogspot.com/2011/05/c.html
//https://docs.microsoft.com/zh-tw/dotnet/api/system.windows.forms.printdialog?view=netcore-3.1
//http://blog.udn.com/pgferic/7457104
C# 的列印

step0. 引用 System.Drawing.Printing;

step1. 新增printDocument
    pdc001
step2. 點擊printDocument兩下開啟printDocument_PrintPage
    private void pdc001_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)

step3. 編輯printDocument_PrintPage
*/

namespace CS_Print_Image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //宣告一個一表機
            PrintDocument pd = new PrintDocument();
            //設定印表機邊界
            Margins margin = new Margins(0, 0, 0, 0);
            pd.DefaultPageSettings.Margins = margin;
            ////設定紙張大小 'vbPRPSUser'為使用者自訂
            PaperSize pageSize = new PaperSize("vbPRPSUser", 256, 256);
            pd.DefaultPageSettings.PaperSize = pageSize;
            //印表機事件設定
            pd.PrintPage += new PrintPageEventHandler(this.pdc001_PrintPage);

            try
            {
                //---直接列印 pd.Print();//列印

                /*
                //---
                //選擇印表機
                // Allow the user to choose the page range he or she would
                // like to print.
                pdlg001.AllowSomePages = true;

                // Show the help button.
                pdlg001.ShowHelp = true;

                // Set the Document property to the PrintDocument for 
                // which the PrintPage Event has been handled. To display the
                // dialog, either this property or the PrinterSettings property 
                // must be set 
                pdlg001.Document = pd;

                DialogResult result = pdlg001.ShowDialog();

                // If the result is OK then print the document.
                if (result == DialogResult.OK)
                {
                    pd.Print();
                }
                //---選擇印表機
                */

                //---
                //預覽列印
                PrintPreviewDialog PPD = new PrintPreviewDialog();

                PPD.Document = pd;

                PPD.ShowDialog();
                //---預覽列印

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "列印失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pd.PrintController.OnEndPrint(pd, new PrintEventArgs());
            }
        }

        private void pdc001_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //列印圖片
            string strDir;
            strDir = System.Windows.Forms.Application.StartupPath;
            strDir += "\\temp01.png";
            Image temp = Image.FromFile(strDir);//圖片檔案

            //設定圖片列印的x,y座標
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;

            //圖片列印的大小
            int width = temp.Width;//temp.Width;
            int height = temp.Height;//temp.Height;
            Rectangle destRect = new Rectangle(x, y, width, height);

            // public void DrawImage (
            // Image image, 要繪製的 Image。
            // Rectangle destRect, Rectangle 結構，指定繪製影像的位置和大小。縮放影像來符合矩形
            // int srcX, 要繪製之來源影像部分左上角的 X 座標
            // int srcY, 要繪製之來源影像部分左上角的 Y 座標。
            // int srcWidth, 要繪製之來源影像部分的寬度。
            // int srcHeight, 要繪製之來源影像部分的高度。
            // GraphicsUnit srcUnit, GraphicsUnit 列舉型別的成員，指定用來判斷來源矩形的測量單位。
            // ImageAttributes imageAttr ImageAttributes，指定 image 物件的重新著色和 Gamma 資訊。
            //)
            //將圖片放入要列印的文件中
            e.Graphics.DrawImage(temp, destRect, 10, 10, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);

            //列印文字
            System.Drawing.Font printFont = new System.Drawing.Font("Arial", 10);//設定字型與大小
            float leftMargin = e.MarginBounds.Left;//取得文件左邊界
            float topMargin = e.MarginBounds.Top;//取得文件上邊界
            int count = 10;//起始列印的行數
            float yPos = 0f;//收集成列印起始點的參數
            yPos = topMargin + count * printFont.GetHeight(e.Graphics);

            // public void DrawString (
            // string s, 要繪製的字串。
            // Font font, Font，定義字串的文字格式。
            // Brush brush, Brush，決定所繪製文字的色彩和紋理。
            // float x, 繪製文字左上角的 X 座標。
            // float y, 繪製文字左上角的 Y 座標。
            // StringFormat format StringFormat，指定套用到所繪製文字的格式化屬性，例如，行距和對齊。
            //)
            //將要列印的文字放入要列印的文件中

            //jash 停用 e.Graphics.DrawString("C#有夠讚", printFont, Brushes.Black, 100, yPos, new StringFormat());
        }
    }
}
