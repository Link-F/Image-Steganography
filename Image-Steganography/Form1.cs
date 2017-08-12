using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                String filename = openFileDialog.FileName;

                // Bilddatei öffnen
                FileStream imageStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                // Bild in die Picturebox setzen
                pictureBox1.Image = System.Drawing.Image.FromStream(imageStream);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                String filename = openFileDialog.FileName;

                // Bilddatei öffnen
                FileStream imageStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                // Bild in die Picturebox setzen
                pictureBox3.Image = System.Drawing.Image.FromStream(imageStream);
            }
        }


        // Text in Bitebene verstecken
        private void button1_Click(object sender, EventArgs e)
        {
            // 8 Bitebenen erstellen
            Bitmap bitebene1 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene2 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene3 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene4 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene5 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene6 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene7 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene8 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

            // Dem bitebenen array zuweisen
            Bitmap[] bitebenen_bilder = new Bitmap[8];
            bitebenen_bilder[0] = bitebene1;
            bitebenen_bilder[1] = bitebene2;
            bitebenen_bilder[2] = bitebene3;
            bitebenen_bilder[3] = bitebene4;
            bitebenen_bilder[4] = bitebene5;
            bitebenen_bilder[5] = bitebene6;
            bitebenen_bilder[6] = bitebene7;
            bitebenen_bilder[7] = bitebene8;

            // Das Bitmap in dem alle Bitebenen bilder zusammen draufkommen
            Bitmap bitebenen_gesamt = new Bitmap(pictureBox1.Width, pictureBox1.Height);


            byte[] pic = new byte[pictureBox1.Image.Width * pictureBox1.Image.Height * 3];
            int i = 0;


            // Speichern der RGB Werte aller Pixel des Bildes
            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    pic[i++] = ((Bitmap)pictureBox1.Image).GetPixel(x, y).R;
                    pic[i++] = ((Bitmap)pictureBox1.Image).GetPixel(x, y).G;
                    pic[i++] = ((Bitmap)pictureBox1.Image).GetPixel(x, y).B;
                }
            }

            int temp = 7;
            // Erstellen der 8 Bitebenen Bilder
            for (int picture = 0; picture <= 7; picture++)
            {
                i = 0;
                int R, G, B;
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox1.Image.Height; y++)
                    {

                        R = (byte)((pic[i]) & (int)Math.Pow(2, picture)); // Bitebenenbilder herausrechnen
                        G = (byte)((pic[i + 1]) & (int)Math.Pow(2, picture)); // Bitebenenbilder herausrechnen
                        B = (byte)((pic[i + 2]) & (int)Math.Pow(2, picture)); // Bitebenenbilder herausrechnen


                        // Die Farbwerte in das Array schreiben erst Rot Grün Blau index 3 und so weiter
                        bitebenen_bilder[picture].SetPixel(x, y, Color.FromArgb(R, G, B));
                        i = i + 3;
                    }
                }
                temp--;
            }

            // Bitebenen Auswahl
            int ebene = 0;

            if (radioButton1.Checked)
                ebene = 0;
            if (radioButton2.Checked)
                ebene = 1;
            if (radioButton3.Checked)
                ebene = 2;
            if (radioButton4.Checked)
                ebene = 3;
            if (radioButton5.Checked)
                ebene = 4;
            if (radioButton6.Checked)
                ebene = 5;
            if (radioButton7.Checked)
                ebene = 6;
            if (radioButton8.Checked)
                ebene = 7;

 
            // Die Zeichenkette zu einem Bitmap Bild machen
            Bitmap string_pic = draw_string_to_bitmap(new Bitmap(pictureBox1.Image.Width,pictureBox1.Image.Height),
                                                    textBox1.Text, new PointF(Int32.Parse(textbox_x.Text), Int32.Parse(textbox_y.Text)));

            // Die Zeichenkette und das Bitebenenbild addieren und zuweisen
            bitebenen_bilder[ebene] = add_bitmaps(bitebenen_bilder[ebene], string_pic);


            // Das präparierte Bild in die Picturebox einfügen
            Bitmap prep_pic = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);


            for (int picture = 0; picture < 8; picture++)
            {
                for (int x = 0; x < prep_pic.Width; x++)
                {
                    for (int y = 0; y < prep_pic.Height; y++)
                    {
                        int r1 = bitebenen_bilder[0].GetPixel(x, y).R;
                        int g1 = bitebenen_bilder[0].GetPixel(x, y).G;
                        int b1 = bitebenen_bilder[0].GetPixel(x, y).B;

                        int r2 = bitebenen_bilder[1].GetPixel(x, y).R;
                        int g2 = bitebenen_bilder[1].GetPixel(x, y).G;
                        int b2 = bitebenen_bilder[1].GetPixel(x, y).B;

                        int r3 = bitebenen_bilder[2].GetPixel(x, y).R;
                        int g3 = bitebenen_bilder[2].GetPixel(x, y).G;
                        int b3 = bitebenen_bilder[2].GetPixel(x, y).B;

                        int r4 = bitebenen_bilder[3].GetPixel(x, y).R;
                        int g4 = bitebenen_bilder[3].GetPixel(x, y).G;
                        int b4 = bitebenen_bilder[3].GetPixel(x, y).B;

                        int r5 = bitebenen_bilder[4].GetPixel(x, y).R;
                        int g5 = bitebenen_bilder[4].GetPixel(x, y).G;
                        int b5 = bitebenen_bilder[4].GetPixel(x, y).B;

                        int r6 = bitebenen_bilder[5].GetPixel(x, y).R;
                        int g6 = bitebenen_bilder[5].GetPixel(x, y).G;
                        int b6 = bitebenen_bilder[5].GetPixel(x, y).B;

                        int r7 = bitebenen_bilder[6].GetPixel(x, y).R;
                        int g7 = bitebenen_bilder[6].GetPixel(x, y).G;
                        int b7 = bitebenen_bilder[6].GetPixel(x, y).B;

                        int r8 = bitebenen_bilder[7].GetPixel(x, y).R;
                        int g8 = bitebenen_bilder[7].GetPixel(x, y).G;
                        int b8 = bitebenen_bilder[7].GetPixel(x, y).B;


                        int red = (r1 + r2 + r3 + r4 + r5 + r6 + r7 + r8)%255;
                        int green = (g1 + g2 + g3 + g4 + g5 + g6 + g7 + g8)%255;
                        int blue = (b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8)%255;

                        if (red > 255)
                            red = 255;
                        if (green > 255)
                            green = 255;
                        if (blue > 255)
                            blue = 255;


                        prep_pic.SetPixel(x, y, Color.FromArgb(red ,green, blue));

                    }
                }
            }
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.Image = prep_pic;
        }


        // Funktion die einen String in ein Bitmap Bild umwandelt
        public Bitmap StringToBitmap(string text, Font font, Color ForeColor, Color BackColor)
        {

            SolidBrush objBrushForeColor = new SolidBrush(ForeColor);
            SolidBrush objBrushBackColor = new SolidBrush(BackColor);

            Point objPoint = new Point(0, 0);

            Font temp = new Font(textBox1.Font.Name, textBox1.Font.Size+20,
                textBox1.Font.Style, textBox1.Font.Unit);


            Bitmap objBitmap = new Bitmap(Width, Height);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBitmap);

            objGraphics.FillRectangle(objBrushBackColor, 0, 0, textBox1.Text.Length*35, 80);
            objGraphics.DrawString(text, font, objBrushForeColor, objPoint);

            return objBitmap;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog dia = new SaveFileDialog();

            if (dia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bild = new Bitmap(pictureBox3.Image);

                bild.Save(dia.FileName);
            }
        }


        // Bitebenen des Bildes aus Picturebox1 anzeigen
        private void button2_Click(object sender, EventArgs e)
        {
            // 8 Bitebenen bitmaps erstellen
            Bitmap bitebene1 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene2 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene3 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene4 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene5 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene6 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene7 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Bitmap bitebene8 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

            // Dem bitebenen array zuweisen
            Bitmap[] bitebenen_bilder = new Bitmap[8];
            bitebenen_bilder[0] = bitebene1;
            bitebenen_bilder[1] = bitebene2;
            bitebenen_bilder[2] = bitebene3;
            bitebenen_bilder[3] = bitebene4;
            bitebenen_bilder[4] = bitebene5;
            bitebenen_bilder[5] = bitebene6;
            bitebenen_bilder[6] = bitebene7;
            bitebenen_bilder[7] = bitebene8;

            // Das Bitmap für alle Bitebenen
            Bitmap bitebenen_gesamt = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            byte[] pic = new byte[pictureBox1.Image.Width * pictureBox1.Image.Height * 3];
            int i = 0;


        // Speichern der RGB Werte aller Pixel des Bildes
        for (int x = 0; x < pictureBox1.Image.Width; x++)
        {
            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                pic[i++] = ((Bitmap)pictureBox1.Image).GetPixel(x, y).R;
                pic[i++] = ((Bitmap)pictureBox1.Image).GetPixel(x, y).G;
                pic[i++] = ((Bitmap)pictureBox1.Image).GetPixel(x, y).B;
            }
        }

            int temp = 7;
            // Erstellen der 8 Bitebenen Bilder
            for (int picture = 0; picture <= 7; picture++)
            {
                i = 0;
                int R, G, B;
        for (int x = 0; x < pictureBox1.Image.Width; x++)
        {
            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {

                R = (byte)((pic[i]) & (int)Math.Pow(2, picture)); // Bitebenenbilder herausrechnen
                G = (byte)((pic[i + 1]) & (int)Math.Pow(2, picture)); // Bitebenenbilder herausrechnen
                B = (byte)((pic[i + 2]) & (int)Math.Pow(2, picture)); // Bitebenenbilder herausrechnen

                        // Helligkeit der Bitebenenbilder nachträglich erhöhen
                        if (picture == 6)
                            {
                                R = R << 1;
                                G = G << 1;
                                B = B << 1;
                            }

                            if (picture == 5)
                            {
                                R = R << 2;
                                G = G << 2;
                                B = B << 2;
                            }

                            if (picture == 4)
                            {
                                R = R << 3;
                                G = G << 3;
                                B = B << 3;
                            }

                            if (picture == 3)
                            {
                                R = R << 4;
                                G = G << 4;
                                B = B << 4;
                            }

                            if (picture == 2)
                            {
                                R = R << 5;
                                G = G << 5;
                                B = B << 5;
                            }

                            if (picture == 1)
                            {
                                R = R << 6;
                                G = G << 6;
                                B = B << 6;
                            }

                            if (picture == 0)
                            {
                                R = R << 7;
                                G = G << 7;
                                B = B << 7;
                            }

                // Die Farbwerte in das Array schreiben erst Rot Grün Blau index 3 und so weiter
                bitebenen_bilder[picture].SetPixel(x, y, Color.FromArgb(R, G, B));
                i = i + 3;
            }
        }
                temp--;
            }

            bitebene1_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene2_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene3_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene4_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene5_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene6_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene7_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene8_box.SizeMode = PictureBoxSizeMode.Zoom;

            bitebene1_box.Image = bitebenen_bilder[0];
            bitebene2_box.Image = bitebenen_bilder[1];
            bitebene3_box.Image = bitebenen_bilder[2];
            bitebene4_box.Image = bitebenen_bilder[3];
            bitebene5_box.Image = bitebenen_bilder[4];
            bitebene6_box.Image = bitebenen_bilder[5];
            bitebene7_box.Image = bitebenen_bilder[6];
            bitebene8_box.Image = bitebenen_bilder[7];
        }


        // Schreibt Strings auf Bitmaps
        private Bitmap draw_string_to_bitmap(Bitmap pic, String text, PointF location) {

            Graphics graphics = Graphics.FromImage(pic);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.Transparent);

            int size = 24;

            if (textbox_fontsize.Text.Length > 0) {
                size = Int32.Parse(textbox_fontsize.Text);
            }

            Font arialFont = new Font("Arial", size);
                
            // Schreibt den String auf die Grafik
            graphics.DrawString(text, arialFont, Brushes.Black, location);
                    
            // Gibt das neue überschriebene Bitmap zurück
            return pic;
        }

        // Addiert zwei Bitmaps
        public Bitmap add_bitmaps(Bitmap largeBmp, Bitmap smallBmp)
        {
            Graphics g = Graphics.FromImage(largeBmp);
            g.CompositingMode = CompositingMode.SourceOver;
            smallBmp.MakeTransparent();
            int margin = 5;
            int x = largeBmp.Width - smallBmp.Width - margin;
            int y = largeBmp.Height - smallBmp.Height - margin;
            g.DrawImage(smallBmp, new Point(x, y));
            return largeBmp;
        }

        // Bitebenen aus der Picturebox 3 anzeigen
        private void button4_Click(object sender, EventArgs e)
        {
            // 8 Bitebenen bitmaps erstellen
            Bitmap bitebene1 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);
            Bitmap bitebene2 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);
            Bitmap bitebene3 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);
            Bitmap bitebene4 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);
            Bitmap bitebene5 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);
            Bitmap bitebene6 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);
            Bitmap bitebene7 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);
            Bitmap bitebene8 = new Bitmap(pictureBox3.Image.Width, pictureBox3.Image.Height);

            // Dem bitebenen array zuweisen
            Bitmap[] bitebenen_bilder = new Bitmap[8];
            bitebenen_bilder[0] = bitebene1;
            bitebenen_bilder[1] = bitebene2;
            bitebenen_bilder[2] = bitebene3;
            bitebenen_bilder[3] = bitebene4;
            bitebenen_bilder[4] = bitebene5;
            bitebenen_bilder[5] = bitebene6;
            bitebenen_bilder[6] = bitebene7;
            bitebenen_bilder[7] = bitebene8;


            //Color[] pic = new Color[pictureBox1.Image.Width * pictureBox1.Image.Height];
            byte[] pic = new byte[pictureBox3.Image.Width * pictureBox3.Image.Height * 3];
            int i = 0;


            // Speichern der RGB Werte aller Pixel des Bildes
            for (int x = 0; x < pictureBox3.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox3.Image.Height; y++)
                {
                    pic[i++] = ((Bitmap)pictureBox3.Image).GetPixel(x, y).R;
                    pic[i++] = ((Bitmap)pictureBox3.Image).GetPixel(x, y).G;
                    pic[i++] = ((Bitmap)pictureBox3.Image).GetPixel(x, y).B;
                }
            }

            int temp = 7;
            // Erstellen der 8 Bitebenen Bilder
            for (int picture = 0; picture <= 7; picture++)
            {
                i = 0;
                int R, G, B;
                for (int x = 0; x < pictureBox3.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox3.Image.Height; y++)
                    {

                        R = (byte)((pic[i]) & (int)Math.Pow(2, picture)); // Bitebenenbilder herausrechnen
                        G = (byte)((pic[i + 1]) & (int)Math.Pow(2, picture));  // Bitebenenbilder herausrechnen
                        B = (byte)((pic[i + 2]) & (int)Math.Pow(2, picture));  // Bitebenenbilder herausrechnen

                        // Helligkeit der Bitebenenbilder nachträglich erhöhen
                        if (picture == 6)
                        {
                            R = R << 1;
                            G = G << 1;
                            B = B << 1;
                        }

                        if (picture == 5)
                        {
                            R = R << 2;
                            G = G << 2;
                            B = B << 2;
                        }

                        if (picture == 4)
                        {
                            R = R << 3;
                            G = G << 3;
                            B = B << 3;
                        }

                        if (picture == 3)
                        {
                            R = R << 4;
                            G = G << 4;
                            B = B << 4;
                        }

                        if (picture == 2)
                        {
                            R = R << 5;
                            G = G << 5;
                            B = B << 5;
                        }

                        if (picture == 1)
                        {
                            R = R << 6;
                            G = G << 6;
                            B = B << 6;
                        }

                        if (picture == 0)
                        {
                            R = R << 7;
                            G = G << 7;
                            B = B << 7;
                        }

                        // Die Farbwerte in das Array schreiben
                        bitebenen_bilder[picture].SetPixel(x, y, Color.FromArgb(R, G, B));
                        i = i + 3;
                    }
                }
                temp--;
            }

            bitebene1_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene2_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene3_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene4_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene5_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene6_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene7_box.SizeMode = PictureBoxSizeMode.Zoom;
            bitebene8_box.SizeMode = PictureBoxSizeMode.Zoom;

            bitebene1_box.Image = bitebenen_bilder[0];
            bitebene2_box.Image = bitebenen_bilder[1];
            bitebene3_box.Image = bitebenen_bilder[2];
            bitebene4_box.Image = bitebenen_bilder[3];
            bitebene5_box.Image = bitebenen_bilder[4];
            bitebene6_box.Image = bitebenen_bilder[5];
            bitebene7_box.Image = bitebenen_bilder[6];
            bitebene8_box.Image = bitebenen_bilder[7];
        }

        // Alle Pictureboxen leeren
        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox3.Image = null;
            bitebene1_box.Image = null;
            bitebene2_box.Image = null;
            bitebene3_box.Image = null;
            bitebene4_box.Image = null;
            bitebene5_box.Image = null;
            bitebene6_box.Image = null;
            bitebene7_box.Image = null;
            bitebene8_box.Image = null;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
