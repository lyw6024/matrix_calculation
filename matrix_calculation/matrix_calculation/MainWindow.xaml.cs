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
        List<RationalNumber> numList = new List<RationalNumber>();
        List<RationalNumber> vectList = new List<RationalNumber>();
        TexFormulaParser parser = new TexFormulaParser();
        string left_LaTeX, right_LaTeX;
        public MainWindow()
        {
            InitializeComponent();
            Mat_Text.Text = "1 1 1\n0 1 1\n 0 0 1";
            Vect_Text.Text = "3\n2\n1";
        }
       
        private void Mat_Text_KeyUp(object sender, KeyEventArgs e)
        {
            Res.Source=null;
            int N = 1;
            left_LaTeX= "";
            try
            {

                var lines = System.Text.RegularExpressions.Regex.Split(Mat_Text.Text.Trim(), @"\s+");
                numList.Clear();
                foreach (var it in lines)
                {
                    RationalNumber a;
                    if (it.Contains('/'))
                    {
                        var rat = it.Split('/');
                        a = new RationalNumber(Convert.ToInt32(rat[0]), Convert.ToInt32(rat[1]));
                    }
                    else
                    {
                        a = new RationalNumber(Convert.ToInt32(it));
                    }
                    numList.Add(a);
                    
                }

                while(numList.Count>=(N+1)*(N+1))
                {
                    N += 1;
                }
                if (numList.Count == N * N)
                {
                    left_LaTeX += @"\pmatrix{";
                    int idx = 0;
                    foreach (var it in numList)
                    {
                        if (idx > 0)
                        {
                            if (idx % N == 0)
                            {
                                left_LaTeX += @"\\";
                            }
                            else
                            {
                                left_LaTeX += "&";
                            }
                        }
                        left_LaTeX += it.ToLaTeX();
                        idx++;
                    }
                    left_LaTeX += "}";

                    var formula = parser.Parse(left_LaTeX + "X=" + right_LaTeX);
                    var pngBytes = formula.RenderToPng(20.0, 0.0, 0.0, "Arial");
                    MemoryStream ms = new MemoryStream(pngBytes, true);

                    BitmapImage p1 = new BitmapImage();
                    p1.BeginInit();
                    p1.StreamSource = ms;
                    p1.EndInit();
                    imgSrc.Source = p1;
                }
            }
            catch
            {
                ;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string latex = matrixAlgo.linearEquation(ref numList, ref vectList);
            var formula = parser.Parse(latex);
            var pngBytes = formula.RenderToPng(20.0, 0.0, 0.0, "Arial");
            MemoryStream ms = new MemoryStream(pngBytes, true);

            BitmapImage p1 = new BitmapImage();
            p1.BeginInit();
            p1.StreamSource = ms;
            p1.EndInit();
            Res.Source = p1;
        }

        private void Vect_Text_KeyUp(object sender, KeyEventArgs e)
        {
            Res.Source = null;
            try
            {
                vectList.Clear();
                var lines = System.Text.RegularExpressions.Regex.Split(Vect_Text.Text.Trim(), @"\s+");
                foreach (var it in lines)
                {
                    RationalNumber a;
                    if (it.Contains('/'))
                    {
                        var rat = it.Split('/');
                        a = new RationalNumber(Convert.ToInt32(rat[0]), Convert.ToInt32(rat[1]));
                    }
                    else
                    {
                        a = new RationalNumber(Convert.ToInt32(it));
                    }
                    vectList.Add(a);
                    
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
