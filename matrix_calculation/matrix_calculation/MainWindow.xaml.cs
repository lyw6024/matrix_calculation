using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using WpfMath;

namespace matrix_calculation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<rationalNumber> numList = new List<rationalNumber>();
        List<rationalNumber> vectList = new List<rationalNumber>();
        TexFormulaParser parser = new TexFormulaParser();
        string left_LaTeX, right_LaTeX;
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void Mat_Text_KeyUp(object sender, KeyEventArgs e)
        {
            left_LaTeX= "";
            try
            {
                var lines = Mat_Text.Text.Split('\n');

                left_LaTeX += @"\pmatrix{";
                for (int k = 0; k < lines.Length; k++)
                {
                    if (k > 0)
                    {
                        left_LaTeX += @"\\";
                    }
                    var msg = lines[k].Split(' ');
                    for (int i = 0; i < msg.Length; i++)
                    {
                        if (i > 0)
                        {
                            left_LaTeX += " & ";
                        }
                        rationalNumber a;
                        if (msg[i].Contains('/'))
                        {
                            var rat = msg[i].Split('/');
                            a = new rationalNumber(Convert.ToInt32(rat[0]), Convert.ToInt32(rat[1]));
                        }
                        else
                        {
                            a = new rationalNumber(Convert.ToInt32(msg[i]));
                        }
                        numList.Add(a);
                        left_LaTeX += a.ToLaTeX();
                    }

                }

                left_LaTeX += "}";
            
                var formula = parser.Parse(left_LaTeX+"X="+right_LaTeX);
                var pngBytes = formula.RenderToPng(20.0, 0.0, 0.0, "Arial");
                MemoryStream ms = new MemoryStream(pngBytes, true);

                BitmapImage p1 = new BitmapImage();
                p1.BeginInit();
                p1.StreamSource = ms;
                p1.EndInit();
                imgSrc.Source = p1;
            }
            catch
            {
                ;
            }
        }

        private void Vect_Text_KeyUp(object sender, KeyEventArgs e)
        {


            try
            {
                vectList.Clear();
                var lines = Vect_Text.Text.Split('\n');
                foreach (var line in lines)
                {
                    var msg = line.Split(' ');
                    foreach(var it in msg)
                    {
                        rationalNumber a;
                        if (it.Contains('/'))
                        {
                            var rat = it.Split('/');
                            a = new rationalNumber(Convert.ToInt32(rat[0]), Convert.ToInt32(rat[1]));
                        }
                        else
                        {
                            a = new rationalNumber(Convert.ToInt32(it));
                        }
                        vectList.Add(a);
                    }
                }
                right_LaTeX = @"\pmatrix{";
                for (int i=0;i<vectList.Count;i++)
                {
                    if(i>0)
                    {
                        right_LaTeX += @"\\";
                    }
                    right_LaTeX += vectList[i].ToLaTeX();
                }
                right_LaTeX += "}";
                var formula = parser.Parse(left_LaTeX+"X="+right_LaTeX);
                var pngBytes = formula.RenderToPng(20.0, 0.0, 0.0, "Arial");
                MemoryStream ms = new MemoryStream(pngBytes, true);

                BitmapImage p1 = new BitmapImage();
                p1.BeginInit();
                p1.StreamSource = ms;
                p1.EndInit();
                imgSrc.Source = p1;
            }
            catch
            {
                ;
            }
        }
    }
}
