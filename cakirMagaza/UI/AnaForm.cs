﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cakirMagaza.BL;
using cakirMagaza.UI;



namespace cakirMagaza
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        Müşteriler mf=new Müşteriler(); 
        Ürünler uf=new Ürünler();   

       

        private void btnMüşteriler_Click(object sender, EventArgs e)
        {
            mf.ShowDialog();
        }

        private void btnÜrünler_Click(object sender, EventArgs e)
        {
            uf.ShowDialog();
        }

        private void btnSatışYap_Click(object sender, EventArgs e)
        {
            FrmSatis frm = new FrmSatis()
            {
                Text = "Satış Yap",
                Satis =new Satis()
                {
                    ID=Guid.NewGuid(),
                }
            };

            if(frm.ShowDialog()==DialogResult.OK)
            {
            tekrar:
                var sonuc = frm.ShowDialog();
                if (sonuc == DialogResult.OK)
                {
                    bool b = Blogic.SatisEkle(frm.Satis);
                   if (b)
                    {
                        DataSet ds1 = Blogic.SatisDetay();
                        if (ds1 != null)
                            dataGridView1.DataSource = ds1.Tables[0];

                    }
                    else
                        goto tekrar;
                }
            }
        }



        private void AnaForm_Load(object sender, EventArgs e)
        {
            DataSet ds1 = Blogic.SatisDetay();
            if (ds1 != null)
                dataGridView1.DataSource = ds1.Tables[0];

            DataSet ds2 = Blogic.OdemeDetay();
            if (ds2 != null)
                dataGridView2.DataSource = ds2.Tables[0];
        }

        private void btnSatisDuzenle_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];

            FrmSatis frm = new FrmSatis()
            {
                Text = "Satış Güncelle",
                Güncelleme = true,
                Satis = new Satis()
                {
                    ID = Guid.Parse(row.Cells[0].Value.ToString()),
                    MusteriID = Guid.Parse(row.Cells[1].Value.ToString()),
                    UrunID= Guid.Parse(row.Cells[2].Value.ToString()),
                    Fiyat= double.Parse(row.Cells[7].Value.ToString()),
                    Tarih= DateTime.Parse(row.Cells[8].Value.ToString()),
                    
                },
            };

            var sonuc = frm.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                bool b = Blogic.SatisGüncelle(frm.Satis);
                if (b)
                {

                    row.Cells[1].Value = frm.Satis.MusteriID;
                    row.Cells[2].Value = frm.Satis.UrunID;
                    row.Cells[7].Value = frm.Satis.Fiyat;
                    row.Cells[8].Value = frm.Satis.Tarih;

                }
            }

        }

        private void btnSatisSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            var ID = row.Cells[0].Value.ToString();


            var sonuc = MessageBox.Show("Dikkat! Kayıt Siliniyor", "Silmeyi Onayla", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (sonuc == DialogResult.OK)
            {
                bool b = BL.Blogic.SatisSil(ID);
                if (b)
                {
                    DataSet ds = BL.Blogic.SatisDetay();
                    if (ds != null)
                        dataGridView1.DataSource = ds.Tables[0];

                }
            }
        }

        private void btnÖdemeYap_Click(object sender, EventArgs e)
        {
            FrmOdeme frm = new FrmOdeme()
            {
                Text = "Ödeme Yap",
                Odeme = new Odeme()
                {
                    ID = Guid.NewGuid(),
                }
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
            tekrar:
                var sonuc = frm.ShowDialog();
                if (sonuc == DialogResult.OK)
                {
                    bool b = Blogic.OdemeEkle(frm.Odeme);
                    if (b)
                    {
                        DataSet ds2 = Blogic.OdemeDetay();
                        if (ds2 != null)
                            dataGridView2.DataSource = ds2.Tables[0];

                    }
                    else
                        goto tekrar;
                }
            }
        }

        private void btnÖdemeDüzenle_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = dataGridView2.SelectedRows[0];

            FrmOdeme frm = new FrmOdeme()
            {
                Text = "Satış Güncelle",
                Güncelleme = true,
                Odeme = new Odeme()
                {
                    ID = Guid.Parse(row.Cells[0].Value.ToString()),
                    MusteriID = Guid.Parse(row.Cells[1].Value.ToString()),
                    Tarih = DateTime.Parse(row.Cells[3].Value.ToString()),
                    Tutar = double.Parse(row.Cells[4].Value.ToString()),
                    Tur = row.Cells[5].Value.ToString(),
                    Aciklama = row.Cells[6].Value.ToString(),

                },
            };

            var sonuc = frm.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                bool b = Blogic.OdemeGüncelle(frm.Odeme);

                if (b)
                {
                    row.Cells[1].Value = frm.Odeme.MusteriID;
                    row.Cells[3].Value = frm.Odeme.Tarih;
                    row.Cells[4].Value = frm.Odeme.Tutar;
                    row.Cells[5].Value = frm.Odeme.Tur;
                    row.Cells[6].Value = frm.Odeme.Aciklama;

                }

            }
        }

        private void btnÖdemeSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView2.SelectedRows[0];
            var ID = row.Cells[0].Value.ToString();


            var sonuc = MessageBox.Show("Seçili kayıt silinsin mi?", "Silmeyi onayla",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (sonuc == DialogResult.OK)
            {
                bool b = Blogic.OdemeSil(ID);

                if (b)
                {

                    DataSet ds = Blogic.OdemeDetay();
                    if (ds != null)
                        dataGridView2.DataSource = ds.Tables[0];
                }

            }
        }
    }
}
    