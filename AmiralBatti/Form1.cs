using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace AmiralBatti
{
    public partial class Form1 : Form
    {


        List<Button> oyuncuPozisyonuButtons;
        List<Button> dusmanPozisyonuButtons;

        Random rand = new Random();

        int toplamGemi = 6;
        int tur = 30;
        int oyuncuPuani;
        int dusmanPuani;


        public Form1()
        {
            InitializeComponent();
            OyunaBasla();
        }

        private void DusmanOynamaTimerEvent(object sender, EventArgs e)
        {
            if (oyuncuPozisyonuButtons.Count > 0 && tur > 0)
            {
                tur -= 1;

                txtTursayisi.Text = "Tur: " + tur;

                int index = rand.Next(oyuncuPozisyonuButtons.Count);

                if ((string)oyuncuPozisyonuButtons[index].Tag == "oyuncuGemi")
                {
                    oyuncuPozisyonuButtons[index].BackgroundImage = Properties.Resources.bombaIcon;
                    dusmanHareketi.Text = oyuncuPozisyonuButtons[index].Text;
                    oyuncuPozisyonuButtons[index].Enabled = false;
                    oyuncuPozisyonuButtons[index].BackColor = Color.DarkBlue;
                    oyuncuPozisyonuButtons.RemoveAt(index);
                    dusmanPuani += 1;
                    txtDusman.Text = dusmanPuani.ToString();
                    DusmanOynamaTimer.Stop();

                }
                else
                {
                    oyuncuPozisyonuButtons[index].BackgroundImage = Properties.Resources.carpiIcon;
                    dusmanHareketi.Text = oyuncuPozisyonuButtons[index].Text;
                    oyuncuPozisyonuButtons[index].Enabled = false;
                    oyuncuPozisyonuButtons[index].BackColor = Color.DarkBlue;
                    oyuncuPozisyonuButtons.RemoveAt(index);
                    DusmanOynamaTimer.Stop();

                }

            }

            if (tur < 1 || dusmanPuani > 5 || oyuncuPuani > 5)
            {
                if (oyuncuPuani > dusmanPuani)
                {
                    MessageBox.Show("Kazandın..");
                    OyunaBasla();

                }
                else if (dusmanPuani > oyuncuPuani)
                {
                    MessageBox.Show("Kaybettin..");
                    OyunaBasla();

                }
                else if (dusmanPuani == oyuncuPuani)
                {
                    MessageBox.Show("Kazanan yok..");
                    OyunaBasla();


                }
            }

        }

        private void SaldırıButtonEvent(object sender, EventArgs e)
        {
            if (DusmanKonumuListBox.Text != "")
            {
                var saldiriPozisyonu = DusmanKonumuListBox.Text.ToLower();

                int index = dusmanPozisyonuButtons.FindIndex(a => a.Name == saldiriPozisyonu);

                if (dusmanPozisyonuButtons[index].Enabled && tur > 0)
                {
                    tur -= 1;
                    txtTursayisi.Text = "Tur: " + tur;

                    if ((string)dusmanPozisyonuButtons[index].Tag == "dusmanGemi")
                    {
                        dusmanPozisyonuButtons[index].Enabled = false;
                        dusmanPozisyonuButtons[index].BackgroundImage = Properties.Resources.bombaIcon;
                        dusmanPozisyonuButtons[index].BackColor = Color.DarkBlue;
                        oyuncuPuani += 1;
                        txtOyuncu.Text = oyuncuPuani.ToString();
                        DusmanOynamaTimer.Start();
                    }
                    else
                    {
                        dusmanPozisyonuButtons[index].Enabled = false;
                        dusmanPozisyonuButtons[index].BackgroundImage = Properties.Resources.carpiIcon;
                        dusmanPozisyonuButtons[index].BackColor = Color.DarkBlue;
                        DusmanOynamaTimer.Start();

                    }

                }


            }
            else
            {
                MessageBox.Show("Önce saldırı konumunu seçin.", "Dikkat!!");
            }

        }

        private void OyuncuPozisyonuButtonEvent(object sender, EventArgs e)
        {
            if (toplamGemi > 3)
            {
                var button = (Button)sender;

                button.Enabled = false;
                button.Tag = "oyuncuGemi";
                button.BackColor = Color.Green;
                toplamGemi -= 1;
            }

            else if (toplamGemi > 1)
            {
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "oyuncuGemi";
                button.BackColor = Color.Red;
                toplamGemi -= 1;
            }
            else if (toplamGemi == 1)
            {
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "oyuncuGemi";
                button.BackColor = Color.Yellow;
                toplamGemi -= 1;
            }
            else if (toplamGemi == 0)
            {
                btnSaldırı.Enabled = true;
                btnSaldırı.BackColor = Color.Red;
                btnSaldırı.ForeColor = Color.White;

                txtYardım.Text = "Saldırı pozisyonunu seciniz.";
            }



        }
        private void OyunaBasla()
        {
            MessageBox.Show("Uçak Gemisi için yanyana 3, Kruvazör için yanyana 2 ve Fırkateyn içi de 1 kutu seçiniz..."); 

            oyuncuPozisyonuButtons = new List<Button> { a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4, d1, d2, d3, d4 };
            dusmanPozisyonuButtons = new List<Button> { e1, e2, e3, e4, f1, f2, f3, f4, g1, g2, g3, g4, h1, h2, h3, h4 };


            DusmanKonumuListBox.Items.Clear();

            DusmanKonumuListBox.Text = null;

            txtYardım.Text = "Uçak Gemisi için yanyana 3, Kruvazör için yanyana 2 ve Fırkateyn içi de 1 kutu seçiniz...";

            for (int i = 0; i < dusmanPozisyonuButtons.Count; i++)
            {
                dusmanPozisyonuButtons[i].Enabled = true;
                dusmanPozisyonuButtons[i].Tag = null;
                dusmanPozisyonuButtons[i].BackColor = Color.LightSteelBlue;
                dusmanPozisyonuButtons[i].BackgroundImage = null;
                DusmanKonumuListBox.Items.Add(dusmanPozisyonuButtons[i].Text);
            }

            for (int i = 0; i < oyuncuPozisyonuButtons.Count; i++)
            {
                oyuncuPozisyonuButtons[i].Enabled = true;
                oyuncuPozisyonuButtons[i].Tag = null;
                oyuncuPozisyonuButtons[i].BackColor = Color.LightSteelBlue;
                oyuncuPozisyonuButtons[i].BackgroundImage = null;
            }

            oyuncuPuani = 0;
            dusmanPuani = 0;
            tur = 30;
            toplamGemi = 6;

            txtOyuncu.Text = oyuncuPuani.ToString();
            txtDusman.Text = dusmanPuani.ToString();
            dusmanHareketi.Text = "E1";

            btnSaldırı.Enabled = false;

            dusmanKonumuSecici();

        }
        private void dusmanKonumuSecici()
        {

            for (int i = 0; i < 6; i++)
            {
                int index = rand.Next(dusmanPozisyonuButtons.Count);

                if (dusmanPozisyonuButtons[index].Enabled == true && (string)dusmanPozisyonuButtons[index].Tag == null)
                {
                    dusmanPozisyonuButtons[index].Tag = "dusmanGemi";

                    Debug.WriteLine("Dusman Pozisyon: " + dusmanPozisyonuButtons[index].Text);
                }
                else
                {
                    index = rand.Next(dusmanPozisyonuButtons.Count);
                }
            }

        }
    }
}
