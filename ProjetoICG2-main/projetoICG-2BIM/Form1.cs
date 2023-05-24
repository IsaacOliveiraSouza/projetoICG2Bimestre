using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace projetoICG_2BIM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int esp = 0;
        int[] coordenadas = new int[10];
        int cont = 0;
        int qtdPontos = 0;
        int pos = 0;
        int pontosClicados = 0;
        int desenhoClicado = 0;
        Pen pen;
        int raio = 0;
        int altura = 0;
        int largura = 0;
        int[] rgb = new int[3];
        float[] linha = new float[10];
        int estilo = 0;
        int itemSelecionado = 0;
        int qtdLinha = 0;
        public void DesenhaRetangulo(PaintEventArgs e, Pen caneta, int x, int y, int altura, int largura)
        {
            //DesenhaRetangulo(e, pen, coordenadas[pos], coordenadas[pos + 1], coordenadas[pos + 2], coordenadas[pos + 3]);
            altura = Math.Abs(altura - x);
            largura = Math.Abs(largura - y);

            if (coordenadas[0] < coordenadas[2])
            {
                x = coordenadas[0];
            }
            else
            {
                x = coordenadas[2];
            }
            if (coordenadas[1] < coordenadas[3])
            {
                y = coordenadas[1];
            }
            else
            {
                y = coordenadas[3];
            }
            e.Graphics.DrawRectangle(caneta, x, y, altura, largura);
        }

        void elipse(PaintEventArgs e, int xc, int yc, int raiox, int raioy)
        {
            Color cores = cor(e, rgb[0], rgb[1], rgb[2]);
            int x;
            int y;
            double teta;
            for (int i = 0; i < 360; i++)
            {
                teta = i * Math.PI / 180;
                x = (int)(xc + raiox * Math.Cos(teta));
                y = (int)(yc + raioy * Math.Sin(teta));
                ponto(x, y, cores, e);
            }
        }
        void circulo(int xc, int yc, int raio, Color cor, PaintEventArgs e)
        {
            ponto(xc, yc, cor, e);
            for (int i = 0; i <= 360; i++)
            {
                int x = (int)(xc + raio * Math.Cos(i));
                int y = (int)(yc + raio * Math.Sin(i));
                ponto(x, y, cor, e);
            }
        }

        public Pen estiloLinha(PaintEventArgs e, Color cor, int esp, float[] valores)
        {

            pen.DashPattern = valores;
            return pen;

        }

        public Color cor(PaintEventArgs e, int r, int g, int b)
        {
            return Color.FromArgb(r, g, b);
        }
        void ponto(int x, int y, Color cor, PaintEventArgs e)
        {
            Pen caneta = new Pen(cor, esp);
            DesenhaLinha(e, caneta, x, y, x + 1, y);
        }

        public Pen caneta(Color cor, int esp)
        {
            Pen caneta = new Pen(cor, esp);
            return caneta;
        }

        public void triangulo(PaintEventArgs e, Pen caneta, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            DesenhaLinha(e, caneta, x1, y1, x2, y2);
            DesenhaLinha(e, caneta, x2, y2, x3, y3);
            DesenhaLinha(e, caneta, x3, y3, x1, y1);
        }

        public void DesenhaLinha(PaintEventArgs e, Pen caneta, int x0, int y0, int x1, int y1)
        {
            e.Graphics.DrawLine(caneta, x0, y0, x1, y1);
        }

        public void losango(PaintEventArgs e, Pen caneta, int x1, int y1)
        {
            int yUp = y1 + 100;
            int yDown = y1 - 100;
            int xR = x1 + 50;
            int xL = x1 - 50;

            DesenhaLinha(e, caneta, xR, y1, x1, yDown);
            DesenhaLinha(e, caneta, x1, yDown, xL, y1);
            DesenhaLinha(e, caneta, xL, y1, x1, yUp);
            DesenhaLinha(e, caneta, x1, yUp, xR, y1);

        }

        public void pentagono(PaintEventArgs e, Pen caneta, int x1, int y1)
        {
            int xR1 = x1 + 100;
            int xL1 = x1 - 100;
            int xR2 = xR1 - 50;
            int xL2 = xL1 + 50;
            int yDown = y1 + 100;
            int yUp = y1 - 100;

            DesenhaLinha(e, caneta, xR1, y1, x1, yUp);
            DesenhaLinha(e, caneta, x1, yUp, xL1, y1);
            DesenhaLinha(e, caneta, xL1, y1, xL2, yDown);
            DesenhaLinha(e, caneta, xL2, yDown, xR2, yDown);
            DesenhaLinha(e, caneta, xR2, yDown, xR1, y1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color cores = cor(e, rgb[0], rgb[1], rgb[2]);
            button28.BackColor = cores;



                pen = caneta(cores, esp);
            if (estilo == 1)
                pen = estiloLinha(e, cores, esp, linha);

            if (desenhoClicado == 1)
            {
                DesenhaLinha(e, pen, coordenadas[0], coordenadas[1], coordenadas[2], coordenadas[3]);
            }
            else if (desenhoClicado == 2)
            {
                circulo(coordenadas[0], coordenadas[1], raio, cores, e);

            }
            else if (desenhoClicado == 3)
            {
                triangulo(e, pen, coordenadas[0], coordenadas[1], coordenadas[2], coordenadas[3], coordenadas[4], coordenadas[5]);
            }
            else if (desenhoClicado == 4)
            {
                losango(e, pen, coordenadas[0], coordenadas[1]);
            }
            else if (desenhoClicado == 5)
            {
                DesenhaRetangulo(e, pen, coordenadas[pos], coordenadas[pos + 1], coordenadas[pos + 2], coordenadas[pos + 3]);
            }
            else if (desenhoClicado == 6)
            {
                pentagono(e, pen, coordenadas[0], coordenadas[1]);
            }
            else if (desenhoClicado == 7)
            {
                elipse(e, coordenadas[0], coordenadas[1], altura, largura);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //linha
            qtdPontos = 2;
            desenhoClicado = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //circulo
            desenhoClicado = 2;
            string r = Interaction.InputBox("Informe o raio do circulo", "Raio Circulo", "0", 100, 100);
            while (string.IsNullOrEmpty(r) == true)
            {
                r = Interaction.InputBox("Informe a Largura corretamente!", "Elipse Altura", "0", 100, 100);
            }
            raio = int.Parse(r);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //triangulo
            qtdPontos = 3;
            desenhoClicado = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //losango
            desenhoClicado = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //quadrado
            qtdPontos = 2;
            desenhoClicado = 5;


        }

        private void button6_Click(object sender, EventArgs e)
        {
            //pent
            qtdPontos = 5;
            desenhoClicado = 6;

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pontosClicados < qtdPontos)
            {
                coordenadas[pos++] = e.X;
                coordenadas[pos++] = e.Y;
                pontosClicados++;

            }
            if (pontosClicados == qtdPontos)
            {
                Invalidate();
                pontosClicados = 0;
                pos = 0;
            }

            if (desenhoClicado == 2 || desenhoClicado == 4 || desenhoClicado == 6 || desenhoClicado == 7)
            {
                coordenadas[pos++] = e.X;
                coordenadas[pos++] = e.Y;
                Invalidate();

                pos = 0;
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //estilo
            itemSelecionado = comboBox1.SelectedIndex;
            switch (itemSelecionado)
            {
                //copia e cola esse switch no leitor de dat e substitui
                case 0:
                    //dashdot
                    Array.Resize(ref linha, 4);
                    linha[0] = 5;
                    linha[1] = 2;
                    linha[2] = 1;
                    linha[3] = 2;
                    estilo = 1;
                    qtdLinha = 4;
                    break;
                case 1:
                    //dash
                    Array.Resize(ref linha, 2);
                    linha[0] = 5;
                    linha[1] = 1;
                    estilo = 1;
                    qtdLinha = 2;
                    break;

                case 2:
                    //dashdotdot
                    Array.Resize(ref linha, 4);
                    linha[0] = 5;
                    linha[1] = 2;
                    linha[2] = 15;
                    linha[3] = 4;
                    estilo = 1;
                    qtdLinha = 4;
                    break;
                case 3:
                    //solid
                    Array.Resize(ref linha, 1);
                    linha[0] = 1;
                    qtdLinha = 1;
                    estilo = 1;
                    break;

                case 4:
                    //dot
                    Array.Resize(ref linha, 2);
                    linha[0] = 1;
                    linha[1] = 1;
                    qtdLinha = 2;
                    estilo = 1;
                    break;


            }


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //expessura
            esp = comboBox2.SelectedIndex;
        }


        private void button7_Click(object sender, EventArgs e)
        {
            //botao preto
            rgb[0] = 0;
            rgb[1] = 0;
            rgb[2] = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //botao branco
            rgb[0] = 255;
            rgb[1] = 255;
            rgb[2] = 255;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //botao cinza padrao
            rgb[0] = 128;
            rgb[1] = 128;
            rgb[2] = 128;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //botao cinza claro
            rgb[0] = 192;
            rgb[1] = 192;
            rgb[2] = 192;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //botao marrom
            rgb[0] = 128;
            rgb[1] = 0;
            rgb[2] = 0;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //botao marrom-claro
            rgb[0] = 210;
            rgb[1] = 105;
            rgb[2] = 30;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //botao vermelho
            rgb[0] = 255;
            rgb[1] = 0;
            rgb[2] = 0;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //botao rosa
            rgb[0] = 255;
            rgb[1] = 192;
            rgb[2] = 255;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //botao laranja
            rgb[0] = 255;
            rgb[1] = 128;
            rgb[2] = 0;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            //botao elipse
            desenhoClicado = 7;
            string alt = Interaction.InputBox("Informe a Altura da elipse", "Elipse Altura", "0", 100, 100);

            while (string.IsNullOrEmpty(alt) == true)
            {
                alt = Interaction.InputBox("Informe a Altura corretamente!", "Elipse Altura", "0", 100, 100);
            }
            string larg = Interaction.InputBox("Informe a Largura da elipse", "Elipse Altura", "0", 100, 100);
            while (string.IsNullOrEmpty(larg) == true)
            {
                larg = Interaction.InputBox("Informe a Largura corretamente!", "Elipse Altura", "0", 100, 100);
            }

            altura = int.Parse(alt);
            largura = int.Parse(larg);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            //laranja claro
            rgb[0] = 255;
            rgb[1] = 192;
            rgb[2] = 128;

        }

        private void button16_Click(object sender, EventArgs e)
        {
            //amarelo 
            rgb[0] = 255;
            rgb[1] = 255;
            rgb[2] = 0;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            //amarelo claro

            rgb[0] = 255;
            rgb[1] = 255;
            rgb[2] = 192;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //verde escuro
            rgb[0] = 0;
            rgb[1] = 128;
            rgb[2] = 0;

        }

        private void button25_Click(object sender, EventArgs e)
        {
            //verde claro
            rgb[0] = 0;
            rgb[1] = 255;
            rgb[2] = 0;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //ciano escuro
            rgb[0] = 0;
            rgb[1] = 192;
            rgb[2] = 255;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //ciano
            rgb[0] = 0;
            rgb[1] = 255;
            rgb[2] = 255;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //azul
            rgb[0] = 0;
            rgb[1] = 0;
            rgb[2] = 255;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //roxo claro
            rgb[0] = 138;
            rgb[1] = 43;
            rgb[2] = 226;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //roxo mais claro
            rgb[0] = 192;
            rgb[1] = 192;
            rgb[2] = 255;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //anil
            rgb[0] = 128;
            rgb[1] = 128;
            rgb[2] = 255;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //botao cor principal
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //gravar txt
            string nomeArquivo = Interaction.InputBox("Informe o nome do arquivo", "Arquivo", "0", 100, 100);
            while (string.IsNullOrEmpty(nomeArquivo) == true)
            {
                nomeArquivo = Interaction.InputBox("Informe o nome do arquivo!", "Arquivo", "0", 100, 100);
            }

            string caminho = @"E:\Desktop\" + nomeArquivo + ".dat";

            if (desenhoClicado == 1)
            {
                File.AppendAllText(caminho, desenhoClicado.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdPontos.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[3].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, esp.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdLinha.ToString() + Environment.NewLine);
                for (int i = 0; i <= qtdLinha-1; i++)
                {
                    if (linha[i] != 0)
                    {
                        File.AppendAllText(caminho, linha[i].ToString() + Environment.NewLine);
                    }
                }
                Invalidate();
            }



            else if (desenhoClicado == 2)
            {
                File.AppendAllText(caminho, desenhoClicado.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, raio.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, esp.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdLinha.ToString() + Environment.NewLine);
                for (int i = 0; i <= qtdLinha-1; i++)
                {
                    if (linha[i] != 0)
                    {
                        File.AppendAllText(caminho, linha[i].ToString() + Environment.NewLine);
                    }
                }
                Invalidate();
            }
            else if (desenhoClicado == 3)
            {

                File.AppendAllText(caminho, desenhoClicado.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdPontos.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[3].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[4].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[5].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, esp.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdLinha.ToString() + Environment.NewLine);
                for (int i = 0; i <= qtdLinha-1; i++)
                {
                    if (linha[i] != 0)
                    {
                        File.AppendAllText(caminho, linha[i].ToString() + Environment.NewLine);
                    }
                }
                Invalidate();
            }
            else if (desenhoClicado == 4)
            {
                File.AppendAllText(caminho, desenhoClicado.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdPontos.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, esp.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdLinha.ToString() + Environment.NewLine);
                for (int i = 0; i <= qtdLinha-1; i++)
                {
                    if (linha[i] != 0)
                    {
                        File.AppendAllText(caminho, linha[i].ToString() + Environment.NewLine);
                    }
                }
                Invalidate();
            }
            else if (desenhoClicado == 5)
            {
                File.AppendAllText(caminho, desenhoClicado.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdPontos.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[3].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, esp.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdLinha.ToString() + Environment.NewLine);
                for (int i = 0; i <= qtdLinha-1; i++)
                {
                    if (linha[i] != 0)
                    {
                        File.AppendAllText(caminho, linha[i].ToString() + Environment.NewLine);
                    }
                }
                Invalidate();
            }
            else if (desenhoClicado == 6)
            {
                File.AppendAllText(caminho, desenhoClicado.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdPontos.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, esp.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdLinha.ToString() + Environment.NewLine);
                for (int i = 0; i <= qtdLinha-1; i++)
                {
                    if (linha[i] != 0)
                    {
                        File.AppendAllText(caminho, linha[i].ToString() + Environment.NewLine);
                    }
                }
                Invalidate();
            }
            else if (desenhoClicado == 7)
            {
                File.AppendAllText(caminho, desenhoClicado.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdPontos.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, coordenadas[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, altura.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, largura.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[0].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[1].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, rgb[2].ToString() + Environment.NewLine);
                File.AppendAllText(caminho, esp.ToString() + Environment.NewLine);
                File.AppendAllText(caminho, qtdLinha.ToString() + Environment.NewLine);
                for (int i = 0; i <= qtdLinha-1; i++)
                {
                    if (linha[i] != 0)
                    {
                        File.AppendAllText(caminho, linha[i].ToString() + Environment.NewLine);
                    }
                }
                Invalidate();
            }


        }

        private void button32_Click(object sender, EventArgs e)
        {
            //LER ARQUIVO
            openFileDialog1.ShowDialog();
            string nomeArquivo = openFileDialog1.FileName;

            string[] lines = System.IO.File.ReadAllLines(nomeArquivo);
            desenhoClicado = int.Parse(lines[0]);
            int ultimaLinha = 0;
            int indiceLinha = 0;
            if (desenhoClicado == 1)
            {
                qtdPontos = int.Parse(lines[1]);
                coordenadas[0] = int.Parse(lines[2]);
                coordenadas[1] = int.Parse(lines[3]);
                coordenadas[2] = int.Parse(lines[4]);
                coordenadas[3] = int.Parse(lines[5]);
                rgb[0] = int.Parse(lines[6]);
                rgb[1] = int.Parse(lines[7]);
                rgb[2] = int.Parse(lines[8]);
                esp = int.Parse(lines[9]);
                qtdLinha = int.Parse(lines[10]);
                ultimaLinha = 11;
                Array.Resize(ref linha, qtdLinha);
                if (qtdLinha > 0)
                {
                    for (int i = ultimaLinha; i <= ultimaLinha + qtdLinha - 1; i++)
                    {
                        linha[indiceLinha] = int.Parse(lines[i]);
                        indiceLinha++;
                    }
                }

                Invalidate();
            }
            else if (desenhoClicado == 2)
            {
                coordenadas[0] = int.Parse(lines[1]);
                coordenadas[1] = int.Parse(lines[2]);
                raio = int.Parse(lines[3]);
                rgb[0] = int.Parse(lines[4]);
                rgb[1] = int.Parse(lines[5]);
                rgb[2] = int.Parse(lines[6]);
                esp = int.Parse(lines[7]);
                qtdLinha = int.Parse(lines[8]);
                ultimaLinha = 9;
                if (qtdLinha > 0)
                {
                    Array.Resize(ref linha, qtdLinha);
                    for (int i = ultimaLinha; i <= ultimaLinha + qtdLinha - 1; i++)
                    {
                        linha[indiceLinha] = int.Parse(lines[i]);

                        indiceLinha++;
                    }
                }

                Invalidate();
            }
            else if (desenhoClicado == 3)
            {
                qtdPontos = int.Parse(lines[1]);
                coordenadas[0] = int.Parse(lines[2]);
                coordenadas[1] = int.Parse(lines[3]);
                coordenadas[2] = int.Parse(lines[4]);
                coordenadas[3] = int.Parse(lines[5]);
                coordenadas[4] = int.Parse(lines[6]);
                coordenadas[5] = int.Parse(lines[7]);
                rgb[0] = int.Parse(lines[8]);
                rgb[1] = int.Parse(lines[9]);
                rgb[2] = int.Parse(lines[10]);
                esp = int.Parse(lines[11]);
                qtdLinha = int.Parse(lines[12]);
                ultimaLinha = 13;
                if (qtdLinha > 0)
                {
                    Array.Resize(ref linha, qtdLinha);
                    for (int i = ultimaLinha; i <= ultimaLinha + qtdLinha - 1; i++)
                    {
                        linha[indiceLinha] = int.Parse(lines[i]);
                        indiceLinha++;
                    }
                    Invalidate();
                }
            }
            else if (desenhoClicado == 4)
            {
                qtdPontos = int.Parse(lines[1]);
                coordenadas[0] = int.Parse(lines[2]);
                coordenadas[1] = int.Parse(lines[3]);
                rgb[0] = int.Parse(lines[4]);
                rgb[1] = int.Parse(lines[5]);
                rgb[2] = int.Parse(lines[6]);
                esp = int.Parse(lines[7]);
                qtdLinha = int.Parse(lines[8]);
                ultimaLinha = 9;
                if (qtdLinha > 0)
                {
                    Array.Resize(ref linha, qtdLinha);
                    for (int i = ultimaLinha; i <= ultimaLinha + qtdLinha - 1; i++)
                    {
                        linha[indiceLinha] = int.Parse(lines[i]);

                        indiceLinha++;
                    }
                    Invalidate();
                }
            }
            else if (desenhoClicado == 5)
            {
                qtdPontos = int.Parse(lines[1]);
                coordenadas[0] = int.Parse(lines[2]);
                coordenadas[1] = int.Parse(lines[3]);
                coordenadas[2] = int.Parse(lines[4]);
                coordenadas[3] = int.Parse(lines[5]);
                rgb[0] = int.Parse(lines[6]);
                rgb[1] = int.Parse(lines[7]);
                rgb[2] = int.Parse(lines[8]);
                esp = int.Parse(lines[9]);
                qtdLinha = int.Parse(lines[10]);
                ultimaLinha = 11;
                if (qtdLinha > 0)
                {
                    Array.Resize(ref linha, qtdLinha);
                    for (int i = ultimaLinha; i <= ultimaLinha + qtdLinha - 1; i++)
                    {
                        linha[indiceLinha] = int.Parse(lines[i]);

                        indiceLinha++;
                    }
                    Invalidate();
                }
            }

            //pentagono

            else if (desenhoClicado == 6)
            {
                qtdPontos = int.Parse(lines[1]);
                coordenadas[0] = int.Parse(lines[2]);
                coordenadas[1] = int.Parse(lines[3]);
                rgb[0] = int.Parse(lines[4]);
                rgb[1] = int.Parse(lines[5]);
                rgb[2] = int.Parse(lines[6]);
                esp = int.Parse(lines[7]);
                qtdLinha = int.Parse(lines[8]);
                ultimaLinha = 9;
                if (qtdLinha > 0)
                {
                    Array.Resize(ref linha, qtdLinha);
                    for (int i = ultimaLinha; i <= ultimaLinha + qtdLinha - 1; i++)
                    {
                        linha[indiceLinha] = int.Parse(lines[i]);

                        indiceLinha++;
                    }
                    Invalidate();
                }
            }
            else if (desenhoClicado == 7)
            {
                qtdPontos = int.Parse(lines[1]);
                coordenadas[0] = int.Parse(lines[2]);
                coordenadas[1] = int.Parse(lines[3]);
                altura = int.Parse(lines[4]);
                largura = int.Parse(lines[5]);
                rgb[0] = int.Parse(lines[6]);
                rgb[1] = int.Parse(lines[7]);
                rgb[2] = int.Parse(lines[8]);
                esp = int.Parse(lines[9]);
                qtdLinha = int.Parse(lines[10]);
                ultimaLinha = 11;
                if (qtdLinha > 0)
                {
                    Array.Resize(ref linha, qtdLinha);
                    for (int i = ultimaLinha; i <= ultimaLinha + qtdLinha - 1; i++)
                    {
                        linha[indiceLinha] = int.Parse(lines[i]);

                        indiceLinha++;
                    }
                }

                Invalidate();

            }
           
        }
    }
}