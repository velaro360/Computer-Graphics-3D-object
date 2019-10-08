using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GrzegorzJarząbek_Grafika2
{
    public partial class Form1 : Form
    {
        public Figure fig;
        public Point3D lightSource;
        public Figure auxiliaryFigure;
        public double AngleValueX = 0;
        public double AngleValueY = 0;
        public double AngleValueZ = 0;
        public int right = 0;
        public int left = 0;
        public int up = 0;
        public int down = 0;

        public class Point3D
        {
            public int x, y, z;

            public Point3D(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        public class Vector
        {
            public double x, y, z;

            public Vector(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        public class Triangle
        {
            public Point3D A, B, C;
            public int averageZ;

            public Triangle(Point3D a, Point3D b, Point3D c)
            {
                A = a;
                B = b;
                C = c;
            }
        }

        public class Figure : ICloneable
        {
            public List<Triangle> triangles;
            public Point3D centralPoint;

            public Figure()
            {
                triangles = new List<Triangle>();
            }

            public void Add(Triangle t)
            {
                triangles.Add(t);
            }

            public object Clone()
            {
                Figure clone = new Figure();

                foreach(Triangle t in this.triangles)
                {
                    clone.Add(
                        new Triangle(
                            new Point3D(t.A.x, t.A.y, t.A.z),
                            new Point3D(t.B.x, t.B.y, t.B.z),
                            new Point3D(t.C.x, t.C.y, t.C.z)
                            )
                        );
                }
                clone.centralPoint = new Point3D(this.centralPoint.x, this.centralPoint.y, this.centralPoint.z);

                return clone;
            }

            public void CountCentralPoint()
            {
                int pointsCount = this.triangles.Count * 3;

                int x =
                    (this.triangles.Sum(t => t.A.x) + this.triangles.Sum(t => t.B.x) + this.triangles.Sum(t => t.C.x)) / pointsCount;
                int y =
                    (this.triangles.Sum(t => t.A.y) + this.triangles.Sum(t => t.B.y) + this.triangles.Sum(t => t.C.y)) / pointsCount;
                int z =
                    (this.triangles.Sum(t => t.A.z) + this.triangles.Sum(t => t.B.z) + this.triangles.Sum(t => t.C.z)) / pointsCount;

                this.centralPoint = new Point3D(x, y, z);
            }

            public void Sort()
            {
                triangles.ForEach(t => t.averageZ = (t.A.z + t.B.z + t.C.z) / 3);
                triangles = triangles.OrderBy(t => t.averageZ).ToList();
            }
        }

        public void InitializeBaseView()
        {
            box.Image = new Bitmap("backgroundDark.bmp");
            fig = new Figure();
            
            # region Ściany
            //Podstawa:
            //przednia
            fig.Add(new Triangle(new Point3D(200, 200, 0), new Point3D(300, 200, 0), new Point3D(200, 300, 0)));
            fig.Add(new Triangle(new Point3D(200, 300, 0), new Point3D(300, 200, 0), new Point3D(300, 300, 0)));

            //tylna
            fig.Add(new Triangle(new Point3D(200, 300, -140), new Point3D(200, 200, -140), new Point3D(300, 200, -140)));
            fig.Add(new Triangle(new Point3D(200, 300, -140), new Point3D(300, 200, -140), new Point3D(300, 300, -140)));

            //lewa
            fig.Add(new Triangle(new Point3D(200, 200, 0), new Point3D(200, 200, -140), new Point3D(200, 300, 0)));
            fig.Add(new Triangle(new Point3D(200, 300, 0), new Point3D(200, 200, -140), new Point3D(200, 300, -140)));

            //prawa
            fig.Add(new Triangle(new Point3D(300, 200, 0), new Point3D(300, 300, 0), new Point3D(300, 200, -140)));
            fig.Add(new Triangle(new Point3D(300, 300, 0), new Point3D(300, 300, -140), new Point3D(300, 200, -140)));

            //górna
            fig.Add(new Triangle(new Point3D(200, 300, 0), new Point3D(300, 300, 0), new Point3D(200, 300, -140)));
            fig.Add(new Triangle(new Point3D(300, 300, 0), new Point3D(200, 300, -140), new Point3D(300, 300, -140)));

            //dolna
            fig.Add(new Triangle(new Point3D(200, 200, 0), new Point3D(300, 200, 0), new Point3D(200, 200, -140)));
            fig.Add(new Triangle(new Point3D(300, 200, 0), new Point3D(200, 200, -140), new Point3D(300, 200, -140)));

            ////Góra:
            ////przednia
            //fig.Add(new Triangle(new Point3D(220, 330, -10), new Point3D(220, 300, -10), new Point3D(280, 300, -10)));
            //fig.Add(new Triangle(new Point3D(220,330,-10), new Point3D(280,300,-10), new Point3D(280,330,-10)));

            ////tylna
            //fig.Add(new Triangle(new Point3D(220,300,-110), new Point3D(280,300,-110), new Point3D(220,330,-110)));
            //fig.Add(new Triangle(new Point3D(220,330,-110), new Point3D(280,330,-110), new Point3D(280,300,-110)));

            ////lewa
            //fig.Add(new Triangle(new Point3D(220,330,-10), new Point3D(220,300,-10), new Point3D(220,300,-110)));
            //fig.Add(new Triangle(new Point3D(220,330,-10), new Point3D(220,300,-110), new Point3D(220,330,-110)));

            ////prawa
            //fig.Add(new Triangle(new Point3D(280,330,-10), new Point3D(280,300,-10), new Point3D(280,300,-110)));
            //fig.Add(new Triangle(new Point3D(280,330,-10), new Point3D(280,300,-100), new Point3D(280,330,-110)));

            ////górna
            //fig.Add(new Triangle(new Point3D(220,330,-10), new Point3D(220,330,-110), new Point3D(280,330,-110)));
            //fig.Add(new Triangle(new Point3D(220,330,-10), new Point3D(280,330,-110), new Point3D(280,330,-10)));

            ////dolna
            //fig.Add(new Triangle(new Point3D(220,300,-10), new Point3D(280,300,-10), new Point3D(220,300,-110)));
            //fig.Add(new Triangle(new Point3D(280,300,-10), new Point3D(220,300,-110), new Point3D(280, 300, -110)));

            ////Lufa:
            ////przednia
            //fig.Add(new Triangle(new Point3D(245,325,70), new Point3D(245,315,70), new Point3D(255,315,70)));
            //fig.Add(new Triangle(new Point3D(245,325,70), new Point3D(255,315,70), new Point3D(255,325,70)));

            ////tylna
            //fig.Add(new Triangle(new Point3D(245,325,-10), new Point3D(245,315,-10), new Point3D(255,315,-10)));
            //fig.Add(new Triangle(new Point3D(245,325,-10), new Point3D(255,315,-10), new Point3D(255,325,-10)));

            ////lewa
            //fig.Add(new Triangle(new Point3D(245,325,70), new Point3D(245,315,70), new Point3D(245,315,-10)));
            //fig.Add(new Triangle(new Point3D(245,325,70), new Point3D(245,315,-40), new Point3D(245,315,-10)));

            ////prawa
            //fig.Add(new Triangle(new Point3D(255,325,70), new Point3D(255,315,70), new Point3D(255,315,-10)));
            //fig.Add(new Triangle(new Point3D(255,325,70), new Point3D(255,315,-10), new Point3D(255,325,-10)));

            ////górna
            //fig.Add(new Triangle(new Point3D(245,325,70), new Point3D(255,325,70), new Point3D(245,325,-10)));
            //fig.Add(new Triangle(new Point3D(255,325,70), new Point3D(245,325,-10), new Point3D(255,325,-10)));

            ////dolna
            //fig.Add(new Triangle(new Point3D(245,315,70), new Point3D(255,315,70), new Point3D(245,315,-10)));
            //fig.Add(new Triangle(new Point3D(255,315,70), new Point3D(245,315,-10), new Point3D(255,315,-10)));

            #endregion

            fig.CountCentralPoint();
            auxiliaryFigure = (Figure)fig.Clone();
            DrawFigure();
        }

        private void DrawLightSource()
        {
            Bitmap bm = (Bitmap)box.Image;

            #region Promienie
            //środek
            bm.SetPixel((int)lightSource.x, box.Height-(int)lightSource.y, Color.Yellow);

            //prawo
            for (int i = 0; i <= 20; i++) bm.SetPixel((int)lightSource.x + i, box.Height - (int)lightSource.y, Color.Yellow);
            //lewo
            for (int i = 0; i <= 20; i++) bm.SetPixel((int)lightSource.x - i, box.Height - (int)lightSource.y, Color.Yellow);
            //góra
            for (int i = 0; i <= 20; i++) bm.SetPixel((int)lightSource.x, box.Height - (int)lightSource.y + i, Color.Yellow);
            //dół
            for (int i = 0; i <= 20; i++) bm.SetPixel((int)lightSource.x, box.Height - (int)lightSource.y - i, Color.Yellow);

            //prawo-góra
            for (int i = 0; i <= 15; i++) bm.SetPixel((int)lightSource.x + i, box.Height - (int)lightSource.y + i, Color.Yellow);
            //lewo-góra
            for (int i = 0; i <= 15; i++) bm.SetPixel((int)lightSource.x - i, box.Height - (int)lightSource.y + i, Color.Yellow);
            //lewo-dół
            for (int i = 0; i <= 15; i++) bm.SetPixel((int)lightSource.x - i, box.Height - (int)lightSource.y - i, Color.Yellow);
            //prawo-dół
            for (int i = 0; i <= 15; i++) bm.SetPixel((int)lightSource.x + i, box.Height - (int)lightSource.y - i, Color.Yellow);
            #endregion

            box.Image = bm;
        }

        public void DrawFigure()
        {
            DrawLightSource();
            fig.CountCentralPoint();

            //algorytm malarza - sortowanie
            fig.Sort();
        }

        public Form1()
        {
            InitializeComponent();

            //lightSource = new Point3D(150, 50, 200);
            lightSource = new Point3D(100, 350, 200);
            InitializeBaseView();
        }

        private void Box_Paint(object sender, PaintEventArgs e)
        {
            Pen pen;

            foreach (Triangle t in fig.triangles)
            {
                pen = new Pen(LambertModel(t), 1);
                e.Graphics.DrawLine(pen, t.A.x, box.Height - t.A.y, t.B.x, box.Height - t.B.y);
                e.Graphics.DrawLine(pen, t.A.x, box.Height - t.A.y, t.C.x, box.Height - t.C.y);
                e.Graphics.DrawLine(pen, t.B.x, box.Height - t.B.y, t.C.x, box.Height - t.C.y);

                Point[] pointsPolygon = {
                    new Point(t.A.x, box.Height - t.A.y),
                    new Point(t.B.x, box.Height - t.B.y),
                    new Point(t.C.x, box.Height - t.C.y)
                };

                e.Graphics.FillPolygon(new SolidBrush(LambertModel(t)), pointsPolygon);
            }
        }

        public Color LambertModel(Triangle t)
        {
            Point3D[] L = new Point3D[2];

            L[0] = lightSource;
            L[1] = new Point3D((t.A.x + t.B.x + t.C.x) / 3, (t.A.y + t.B.y + t.C.y) / 3, (t.A.z + t.B.z + t.C.z) / 3);

            Vector vectorL, vectorN, line1, line2;

            line1 = new Vector(t.A.x - t.B.x, t.A.y - t.B.y, t.A.z - t.B.z);
            line2 = new Vector(t.A.x - t.C.x, t.A.y - t.C.y, t.A.z - t.C.z);

            vectorN =
                new Vector
                (line1.y * line2.z - line1.z * line2.y, line1.z * line2.x - line1.x * line2.z, line1.x * line2.y - line1.y * line2.x);
            vectorL = new Vector(L[0].x - L[1].x, L[0].y - L[1].y, L[0].z - L[1].z);

            double lengthN = Math.Sqrt(Math.Pow(vectorN.x, 2) + Math.Pow(vectorN.y, 2) + Math.Pow(vectorN.z, 2));
            double lengthL = Math.Sqrt(Math.Pow(vectorL.x, 2) + Math.Pow(vectorL.y, 2) + Math.Pow(vectorL.z, 2));

            vectorN.x /= lengthN;
            vectorN.y /= lengthN;
            vectorN.z /= lengthN;

            vectorL.x /= lengthL;
            vectorL.y /= lengthL;
            vectorL.z /= lengthL;

            double cos =
                (vectorL.x * vectorN.x + vectorL.y * vectorN.y + vectorL.z * vectorN.z) /(VectorLength(vectorL)*VectorLength(vectorN));

            int Ia = 160; //source light
            int Ip = 85;  //ambient light
            double I; //light intensity

            I = Ia + Ip * Math.Abs(cos);

            return Color.FromArgb(0, (int)(I), (int)(I));
        }

        private double VectorLength(Vector vec)
        {
            return Math.Sqrt(Math.Pow(vec.x, 2) + Math.Pow(vec.y, 2) + Math.Pow(vec.z, 2));
        }

        private void MoveFigure(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w')
                up += 5;

            if (e.KeyChar == 'a')
                left += 5;

            if (e.KeyChar == 's')
                down += 5;

            if (e.KeyChar == 'd')
                right += 5;

            if (e.KeyChar == 'x')
                AngleValueX += 0.04;

            if (e.KeyChar == 'y')
                AngleValueY += 0.04;

            if (e.KeyChar == 'z')
                AngleValueZ += 0.04;

            if (auxiliaryFigure != null)
                fig = (Figure)auxiliaryFigure.Clone();

            foreach (Triangle t in fig.triangles)
            {
                t.A.y += up;
                t.B.y += up;
                t.C.y += up;
                t.A.y -= down;
                t.B.y -= down;
                t.C.y -= down;

                t.A.x += right;
                t.B.x += right;
                t.C.x += right;
                t.A.x -= left;
                t.B.x -= left;
                t.C.x -= left;  
            }

            fig.CountCentralPoint();

            MoveFigureToZeroPoint();
            RotateX();
            RotateY();
            RotateZ();
            SetFigureToPreviousPoint();

            DrawFigure();
        }

        private void MoveFigureToZeroPoint()
        {
            int x = 0 - fig.centralPoint.x;
            int y = 0 - fig.centralPoint.y;
            int z = 0 - fig.centralPoint.z;

            foreach(Triangle t in fig.triangles)
            {
                t.A.x += x;
                t.B.x += x;
                t.C.x += x;

                t.A.y += y;
                t.B.y += y;
                t.C.y += y;

                t.A.z += z;
                t.B.z += z;
                t.C.z += z;
            }
        }

        public void SetFigureToPreviousPoint()
        {
            int x = 0 - fig.centralPoint.x;
            int y = 0 - fig.centralPoint.y;
            int z = 0 - fig.centralPoint.z;

            foreach (Triangle t in fig.triangles)
            {
                t.A.x -= x;
                t.B.x -= x;
                t.C.x -= x;

                t.A.y -= y;
                t.B.y -= y;
                t.C.y -= y;

                t.A.z -= z;
                t.B.z -= z;
                t.C.z -= z;
            }
        }

        private void RotateX()
        {
            foreach (Triangle t in fig.triangles)
            {
                int tmpY = t.A.y;
                t.A.y = (int)(Math.Cos(AngleValueX) * t.A.y - t.A.z * Math.Sin(AngleValueX));
                t.A.z = (int)(Math.Sin(AngleValueX) * tmpY + t.A.z * Math.Cos(AngleValueX));

                tmpY = t.B.y;
                t.B.y = (int)(Math.Cos(AngleValueX) * t.B.y - t.B.z * Math.Sin(AngleValueX));
                t.B.z = (int)(Math.Sin(AngleValueX) * tmpY + t.B.z * Math.Cos(AngleValueX));

                tmpY = t.C.y;
                t.C.y = (int)(Math.Cos(AngleValueX) * t.C.y - t.C.z * Math.Sin(AngleValueX));
                t.C.z = (int)(Math.Sin(AngleValueX) * tmpY + t.C.z * Math.Cos(AngleValueX));
            }
        }

        private void RotateY()
        {
            foreach (Triangle t in fig.triangles)
            {
                int tmpX = t.A.x;
                t.A.x = (int)(Math.Cos(AngleValueY) * t.A.x + Math.Sin(AngleValueY)*t.A.z);
                t.A.z = (int)(-Math.Sin(AngleValueY) * tmpX + t.A.z * Math.Cos(AngleValueY));

                tmpX = t.B.x;
                t.B.x = (int)(Math.Cos(AngleValueY) * t.B.x + Math.Sin(AngleValueY) * t.B.z);
                t.B.z = (int)(-Math.Sin(AngleValueY) * tmpX + t.B.z * Math.Cos(AngleValueY));

                tmpX = t.C.x;
                t.C.x = (int)(Math.Cos(AngleValueY) * t.C.x + Math.Sin(AngleValueY) * t.C.z);
                t.C.z = (int)(-Math.Sin(AngleValueY) * tmpX + t.C.z * Math.Cos(AngleValueY));
            }
        }

        private void RotateZ()
        {
            foreach (Triangle t in fig.triangles)
            {
                int tmpX = t.A.x;
                t.A.x = (int)(Math.Cos(AngleValueZ) * t.A.x - Math.Sin(AngleValueZ) * t.A.y);
                t.A.y = (int)(Math.Sin(AngleValueZ) * tmpX + Math.Cos(AngleValueZ) * t.A.y);

                tmpX = t.B.x;
                t.B.x = (int)(Math.Cos(AngleValueZ) * t.B.x - Math.Sin(AngleValueZ) * t.B.y);
                t.B.y = (int)(Math.Sin(AngleValueZ) * tmpX + Math.Cos(AngleValueZ) * t.B.y);

                tmpX = t.C.x;
                t.C.x = (int)(Math.Cos(AngleValueZ) * t.C.x - Math.Sin(AngleValueZ) * t.C.y);
                t.C.y = (int)(Math.Sin(AngleValueZ) * tmpX + Math.Cos(AngleValueZ) * t.C.y);
            }
        }
    }
}
